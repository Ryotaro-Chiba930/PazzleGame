using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartTextSpawner : MonoBehaviour
{
    [Header("表示する文字")]
    public string text = "START";

    [Header("文字プレハブ")]
    public GameObject charPrefab;

    [Header("文字スプライト")]
    public List<CharSprite> charSprites;

    [Header("文字間隔")]
    public float spacing = 1.2f;

    Dictionary<char, Sprite> dict = new();
    List<GameObject> spawnedChars = new();

    [System.Serializable]
    public class CharSprite
    {
        public char character;
        public Sprite sprite;
    }

    void Start()
    {
        BuildDictionary();
        SpawnText();
        AddClickCollider();
    }

    void BuildDictionary()
    {
        dict.Clear();
        foreach (var c in charSprites)
        {
            if (!dict.ContainsKey(c.character))
                dict.Add(c.character, c.sprite);
        }
    }

    void SpawnText()
    {
        float x = 0f;

        foreach (char c in text)
        {
            if (c == ' ') {
                x += spacing;
                continue;
            }

            if (!dict.ContainsKey(c))
            {
                Debug.LogWarning($"Sprite not found for: {c}");
                continue;
            }

            GameObject obj = Instantiate(charPrefab, transform);
            obj.transform.localPosition = new Vector3(x, 0, 0);

            var sr = obj.GetComponent<SpriteRenderer>();
            sr.sprite = dict[c];

            spawnedChars.Add(obj);
            x += spacing;
        }
    }

    // START 全体をまとめてクリックできる当たり判定を作る
    void AddClickCollider()
    {
        if (spawnedChars.Count == 0) return;

        Bounds bounds = new Bounds(
            spawnedChars[0].transform.position,
            Vector3.zero
        );

        foreach (var obj in spawnedChars)
        {
            var sr = obj.GetComponent<SpriteRenderer>();
            bounds.Encapsulate(sr.bounds);
        }

        BoxCollider2D col = gameObject.AddComponent<BoxCollider2D>();
        col.isTrigger = true;
        col.size = bounds.size;
        col.offset = transform.InverseTransformPoint(bounds.center);
    }

    // クリック判定
    void OnMouseDown()
    {
        Debug.Log("START CLICKED");
        SceneManager.LoadScene("GameScene");
    }
}
