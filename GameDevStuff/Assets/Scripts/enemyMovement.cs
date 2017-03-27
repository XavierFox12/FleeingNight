using UnityEngine;
using System.Collections;

public class enemyMovement : MonoBehaviour {

	public Transform target1, target2, target3;
	public int moveSpeed;
	public int rotationSpeed;
	private GameObject car;
	private GameObject player1;
	private GameObject player2;

	private Transform myTransform;

	void Awake() {
		myTransform = transform;
	}

	void Start () {
		car = GameObject.FindGameObjectWithTag ("Car");
		player1 = GameObject.FindGameObjectWithTag ("Player");
		player2 = GameObject.FindGameObjectWithTag ("Player");

		target1 = car.transform;
		target2 = player1.transform;
		target3 = player2.transform;
	}

	void Update () {
		Vector3 dir = target1.position - myTransform.position;
		dir.z = 0.0f;
		if (dir != Vector3.zero) {
			myTransform.rotation = Quaternion.Slerp (myTransform.rotation, Quaternion.FromToRotation(Vector3.right, dir), rotationSpeed * Time.deltaTime);
		}

		myTransform.position += (target1.position - myTransform.position).normalized * moveSpeed * Time.deltaTime;
	}
}
