using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
//---------------------------------------------------
public class FinishUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

    void Start()
    {
        int score = GameManager.Instance.GetScore();
        scoreText.text = $"SCORE : {score}";
    }

        public void OnTitleButton()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
