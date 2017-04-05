using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public Text gameOverText;
    private int playerDeath;

	void Start () {
        playerDeath = 0;
	}
	
	void Update () {
		
	}

    public void PlayerDeathCount()
    {
        ++playerDeath;
        if (playerDeath >= 2)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        gameOverText.text = "Game Over";
    }
}
