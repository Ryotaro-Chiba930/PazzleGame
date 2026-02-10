using UnityEngine;
using UnityEngine.SceneManagement;

public class HeightLimitTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Block タグのオブジェクトが触れたら
        if (other.CompareTag("Block"))
        {
            Debug.Log("HeightLimit にブロックが触れた！");

            // GameOverScene に遷移
            SceneManager.LoadScene("FinishScene"); 
           
        }
    }
}
