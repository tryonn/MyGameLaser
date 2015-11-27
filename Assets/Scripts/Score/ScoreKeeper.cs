using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreKeeper : MonoBehaviour
{
    private int score = 0;
    [SerializeField] private Text scoreText;


    private void Start()
    {
        scoreText.text = score.ToString();
        ResetScore();
    }

    public void SetScorePoint(int point)
    {
        score += point;
        scoreText.text = score.ToString();
    }

    public void ResetScore()
    {
        score = 0;
        scoreText.text = score.ToString();
    }

}
