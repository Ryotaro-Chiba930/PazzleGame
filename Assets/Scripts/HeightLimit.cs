using UnityEngine;
//------------------------------------------------------------------------------
public class HeightLimit : MonoBehaviour
{
    GameOverManager gameOverManager;
    bool isGameOver = false;
//------------------------------------------------------------------------------
    void Awake()
    {
        gameOverManager = FindAnyObjectByType<GameOverManager>();

        if (gameOverManager == null)
        {
            Debug.LogError("GameOverManagerがシーン内に存在しない");
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (isGameOver) return;

        if (col.CompareTag("Block"))
        {
            isGameOver = true;

            Debug.Log("GAME OVER");
  Debug.Log($"触れた: {col.name} / tag={col.tag}");
            gameOverManager?.ShowGameOver();

            //再判定防止用
            GetComponent<Collider2D>().enabled = false;
        }
    }

}
