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

    private Material runtimeMat;
    private float grayValue = 1f;
    private bool isUnlocked;

    void OnEnable()
    {
        ColorHandle();
    }

    void ColorHandle()
    {
        isUnlocked = mapData != null && mapData.IsUnlocked();
        img = GetComponent<Image>();

        runtimeMat = Instantiate(img.material);
        img.material = runtimeMat;

        originalScale = transform.localScale;

        isUnlocked = mapData != null && mapData.IsUnlocked();

        if (isUnlocked)
        {
            // Map đã unlock từ trước → HIỂN THỊ MÀU NGAY
            runtimeMat.SetFloat("_GrayAmount", 0f);
            grayValue = 0f;
        }
        else
        {
            // Map chưa unlock → xám
            runtimeMat.SetFloat("_GrayAmount", 1f);
            grayValue = 1f;
        }

        targetScale = originalScale;
    }

    void Update()
    {
        transform.localScale =
            Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime * speed);

        if (fadeToNormal)
        {
            grayValue = Mathf.Lerp(grayValue, 0f, Time.deltaTime * fadeSpeed);
            runtimeMat.SetFloat("_GrayAmount", grayValue);
        }
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isUnlocked) return;
        targetScale = originalScale * scaleAmount;
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        targetScale = originalScale;
    }


    public void OnMapButtonClicked()
    {
        if (!isUnlocked)
        {
            Debug.Log("This map is locked!");
            return;
        }

        // chỉ to lên, KHÔNG đổi màu nữa
        targetScale = originalScale * scaleAmount;

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
