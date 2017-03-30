using UnityEngine;
using System.Collections;

public class enemyMovement : MonoBehaviour {

	public Transform target1, target2;
	public int moveSpeed;
	public int rotationSpeed;
    private int carHealth, playerHealth1;
	private GameObject car;
	private GameObject player1;
    private GameObject player2;
	private Transform myTransform;

	void Awake() {
		myTransform = transform;
	}

	void Start () {
        car = GameObject.FindGameObjectWithTag ("Car");
		player1 = Car.Player1;
        player2 = Car.Player2;
        Debug.Log(car == null);
        Debug.Log(player1 == null);
		target1 = car.transform;
		target2 = player1.transform;
	}

	void Update () {
        carHealth = car.GetComponent<Car>().health;
        if (Car.Player1 != null)
        {
            playerHealth1 = Car.Player1.GetComponent<playerMovement>().health;
        }
        if (carHealth > 0)
        {
            Vector3 dir = target1.position - myTransform.position;
            dir.z = 0.0f;
            if (dir != Vector3.zero)
            {
                myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.FromToRotation(Vector3.right, dir), rotationSpeed * Time.deltaTime);
            }

            myTransform.position += (target1.position - myTransform.position).normalized * moveSpeed * Time.deltaTime;
        }
	}
}
