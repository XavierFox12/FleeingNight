using UnityEngine;
using System.Collections;

public class playerMovement : MonoBehaviour {

    public int playerNumber = 1;
    public float speed = 12f;
    public int health = 10;
    private string movementVertical;
    private string movementHorizontal;
    private Rigidbody2D rb;
	private SpriteRenderer spriteR;
    private float movementInputHorizontal;
    private float movementInputVertical;
	private int ammo = 10;
	

    void Awake ()
    {
        rb = GetComponent<Rigidbody2D>();
		spriteR = GetComponent<SpriteRenderer> ();
    }

	void Start () {
        movementHorizontal = "Horizontal" + playerNumber;
        movementVertical = "Vertical" + playerNumber;
		rb.constraints = RigidbodyConstraints2D.FreezeRotation;
	}
	
	void Update () {
        movementInputHorizontal = Input.GetAxis(movementHorizontal);
        movementInputVertical = Input.GetAxis(movementVertical);
        if (health < 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        Vector2 movement = new Vector2(movementInputHorizontal, movementInputVertical);

        rb.velocity = movement * speed;
    }

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag ("Ammo")) {
			other.gameObject.SetActive (false);
			ammo += 10;
		} else if (other.gameObject.CompareTag ("Part")) {
			other.gameObject.SetActive (false);
		} else if (other.gameObject.CompareTag ("Enemy")) {
			other.gameObject.SetActive (false);
			--health;
		} else if (other.gameObject.CompareTag ("Door")) {
			spriteR.enabled = false;
		}
        else if (other.gameObject.CompareTag("Wall"))
        {
            health = 0;
            this.gameObject.SetActive(false);
            GameOver();
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over");
    }
}
