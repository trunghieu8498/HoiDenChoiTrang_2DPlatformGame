using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MapButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string buttonID; // duy nhất cho mỗi map
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
    public ColorGameManager coloringGame;
    public Vector2 startMapPosition;
    public PlayerMovement player;
    public Sprite guideBoard;
    public Vector2 focusPosition;
    public Sprite mapNameBoard;
    public StarPool starPool;

    void Start()
    {
        // Load trạng thái từ PlayerPrefs
        isUnlocked = PlayerPrefs.GetInt(buttonID, isUnlocked ? 1 : 0) == 1;

        img = GetComponent<Image>();
        originalColor = img.color;
        grayColor = Color.gray;

        if (isUnlocked)
        {
            originalScale = transform.localScale + new Vector3(0.2f, 0.2f, 0.2f);
            img.color = originalColor; // giữ màu gốc
        }
        else
        {
            originalScale = transform.localScale;
            img.color = grayColor; // bắt đầu đen trắng
        }

        targetScale = originalScale;
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

            // lưu trạng thái unlock
            PlayerPrefs.SetInt(buttonID, 1);
            PlayerPrefs.Save();

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
        GameManager.Instance.OpenMapSelected(mapToOpen, player, coloringGame, startMapPosition, focusPosition, guideBoard, mapNameBoard, starPool);
        UIManager.Instance.CloseSelectMapScreen();
    }
}
