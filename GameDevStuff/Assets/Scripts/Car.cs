using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Car : MonoBehaviour {

    public float speed;
    public Text countText;
    public Text winText;
    public Text loseText;
    public GameObject FrontShot;
	public GameObject BackShot;
	public static GameObject Player1;
	public static GameObject Player2;
    public GameObject Player1Prefab;
    public GameObject Player2Prefab;
    public Transform ShotSpawn;
	public Transform ShotSpawn2;
	public Transform PlayerSpawn;
	public Transform PlayerSpawn2;
    //public SpriteRenderer playerRenderer1;
    //public SpriteRenderer playerRenderer2;
    public int health = 10;
    public float fireRate;
	private int playerCountStop;
    private float nextFire;
    private int ammo = 10;
    private Rigidbody2D rb2d;
	private RigidbodyConstraints2D constraints;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        playerCountStop = 0;
    }

    void Update()
    {
        //Checks if the car's health is below 0
        if (health <= 0)
        {
            this.gameObject.SetActive(false);
            GameOver();
        }

        //Fires the shot forward
        if (Input.GetButton ("Fire1") && Time.time > nextFire && ammo > 0)
        {
            nextFire = Time.time + fireRate;
            Instantiate(FrontShot, ShotSpawn.position, ShotSpawn.rotation);
            --ammo;
            SetAmmoText();
        }
        //Fires the shot backwards
		if (Input.GetButton ("Fire3") && Time.time > nextFire && ammo > 0) {
			nextFire = Time.time + fireRate;
			Instantiate (BackShot, ShotSpawn2.position, ShotSpawn2.rotation);
			--ammo;
            SetAmmoText();
		}
        //Destroys the player if Health is below 0
        if (health <= 0)
        {
            Destroy(this.gameObject);
            GameOver();
        }

		if (Input.GetKeyDown(KeyCode.Space) && playerCountStop == 0)
		{
            //Freezes the car and makes the two player appear
            //playerRenderer1.enabled = true;
           // playerRenderer2.enabled = true;
            rb2d.constraints = RigidbodyConstraints2D.FreezePosition;
			Player1 = Instantiate (Player1Prefab, PlayerSpawn.position, PlayerSpawn.rotation);
			Player2 = Instantiate (Player2Prefab, PlayerSpawn2.position, PlayerSpawn2.rotation);
			playerCountStop++;
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
            SetAmmoText();
        }
        /*else if (other.gameObject.CompareTag("Part"))
        {
            other.gameObject.SetActive(false);
        }*/
        else if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.SetActive(false);
            --health;
        }
        else if (other.gameObject.CompareTag("Wall"))
        {
            health = 0;
            this.gameObject.SetActive(false);
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
        //winText.text = "You Win";
    }

    //Displays Game Over when the player dies
    void GameOver()
    {
        Debug.Log("Game Over");
        //loseText.text = "Game Over";
    }

    //Displays the amount of ammo the player has
    void SetAmmoText()
    {
        countText.text = "Ammo: " + ammo.ToString();
    }
}
