using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public float restartDelay = 1f;
    public Score scoreController;

    static public string score;

	public void PlayerDead()
    {
        Invoke("Restart", restartDelay);
    }

    void Start()
    {
        if (ServerManager.offline)
        {
            scoreController.enabled = false;
            scoreController.score.fontSize = 25;
            scoreController.score.text = "nessun server trovato, premere ESC per tornare alla home";
            Debug.Log("failed to connect");
            Invoke("ReableScore", 3f);
        }
        else
        {
            
        }
        
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("loadScene");
        }
    }

    void Restart()
    {
        if (!ServerManager.offline)
        {
            score = scoreController.score.text;
            SceneManager.LoadScene("fineRound");
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }    
    }

    void ReableScore()
    {
        scoreController.score.fontSize = 70;
        scoreController.enabled = true;
    }
}
