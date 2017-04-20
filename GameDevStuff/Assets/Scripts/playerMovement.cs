using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerMovement : MonoBehaviour
{

    public int playerNumber = 1;                // Determines what player this script belongs to
    public GameObject shot;                     // The shot gameobject that allows the player to shoot
    public Transform shotSpawn;                 // The shot spawn, which is where the shot will spawn from
    public Slider healthSlider;                 // Controller for the players health
    public Text winText;                        // The text that displays "You Win" if the player reaches the end
    public cameraMovement cameraScript;         // The camera script that will keep its eye on the player
    public GameManager gameManagerScript;       // The game manager, which will determine if the game is over for the player
    public Car carScript;                       // The car script that will be called if the player picks up scrap or gas
    public float speed = 10f;                   // Controls the speed of the player
    public int health = 10;                     // Controls the health of the player
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

        if (Input.GetButton(fire) && Time.time > nextFire && ammo > 0)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
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
        else if (other.gameObject.CompareTag("Part"))
        {
            other.gameObject.SetActive(false);
        }
        else if(other.gameObject.CompareTag("Gas"))
        {
            carScript.gas += 10f;
            other.gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            --health;
            healthSlider.value = health;
        }
        else if (other.gameObject.CompareTag("Door"))
        {
            //spriteR.enabled = false;
            other.transform.parent.GetComponent<Car>().CheckCar();
            cameraScript.UnTrackPlayer(this.gameObject);
            Destroy(this.gameObject);
        }
        else if (other.gameObject.CompareTag("Wall"))
        {
            health = 0;
            gameManagerScript.PlayerDeathCount();
            healthSlider.value = health;
            cameraScript.UnTrackPlayer(this.gameObject);
            Destroy(this.gameObject);
        }
        else if (other.gameObject.CompareTag("Finish"))
        {
            YouWin();
        }
    }

    void YouWin()
    {
        Debug.Log("You Win");
        SceneManager.LoadScene("titleScreen");
        //winText.text = "You Win";
    }
}