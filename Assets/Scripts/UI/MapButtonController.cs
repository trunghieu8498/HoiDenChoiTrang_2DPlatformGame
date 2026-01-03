using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MapButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Data")]
    public MapData mapData;

    [Header("Animation")]
    private float scaleAmount = 1.1f;
    private float speed = 8f;
    private float fadeSpeed = 2f;

    private Image img;
    private Vector3 originalScale;
    private Vector3 targetScale;
    private bool fadeToNormal;

    private Color originalColor;
    private Color grayColor = Color.gray;


    void Start()
    {
        img = GetComponent<Image>();
        originalColor = img.color;
        bool unlocked = false;
        if (mapData != null)
        {
            unlocked = mapData.IsUnlocked();
        }

        originalScale = transform.localScale;
        if (unlocked)
        {
            originalScale += Vector3.one * 0.2f;
            img.color = originalColor;
        }
        else
        {
            img.color = grayColor;
        }

        targetScale = originalScale;
    }

    void Update()
    {
        transform.localScale =
            Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * speed);

        if (fadeToNormal)
        {
            img.color =
                Color.Lerp(img.color, originalColor, Time.deltaTime * fadeSpeed);
        }
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
        if (!mapData.IsUnlocked())
        {
            Debug.Log("This map is locked!");
            return;
        }

        fadeToNormal = true;
        mapData.Unlock();
        StartCoroutine(OpenMapAfterFade());
    }

    IEnumerator OpenMapAfterFade()
    {
        yield return new WaitForSeconds(1f);

        GameManager.Instance.OpenMapSelected(
            mapData.mapPrefab,
            mapData.player,
            mapData.colorGameManager,
            mapData.startMapPosition,
            mapData.focusPosition,
            mapData.guideBoard,
            mapData.mapNameBoard,
            mapData.starPool
        );

        UIManager.Instance.CloseSelectMapScreen();
    }
}
