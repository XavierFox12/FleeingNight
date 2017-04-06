using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerMovement : MonoBehaviour
{

    public int playerNumber = 1;
    public GameObject shot;
    public Transform shotSpawn;
    public Slider healthSlider;
    public Text winText;
    public cameraMovement cameraScript;
    public GameManager gameManagerScript;
    public float speed = 10f;
    public int health = 10;
    private string fire;
    private string movementVertical;
    private string movementHorizontal;
    private Rigidbody2D rb;
    private SpriteRenderer spriteR;
    private float movementInputHorizontal;
    private float movementInputVertical;
    private float nextFire;
    private float fireRate;
    private int ammo = 1000;


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