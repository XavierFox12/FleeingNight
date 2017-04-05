using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGeneration : MonoBehaviour {

    public GameObject rock;
    public GameObject oil;
    public GameObject cactus;
    public GameObject scrap;
    public Transform spawnPoint;
    private int generationCount = 0;

	public void FieldGeneration()
    {
        if (generationCount <= 0)
        {
            generationCount++;
            for (int i = 0; i < 10; i++)
            {
                Debug.Log("HIIII");
                Instantiate(rock, spawnPoint.position, spawnPoint.rotation);
                Instantiate(oil, spawnPoint.position, spawnPoint.rotation);
                Instantiate(cactus, spawnPoint.position, spawnPoint.rotation);
                Instantiate(scrap, spawnPoint.position, spawnPoint.rotation);
            }
        }
    }
}
