using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerMovement : MonoBehaviour
{

    public int playerNumber = 1;                // Determines what player this script belongs to
    public GameObject shot;                     // The shot gameobject that allows the player to shoot forward
    public GameObject backShot;                 // The backShot gameobject that allows the player to shoot back
    public Transform shotSpawn;                 // The shot spawn, which is where the shot will spawn from the front
    public Transform shotSpawn2;                // The shot spawn, which is where the shot will spawn from the back
    public Slider healthSlider;                 // Controller for the players health
    public Text winText;                        // The text that displays "You Win" if the player reaches the end
    public cameraMovement cameraScript;         // The camera script that will keep its eye on the player
    public GameManager gameManagerScript;       // The game manager, which will determine if the game is over for the player
    public Car carScript;                       // The car script that will be called if the player picks up scrap or gas
    public float speed = 10f;                   // Controls the speed of the player
    public int health = 10;                     // Controls the health of the player
    public Animator animator;                   // Controls the animator for the player movements
    public Animator gunAnimator;                // Controls the animator for the players guns
    private string fire;                        // Determines if the player shooting is 1 or 2
    private string movementVertical;            // Allows the player to move vertically
    private string movementHorizontal;          // Allows the player to move horizontally
    private Rigidbody2D rb;                     // Grabs the 2d rigidbody off the player
    private SpriteRenderer spriteR;             // Grabs the sprite renderer off the player
    private float movementInputHorizontal;      // Controls the input for moving horizontally
    private float movementInputVertical;        // Controls the input for moving vertically
    private float nextFire;                     // Determines how long the player has to wait before they can shoot again
    private float fireRate;                     // Determines the fire rate of the players
    private int ammo = 1000;                    // Controls the amount of ammo the player has
    private int inventory;                      // Controls what is in the players inventory


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteR = GetComponent<SpriteRenderer>();
        winText = winText.GetComponent<Text>();
        movementHorizontal = "Horizontal" + playerNumber;
        movementVertical = "Vertical" + playerNumber;
        fire = "Fire" + playerNumber;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        winText.text = "";
        fireRate = .5f;
    }

    void Start()
    {
        health = 10;
        healthSlider.value = health;

        //fireRate = .5f;
    }

    void Update()
    {
        movementInputHorizontal = Input.GetAxis(movementHorizontal);
        movementInputVertical = Input.GetAxis(movementVertical);

        if (Input.GetKeyDown(KeyCode.H) && Time.time > nextFire && ammo > 0 && playerNumber == 1)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            --ammo;
            //SetAmmoText();
        }

        if (Input.GetKeyDown(KeyCode.G) && Time.time > nextFire && ammo > 0 && playerNumber == 1)
        {
            nextFire = Time.time + fireRate;
            Instantiate(backShot, shotSpawn2.position, shotSpawn.rotation);
            --ammo;
            //SetAmmoText();
        }

        if (Input.GetKeyDown(KeyCode.Period) && Time.time > nextFire && ammo > 0 && playerNumber == 2)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            --ammo;
            //SetAmmoText();
        }

        if (Input.GetKeyDown(KeyCode.Comma) && Time.time > nextFire && ammo > 0 && playerNumber == 2)
        {
            nextFire = Time.time + fireRate;
            Instantiate(backShot, shotSpawn2.position, shotSpawn.rotation);
            --ammo;
            //SetAmmoText();
        }

        if (health < 0)
        {
            gameManagerScript.PlayerDeathCount();
            healthSlider.value = health;
            cameraScript.UnTrackPlayer(this.gameObject);
            Destroy(this.gameObject);
        }

        if ((Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.E)) && inventory > 0)
        {
            carScript.AddHealth();
            --inventory;
        }

        if ((Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Q)) && inventory > 0)
        {
            ammo += 10;
            --inventory;
            Debug.Log(ammo);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && playerNumber == 2)
        {
            animator.SetBool("turnBack", false);
            animator.SetBool("turnFront", false);
            animator.SetBool("turnLeft", false);
            animator.SetBool("turnRight", true);
            gunAnimator.SetBool("gunLeft", false);
            gunAnimator.SetBool("gunRight", true);
            gunAnimator.SetBool("gunIdle", false);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && playerNumber == 2)
        {
            animator.SetBool("turnBack", false);
            animator.SetBool("turnFront", false);
            animator.SetBool("turnLeft", true);
            animator.SetBool("turnRight", false);
            gunAnimator.SetBool("gunLeft", true);
            gunAnimator.SetBool("gunRight", false);
            gunAnimator.SetBool("gunIdle", false);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && playerNumber == 2)
        {
            animator.SetBool("turnBack", true);
            animator.SetBool("turnFront", false);
            animator.SetBool("turnLeft", false);
            animator.SetBool("turnRight", false);
            gunAnimator.SetBool("gunLeft", false);
            gunAnimator.SetBool("gunRight", false);
            gunAnimator.SetBool("gunIdle", true);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && playerNumber == 2)
        {
            animator.SetBool("turnBack", false);
            animator.SetBool("turnFront", true);
            animator.SetBool("turnLeft", false);
            animator.SetBool("turnRight", false);
            gunAnimator.SetBool("gunLeft", false);
            gunAnimator.SetBool("gunRight", false);
            gunAnimator.SetBool("gunIdle", true);
        }
        else if (Input.GetKeyDown(KeyCode.D) && playerNumber == 1)
        {
            animator.SetBool("turnBack", false);
            animator.SetBool("turnFront", false);
            animator.SetBool("turnLeft", false);
            animator.SetBool("turnRight", true);
            gunAnimator.SetBool("gunLeft", false);
            gunAnimator.SetBool("gunRight", true);
            gunAnimator.SetBool("gunIdle", false);
        }
        else if (Input.GetKeyDown(KeyCode.A) && playerNumber == 1)
        {
            animator.SetBool("turnBack", false);
            animator.SetBool("turnFront", false);
            animator.SetBool("turnLeft", true);
            animator.SetBool("turnRight", false);
            gunAnimator.SetBool("gunLeft", true);
            gunAnimator.SetBool("gunRight", false);
            gunAnimator.SetBool("gunIdle", false);
        }
        else if (Input.GetKeyDown(KeyCode.W) && playerNumber == 1)
        {
            animator.SetBool("turnBack", true);
            animator.SetBool("turnFront", false);
            animator.SetBool("turnLeft", false);
            animator.SetBool("turnRight", false);
            gunAnimator.SetBool("gunLeft", false);
            gunAnimator.SetBool("gunRight", false);
            gunAnimator.SetBool("gunIdle", true);
        }
        else if (Input.GetKeyDown(KeyCode.S) && playerNumber == 1)
        {
            animator.SetBool("turnBack", false);
            animator.SetBool("turnFront", true);
            animator.SetBool("turnLeft", false);
            animator.SetBool("turnRight", false);
            gunAnimator.SetBool("gunLeft", false);
            gunAnimator.SetBool("gunRight", false);
            gunAnimator.SetBool("gunIdle", true);
        }
    }

    void FixedUpdate()
    {
        Vector2 movement = new Vector2(movementInputHorizontal, movementInputVertical);

        rb.velocity = movement * speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ammo"))
        {
            other.gameObject.SetActive(false);
            ammo += 10;
        }
        else if (other.gameObject.CompareTag("Scrap"))
        {
            other.gameObject.SetActive(false);
            ++inventory;
            Debug.Log("scrap +1");
        }
        else if (other.gameObject.CompareTag("Gas"))
        {
            carScript.AddGas();
            other.gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            --health;
            healthSlider.value = health;
        }
        else if (other.gameObject.CompareTag("Wall"))
        {
            health = 0;
            gameManagerScript.PlayerDeathCount();
            healthSlider.value = health;
            cameraScript.UnTrackPlayer(this.gameObject);
            SceneManager.LoadScene("titleScreen");
            Destroy(this.gameObject);
        }
        else if (other.gameObject.CompareTag("Finish"))
        {
            YouWin();
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Door"))
        {
            //spriteR.enabled = false;
            if (Input.GetKeyDown(KeyCode.RightShift) || Input.GetKeyDown(KeyCode.LeftShift))
            {
                other.transform.parent.GetComponent<Car>().CheckCar();
                cameraScript.UnTrackPlayer(this.gameObject);
                Destroy(this.gameObject);
            }
        }
    }

        void YouWin()
    {
        Debug.Log("You Win");
        SceneManager.LoadScene("titleScreen");
        //winText.text = "You Win";
    }
}