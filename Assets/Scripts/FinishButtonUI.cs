using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
//----------------------------------------------------
public class FinishButtonUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] RectTransform checkIcon;

    Button button;
    Image buttonImage;
//----------------------------------------------------
    void Awake()
    {
        button = GetComponent<Button>();
        buttonImage = GetComponent<Image>();
    }

    void Start()
    {
        checkIcon.gameObject.SetActive(false);
    }
    public void OnClick()
    {
        // 二度押し防止用
        button.interactable = false;
        buttonImage.DOFade(0.6f, 0.2f);

        checkIcon.gameObject.SetActive(true);
        checkIcon.localScale = Vector3.zero;

        checkIcon
            .DOScale(1f, 0.25f)
            .SetEase(Ease.OutBack);
    }
}
