using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MapButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool isUnlocked = false;

    public float scaleAmount = 1.05f;
    public float speed = 10f;

    Vector3 originalScale;
    Vector3 targetScale;

    Image img;
    Color originalColor;
    Color grayColor;
    bool fadeToNormal = false;
    float fadeSpeed = 2f;
    public GameObject mapToOpen;
    public GameObject coloringGame;
    public Vector2 startMapPosition;
    public PlayerMovement player;
    public Sprite guideBoard;
    public Vector2 focusPosition;

    void Start()
    {
        if (isUnlocked)
            originalScale = transform.localScale + new Vector3(0.2f, 0.2f, 0.2f);
        else
            originalScale = transform.localScale;
        targetScale = originalScale;

        img = GetComponent<Image>();
        originalColor = img.color;
        grayColor = Color.gray;

        img.color = grayColor; // luôn bắt đầu đen trắng
    }

    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * speed);

        if (fadeToNormal)
            img.color = Color.Lerp(img.color, originalColor, Time.deltaTime * fadeSpeed);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetScale = originalScale * scaleAmount;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetScale = originalScale;
    }

    public void OnMapButtonClicked()
    {
        if (isUnlocked)
        {
            fadeToNormal = true;
            StartCoroutine(WaitForColorFade());
        }
        else
        {
            Debug.Log("This map is locked!");
        }
    }

    IEnumerator WaitForColorFade()
    {
        yield return new WaitForSeconds(1f);
        GameManager.Instance.OpenMapSelected(mapToOpen, player, coloringGame, startMapPosition, focusPosition, guideBoard);
        UIManager.Instance.CloseSelectMapScreen();
    }
}
