using UnityEngine;
using UnityEngine.SceneManagement;

//設定項目------------------------------------------------------
public class BlockSpawner : MonoBehaviour
{
    [Header("テトリスブロックPrefab（複数）")]
    public GameObject[] blockPrefabs;

    [Header("最大ブロック数")]
    public int maxBlockCount = 7;

    int landedCount = 0;
    bool isWaiting = false;
//--------------------------------------------------------------
    void Start()
    {
        SpawnBlock();
    }
//--------------------------------------------------------------
    void SpawnBlock()
    {
        if (landedCount >= maxBlockCount) return;
        if (blockPrefabs.Length == 0) return;

        int index = Random.Range(0, blockPrefabs.Length);
        GameObject block = Instantiate(
            blockPrefabs[index],
            transform.position,
            Quaternion.identity
        );

        BlockController controller = block.GetComponent<BlockController>();
        controller.SetSpawner(this);

        GameManager.Instance.SetCurrentBlock(controller);
    }

    public void OnBlockLanded()
    {
        if (isWaiting) return;

        landedCount++;
        Debug.Log($"着地数: {landedCount}");

        if (landedCount >= maxBlockCount)
        {
            Debug.Log("最大数に到達");
            return;
        }

        isWaiting = true;
        Invoke(nameof(SpawnNext), 0.5f);
    }

    void SpawnNext()
    {
        isWaiting = false;
        SpawnBlock();
    }
}
