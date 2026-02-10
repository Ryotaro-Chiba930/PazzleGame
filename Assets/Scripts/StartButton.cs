using UnityEngine;
using UnityEngine.SceneManagement;
//----------------------------------------------------
public class StartButton : MonoBehaviour
{
    public void LoadGameScene()
    {
      Debug.Log("クリックされた");
        SceneManager.LoadScene("GameScene");

    }
}
