using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawnPoint : MonoBehaviour {

    public GameObject randomGenerationPoint;
    public GameObject topBorder;
    public GameObject bottomBorder;
    private float randomNumber1, randomNumber2;

	void Start () {
        randomNumber1 = 0f;
        randomNumber2 = 0f;
	}
	
	void Update () {
        randomNumber1 = Random.Range(topBorder.transform.position.y, bottomBorder.transform.position.y);
        randomNumber2 = Random.Range(randomGenerationPoint.transform.position.x, randomGenerationPoint.transform.position.x + 100);

        transform.position = new Vector2(randomNumber2, randomNumber1);
	}
}
