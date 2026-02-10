using UnityEngine;
using TMPro;
//---------------------------------------------------
public class ScoreManager : MonoBehaviour
{
    public int score;
    public TMP_Text scoreText;

    public void AddScore()
    {
        score++;
        scoreText.text = score.ToString();
    }
}
