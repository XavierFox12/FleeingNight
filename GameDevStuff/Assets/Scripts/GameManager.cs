using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public Text gameOverText;
    public GameObject enemy;
    public Transform enemySpawn1;
    public Transform enemySpawn2;
    public float spawnWait;
    private int playerDeathCount;

    void Start()
    {
        StartCoroutine(SpawnEnemies());
        gameOverText = gameOverText.GetComponent<Text>();
        playerDeathCount = 0;
        gameOverText.text = "";
        //StartCoroutine(SpawnEnemies());
    }

    void Update()
    {

    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            Vector2 spawnPosition1 = new Vector2(enemySpawn1.position.x, Random.Range(-enemySpawn1.position.y, enemySpawn1.position.y));
            Vector2 spawnPosition2 = new Vector2(enemySpawn2.position.x, Random.Range(-enemySpawn2.position.y, enemySpawn2.position.y));
            Quaternion spawnRotation = Quaternion.identity;
            Instantiate(enemy, spawnPosition1, spawnRotation);
            Instantiate(enemy, spawnPosition2, spawnRotation);
            yield return new WaitForSeconds(spawnWait);
        }
    }

    public void PlayerDeathCount()
    {
        ++playerDeathCount;
        if (playerDeathCount >= 2)
        {
            Debug.Log("HII");
            GameOver();
        }
    }

    void GameOver()
    {
        //gameOverText.text = "Game Over";
        Debug.Log("Game Over");
        SceneManager.LoadScene("titleScreen");
    }
}