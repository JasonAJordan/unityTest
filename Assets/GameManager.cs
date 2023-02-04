using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{

    public int lives;
    public int score;
    public Text livesText;
    public Text scoreText;
    public Text highScoreText; 
    public InputField highScoreInput;
    public bool gameOver; 
    public GameObject gameOverPanel;

    public GameObject loadLevelPanel;
    public int numberOfBricks;
    public bool inPlay;
    public Transform[] levels;

    public int currentLevelIndex = 0;

    public GameObject instantiateTest;

    // Start is called before the first frame update
    void Start()
    { 
        livesText.text = "Lives: " + lives;
        scoreText.text = "Score: " + score; 
        numberOfBricks = GameObject.FindGameObjectsWithTag("brick").Length;
        // Instantiate (instantiateTest, Vector2.zero, Quaternion.identity);
        PlayerPrefs.DeleteAll();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateLives(int changeInLives){
        lives += changeInLives;

        // Check for no lives lieft and trigger the end of the game
        if (lives <= 0 ){
            lives = 0;
            GameOver();
        }

        livesText.text = "Lives: " + lives;
    }

    public void UpdateScore(int points){
        score += points; 
        scoreText.text = "Score: " + score; 

    }

    public void UpdateNumberOfBricks(){
        numberOfBricks --;
        if (numberOfBricks <= 0) {
            if (currentLevelIndex >= levels.Length - 1) {
                GameOver();
            } else {
                loadLevelPanel.SetActive(true);
                loadLevelPanel.GetComponentInChildren<Text>().text = "Level " + (currentLevelIndex + 2);
                gameOver = true;
                Invoke ("LoadLevel", 3f);
            }

        }
    }

    void LoadLevel(){
        currentLevelIndex++;
        Debug.Log(currentLevelIndex);
        Instantiate (levels[currentLevelIndex], Vector2.zero, Quaternion.identity);
        numberOfBricks = GameObject.FindGameObjectsWithTag("brick").Length;
        gameOver = false;
        inPlay = false;
        loadLevelPanel.SetActive(false);
    }

    void GameOver(){
        gameOver = true;
        gameOverPanel.SetActive (true);
        int highScore = PlayerPrefs.GetInt("HIGHSCORE"); 
        if (score > highScore){
            PlayerPrefs.SetInt("HIGHSCORE", score);
            highScoreText.text = "New High Score!" + "\n" + "Enter Your Name Below.";
            highScoreInput.gameObject.SetActive(true);


        } else {
            highScoreText.text = PlayerPrefs.GetString("HIGHSCORENAME") + "'s high Score was " + highScore + "!\nCan you beat it?"; 
        }
    }

    public void NewHighScore(){
        string highScoreName = highScoreInput.text;
        PlayerPrefs.SetString ("HIGHSCORENAME", highScoreName);
        highScoreInput.gameObject.SetActive(false);
        highScoreText.text = "Congratulations " + highScoreName + "\n" + "Your New High Score is " + score;
    }

    public void PlayAgain(){
        SceneManager.LoadScene("SampleScene"); 

    }

    public void Quit(){
        SceneManager.LoadScene("StartMenu");
        // Application.Quit();
        // Debug.Log("Game Quit");
    }
}
