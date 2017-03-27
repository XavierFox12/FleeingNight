using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarknessMovement : MonoBehaviour {

    public float speed = 0f;
    private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }
}
