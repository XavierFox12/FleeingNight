using UnityEngine;
using System.Collections;

public class moveBackShot : MonoBehaviour {
	public float speed;
	private int lifespan = 2;
	Rigidbody2D rb;

	void Start () {
		rb = GetComponent<Rigidbody2D>();
		rb.velocity = transform.right * -speed;
	}

	void Update ()
	{
		Destroy(this.gameObject, lifespan);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Enemy"))
		{
            Destroy(other.gameObject);
			Destroy(this.gameObject);
		}
        else if (other.gameObject.CompareTag("Cacti"))
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
        else if (other.gameObject.CompareTag("Breakable"))
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
