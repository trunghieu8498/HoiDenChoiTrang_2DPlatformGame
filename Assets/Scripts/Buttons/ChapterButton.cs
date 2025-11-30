using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChapterButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool isUnlocked = false;
    public Image buttonImage;          // Hình nền của button
    public Color highlightColor = Color.white;
    public float scaleFactor = 1.2f;


    private Vector3 originalScale;
    private Color originalColor;

    private bool isSelected = false;

    void Start()
    {
        originalScale = transform.localScale;
        originalColor = buttonImage.color;
    }

    public void ShowChapterInfor()
    {
        if (isUnlocked)
        {
            isSelected = true;
            // MainMenuManager.Instance.ShowChapterInfoPanel();
        }
    }

    public void SetUnSelected()
    {
        isSelected = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isSelected)
        {
            // Phóng to
            transform.localScale = originalScale * scaleFactor;
            // Đổi màu để sáng lên
            buttonImage.color = highlightColor;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isSelected)
        {
            transform.localScale = originalScale;
            buttonImage.color = originalColor;
        }
    }
}
