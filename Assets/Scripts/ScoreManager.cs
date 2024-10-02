
using TMPro;
using UnityEngine;


public class ScoreManager : MonoBehaviour
{
  
    [SerializeField] private TMP_Text scoreText;

    private int saveHighScore = 0;

    private int score;

    public int Score
    {
        get { return score; }
        set
        {
            score = value;

            scoreText.text = $"Pisteet: {score} / {saveHighScore}";
        }
    }



    private void Start()
    {
        saveHighScore = PlayerPrefs.GetInt("HighScore", 0);

        scoreText.text = $"pisteet: {score} / {saveHighScore}";

      
    }

    public void UpdateHighScore(int currentScore)
    {
        saveHighScore = PlayerPrefs.GetInt("HighScore", 0);

        if (currentScore > saveHighScore)
        {
            PlayerPrefs.SetInt("HighScore", currentScore);
            PlayerPrefs.Save();

            scoreText.text = $"Pisteet: {score} / {currentScore}";
        }
    }
}
