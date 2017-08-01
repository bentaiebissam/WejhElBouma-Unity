using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;

public class GameOverFunctions : MonoBehaviour
{
    int Score;
    int HighScore;
    int Hscore;

    public Text ScoreText;
    public Text HighScoreText;
    // Use this for initialization
    void Start()
    {
        Score = PlayerPrefs.GetInt("Score");
        HighScore = PlayerPrefs.GetInt("HighScore");
        
        if (Score > HighScore)
        {   
            HighScore = Score;
            PlayerPrefs.SetInt("HighScore", Score);
        }

        SetScore();

    }

    // Update is called once per frame
    void Update()
    {
      

    }
    public void restart()
    {
        SceneManager.LoadScene("Main");

    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Main menu");
    }
    public void Quit()
    {
        Application.Quit();

    }
    public void SetScore()

    {
        Score = PlayerPrefs.GetInt("Score");
        ScoreText.text = "Score :" + Score.ToString();
        //HighScoreText.text = "HighScore: " + HighScore.ToString();

    }
    public void PostToLeaderBoard()
    {  
        
        Social.ReportScore(Score, "CgkI69TCipoOEAIQBg", (bool success) => {
            if (success) { Debug.Log("Success"); PlayGamesPlatform.Instance.ShowLeaderboardUI("CgkI69TCipoOEAIQBg"); }
            else Debug.Log("Fail");
        });
    }
}
