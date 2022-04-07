using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region private variables
    [SerializeField] static GameManager gameManagerInstance;
    [SerializeField] BasketController basketController;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI yourScoreText;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] GameObject gamePausedMenu;
    [SerializeField] GameObject hud;
    public float score = 0f;
    float time = 30f;
    int sceneID = 0;
    #endregion private variables

    #region private methods
    void Awake()
    {
        // set instance if null
        // else destroy (since we need one instance)
        if (gameManagerInstance == null) {
            gameManagerInstance = this;
        }        
        else {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        // set time scale to 1
        // set activation of the menus to false
        Time.timeScale = 1f;
        gameOverMenu.SetActive(false);
        gamePausedMenu.SetActive(false);
        hud.SetActive(true);
        scoreText.text = "SCORE: 0"; 
    }
    void Update()
    {
        // decrease timer by Time.deltaTime
        if (time > 0)
        {
            time -= Time.deltaTime;
        }
        else
        {
            time = 0f;
            GameOver();
        }

        // Change the color of the text from white to red, when timer <= 10
        // change the color of the text from red to white, when timer > 10
        if (time < 11) { text.color = Color.red; }
        else { text.color = Color.white; }

        // Working - test only
        //if (Input.GetKeyDown(KeyCode.F)) { time += 10; }

        DisplayTime(time);

        // Scoring system is three phases:
        // 1- Score = 0, no movements
        // 2- Score between 1 and 4 inclusive, start adding movement
        // 3- Score between 5 and 10 inclusive, add more movement
        // 4- Score higher than 10, add more movement speed and amplitude
        if (score >= 1 && score < 5) {
            basketController.speed = 2f;
            basketController.amplitude = 0.4f;
        }
        if (score >= 5 && score < 11) {
            basketController.speed = 3f;
            basketController.amplitude = 1f;
        }
        if (score > 10) {
            basketController.speed = 4.5f;
            basketController.amplitude = 2.2f;
        }
    }
    void DisplayTime(float t)
    {
        if (t < 0) {
            t = 0f;
        }

        float minute = Mathf.FloorToInt(t / 60);
        float second = Mathf.FloorToInt(t % 60);

        timeText.text = string.Format("TIME: {0:00}:{1:00}", minute, second);
    }
    #endregion private methods

    #region public methods
    public void AddScoreTime()
    {
        // add score
        //score = float.Parse(scoreText.text);
        score += 1;
        scoreText.text = string.Format("SCORE: {0}" , score.ToString());

        // increase time by scoring
        time += 10;
    }
    public void ResumeGame()
    {
        // Resuming the game
        Time.timeScale = 1f;
        gamePausedMenu.SetActive(false);
    }
    public void PauseGame()
    {
        // Pausing the game
        Time.timeScale = 0f;
        gamePausedMenu.SetActive(true);
    }
    public void GameOver()
    {
        // show game over menu
        // set time scale to 0
        gameOverMenu.SetActive(true);
        yourScoreText.text = "YOUR SCORE IS " + score;
        Time.timeScale = 0f;
    }
    public void RestartLevel()
    {
        // get the active scene to restart
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void BackToMenu()
    {
        // Go back to main menu
        SceneManager.LoadScene(sceneID);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    #endregion public methods
}
