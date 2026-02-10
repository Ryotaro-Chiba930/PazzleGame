using UnityEngine;
using UnityEngine.SceneManagement;
//----------------------------------------------------------------
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    int score = 0;
    bool finishRequested = false;

    BlockController currentBlock;
    void Awake()
    {
        Instance = this;
    }

    // ブロック生成時
    public void SetCurrentBlock(BlockController block)
    {
        currentBlock = block;
    }

    // ブロック着地時
    public void OnBlockLanded(BlockController block)
    {
        if (block != currentBlock) return;

        currentBlock = null;
        score++;

        if (finishRequested)
        {
            GoFinishScene();
        }
    }


    public void OnFinishButton()
    {
        finishRequested = true;
        if (currentBlock == null)
        {
            GoFinishScene();
        }
    }

    void GoFinishScene()
    {
        SceneManager.LoadScene("FinishScene");
    }

    public int GetScore()
    {
        return score;
    }
}
