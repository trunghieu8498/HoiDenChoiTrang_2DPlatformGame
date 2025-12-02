using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class DragItem : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private Vector3 originalPosition;
    private Canvas canvas;

    [Header("Target Settings")]
    public List<RectTransform> correctTargets;  // danh sách target
    private List<RectTransform> originalTargets; // lưu bản sao để reset

    void Awake()
    {
        // khởi tạo an toàn
        if (correctTargets != null)
        {
            originalTargets = new List<RectTransform>();
            foreach (var t in correctTargets)
            {
                if (t != null)
                    originalTargets.Add(t);
            }
        }
        else
        {
            originalTargets = new List<RectTransform>();
        }
    }

    void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        originalPosition = transform.position;
        ResetTargets(); // lần đầu chơi, reset target
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        RectTransform hitTarget = null;

        if (correctTargets == null) return;

        // tìm target đúng chưa active
        foreach (var target in correctTargets)
        {
            if (target == null) continue;

            if (!target.gameObject.activeSelf &&
                RectTransformUtility.RectangleContainsScreenPoint(
                    target,
                    Input.mousePosition,
                    canvas.worldCamera))
            {
                hitTarget = target;
                break;
            }
        }

        if (hitTarget != null)
        {
            hitTarget.gameObject.SetActive(true);
            ColorGameManager.Instance.CheckCompletedGame();
        }

        transform.position = originalPosition;
    }

    public void ResetTargets()
    {
        if (originalTargets == null) return;

        foreach (var target in originalTargets)
        {
            if (target != null)
                target.gameObject.SetActive(false);
        }

        correctTargets = new List<RectTransform>(originalTargets);
    }
}
