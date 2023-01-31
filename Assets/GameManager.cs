using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public int lives;
    public int score;
    public Text livesText;
    public Text scoreText;


    // Start is called before the first frame update
    void Start()
    { 
    livesText.text = "Lives: " + lives;
    scoreText.text = "Score: " + score; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateLives(int changeInLives){
        lives += changeInLives;

        // Check for no lives lieft and trigger the end of the game

        livesText.text = "Lives: " + lives;
    }

    public void UpdateScore(int points){
        score += points; 
        scoreText.text = "Score: " + score; 

    }
}
