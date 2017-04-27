using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Car : MonoBehaviour
{

    public float speed;
    public float gas;
    public Animator animator;
    public Slider healthSlider;
    public cameraMovement cameraScript;
    public Text countText;
    public Text winOrLoseText;
    public GameObject FrontShot;
    public GameObject BackShot;
    public static GameObject Player1;
    public static GameObject Player2;
    public GameObject Player1Prefab;
    public GameObject Player2Prefab;
    public Transform ShotSpawn;
    public Transform ShotSpawn2;
    public Transform ShotSpawn3;
    public Transform ShotSpawn4;
    public Transform PlayerSpawn;
    public Transform PlayerSpawn2;
    //public SpriteRenderer playerRenderer1;
    //public SpriteRenderer playerRenderer2;
    public int health;
    public float fireRate;
    private int playerCountStop;
    private int playersInCar;
    private float nextFire;
    private int ammo = 1000;
    private int tmpAmmo;
    private Rigidbody2D rb2d;
    private RigidbodyConstraints2D constraints;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        winOrLoseText.text = "";
        playerCountStop = 0;
        playersInCar = 2;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && Time.time > nextFire && ammo > 0)
        {
            nextFire = Time.time + fireRate;
            Instantiate(FrontShot, ShotSpawn3.position, ShotSpawn3.rotation);
            --ammo;
            //SetAmmoText();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && Time.time > nextFire && ammo > 0)
        {
            nextFire = Time.time + fireRate;
            Instantiate(FrontShot, ShotSpawn4.position, ShotSpawn4.rotation);
            --ammo;
            //SetAmmoText();
        }
        //Fires the shot forward
        else if (Input.GetKeyDown(KeyCode.RightArrow) && Time.time > nextFire && ammo > 0)
        {
            nextFire = Time.time + fireRate;
            Instantiate(FrontShot, ShotSpawn.position, ShotSpawn.rotation);
            --ammo;
            //SetAmmoText();
        }
        //Fires the shot backwards
        if (Input.GetKeyDown(KeyCode.LeftArrow) && Time.time > nextFire && ammo > 0)
        {
            nextFire = Time.time + fireRate;
            Instantiate(BackShot, ShotSpawn2.position, ShotSpawn2.rotation);
            --ammo;
            //SetAmmoText();
        }
        //Destroys the player if Health is below 0
        /*if (health <= 0)
        {
            Destroy(this.gameObject);
            GameOver();
        }*/

        if ((Input.GetKeyDown(KeyCode.Space) || health <= 0 || gas <= 0) && playerCountStop == 0)
        {
            ExitCar();
        }
        else if (gas > 0 && playerCountStop == 0)
        {
            gas -= Time.deltaTime;
            Debug.Log(gas);
        }
    }

    void FixedUpdate()
    {
        //Moves the car Vertically and Horizontally
        float moveHorizontal = Input.GetAxis("Horizontal");

        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        rb2d.velocity = movement * speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ammo"))
        {
            other.gameObject.SetActive(false);
            ammo += 10;
            //SetAmmoText();
        }
        /*else if (other.gameObject.CompareTag("Part"))
        {
            other.gameObject.SetActive(false);
        }*/
        else if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            --health;
            healthSlider.value = health;
        }
        else if (other.gameObject.CompareTag("Wall"))
        {
            health = 0;
            if (playerCountStop == 0)
            {
                ExitCar();
            }
            healthSlider.value = health;
            SceneManager.LoadScene("titleScreen");
            Destroy(this.gameObject);
        }
        else if (other.gameObject.CompareTag("Finish"))
        {
            YouWin();
        }
    }

    //Displays You Win when the player crosses the Finish Line
    void YouWin()
    {
        Debug.Log("You Win");
        winOrLoseText.text = "You Win";
        SceneManager.LoadScene("titleScreen");
    }

    //Displays Game Over when the player dies
    /*void GameOver()
    {
        Debug.Log("Game Over");
        winOrLoseText.text = "Game Over";
    }*/

    //Displays the amount of ammo the player has
    /* void SetAmmoText()
     {
         countText.text = "Ammo: " + ammo.ToString();
     }*/

    //Checks to see if both the players are in the car
    public void CheckCar()
    {
        ++playersInCar;
        if (playersInCar >= 2)
        {
            Debug.Log("Players are in the Car");
            cameraScript.TrackCar(this.gameObject);
            rb2d.constraints &= ~RigidbodyConstraints2D.FreezeAll;
            rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
            playerCountStop = 0;
            ammo = tmpAmmo;
        }
    }

    public void ExitCar()
    {
        //Freezes the car and makes the two player appear
        //playerRenderer1.enabled = true;
        // playerRenderer2.enabled = true;
        tmpAmmo = ammo;
        ammo = 0;
        Player1 = Instantiate(Player1Prefab, PlayerSpawn.position, PlayerSpawn.rotation);
        Player2 = Instantiate(Player2Prefab, PlayerSpawn2.position, PlayerSpawn2.rotation);
        Player1.GetComponent<playerMovement>().cameraScript = cameraScript;
        Player2.GetComponent<playerMovement>().cameraScript = cameraScript;
        cameraScript.UnTrackCar(this.gameObject);
        cameraScript.TrackPlayer(Player1);
        cameraScript.TrackPlayer(Player2);
        playerCountStop++;
        playersInCar = 0;
        rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public void AddGas()
    {
        gas += 10f;
        Debug.Log(gas);
    }

    public void AddHealth()
    {
        health = health + 5;
        Debug.Log(health);
    }
}
