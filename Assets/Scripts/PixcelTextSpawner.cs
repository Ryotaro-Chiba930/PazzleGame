using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
//設定項目-------------------------------------------------------------------
public class PixelTextSpawner : MonoBehaviour
{
    [Header("表示する文字")]
    public string text = "ONE MORE BLOCKS";

    [Header("文字プレハブ")]
    public GameObject charPrefab;

    [Header("文字スプライト")]
    public List<CharSprite> charSprites;

    [Header("文字間隔")]
    public float spacing = 1.1f;

    [Header("回転時間（1文字ごと）")]
    public float rotateDuration = 0.4f;

    [Header("次の文字までの待ち時間")]
    public float interval = 0.05f;

    Dictionary<char, Sprite> dict;
    List<Transform> spawnedChars = new List<Transform>();

    [System.Serializable]
//---------------------------------------------------------------------------
    public class CharSprite
    {
        public char character;
        public Sprite sprite;
    }
//---------------------------------------------------------------------------
    void Start()
    {
        dict = new Dictionary<char, Sprite>();
        foreach (var c in charSprites)
        {
            dict[c.character] = c.sprite;
        }

        SpawnText();
        PlayRotateLoop();
    }

    void SpawnText()
    {
        float x = 0;

        foreach (char c in text)
        {
            if (c == ' ')
            {
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

            spawnedChars.Add(obj.transform);

            x += spacing;
        }
    }

    void PlayRotateLoop()
    {
        Sequence seq = DOTween.Sequence();

        foreach (var t in spawnedChars)
        {
            t.localRotation = Quaternion.identity;

            seq.Append(
                t.DOLocalRotate(
                    new Vector3(0, 0, -360f),
                    rotateDuration,
                    RotateMode.FastBeyond360
                ).SetEase(Ease.OutCubic)
            );

            seq.AppendInterval(interval);
        }
        seq.SetLoops(-1);
    }
}
