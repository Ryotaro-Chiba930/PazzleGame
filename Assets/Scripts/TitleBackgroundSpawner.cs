using UnityEngine;
using DG.Tweening;
//-----------------------------------------------------------------------------------------
public class TitleBackgroundSpawner : MonoBehaviour
{
    //後ほど整理---------------------------------------------------------------------------
    public GameObject[] blockPrefabs;
    public float spawnInterval = 0.5f;
    public float fallDuration = 6f;

    public float minX = -8f;
    public float maxX = 8f;
    public float startY = 6f;
    public float endY = -6f;
//-----------------------------------------------------------------------------------------
    void Start()
    {
        InvokeRepeating(nameof(SpawnBlock), 0f, spawnInterval);
    }

    void SpawnBlock()
    {
        GameObject prefab = blockPrefabs[Random.Range(0, blockPrefabs.Length)];
        Vector3 pos = new Vector3(Random.Range(minX, maxX), startY, 0);

        GameObject block = Instantiate(prefab, pos, Quaternion.identity);

        block.transform
            .DOMoveY(endY, fallDuration)
            .SetEase(Ease.Linear)
            .OnComplete(() => Destroy(block));

            block.transform.DORotate(
    new Vector3(0,0,Random.Range(-180,180)),
    fallDuration,
    RotateMode.FastBeyond360
);
            
    }
}
