using UnityEngine;
using System.Collections;

public class enemyMovement : MonoBehaviour {

	public Transform target1, target2, target3;
    public int moveSpeed;
	public int rotationSpeed;
    private int carHealth, player1Health, player2Health;
	private GameObject car;
	private GameObject player1;
    private GameObject player2;
	private Transform myTransform;

	void Awake() {
		myTransform = transform;
	}

	void Start () {
        car = GameObject.FindGameObjectWithTag ("Car");
		//player1 = Car.Player1;
        //player2 = Car.Player2;
        Debug.Log(car == null);
        Debug.Log(player1 == null);
		target1 = car.transform;
		target2 = player1.transform;
        target3 = player2.transform;
	}

	void Update () {
        carHealth = car.GetComponent<Car>().health;
        if (Car.Player1 != null)
        {
            Debug.Log("HIIIIIIII");
            player1Health = Car.Player1.GetComponent<playerMovement>().health;
            Vector3 dir = target2.position - myTransform.position;
            dir.z = 0.0f;
            if (dir != Vector3.zero)
            {
                myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.FromToRotation(Vector3.right, dir), rotationSpeed * Time.deltaTime);
            }

            myTransform.position += (target2.position - myTransform.position).normalized * moveSpeed * Time.deltaTime;
            if (transform.position.x > target2.transform.position.x)
            {
                transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
            }
            else if (transform.position.x < target2.transform.position.x)
            {
                transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            }
        }
        if (Car.Player2 != null)
        {
            player2Health = Car.Player2.GetComponent<playerMovement>().health;
        }

        //The enemy is able to track the car
        if (carHealth > 0)
        {
            Vector3 dir = target1.position - myTransform.position;
            dir.z = 0.0f;
            if (dir != Vector3.zero)
            {
                myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.FromToRotation(Vector3.right, dir), rotationSpeed * Time.deltaTime);
            }

            myTransform.position += (target1.position - myTransform.position).normalized * moveSpeed * Time.deltaTime;
            if (transform.position.x > target1.transform.position.x)
            {
                transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
            }
            else if (transform.position.x < target1.transform.position.x)
            {
                transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            }
        }



        //The enemy is able to track player2
        if (player2Health > 0)
        {
            Vector3 dir = target3.position - myTransform.position;
            dir.z = 0.0f;
            if (dir != Vector3.zero)
            {
                myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.FromToRotation(Vector3.right, dir), rotationSpeed * Time.deltaTime);
            }

            myTransform.position += (target3.position - myTransform.position).normalized * moveSpeed * Time.deltaTime;
            if (transform.position.x > target3.transform.position.x)
            {
                transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
            }
            else if (transform.position.x < target3.transform.position.x)
            {
                transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            }
        }
    }
}
