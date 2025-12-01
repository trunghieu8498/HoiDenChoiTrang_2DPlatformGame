using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class DragItem : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private Vector3 originalPosition;
    public List<RectTransform> correctTargets;  // danh sách target
    private Canvas canvas;

    void Start()
    {
        canvas = GetComponentInParent<Canvas>();
        originalPosition = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        RectTransform hitTarget = null;

        // chỉ check target chưa được kích hoạt
        foreach (var target in correctTargets)
        {
            if (!target.gameObject.activeSelf &&    // target chưa active
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
            // bật target vừa đúng
            hitTarget.gameObject.SetActive(true);

            // loại nó khỏi danh sách để lần sau không còn bị check nữa
            correctTargets.Remove(hitTarget);
            ColorGameManager.Instance.CheckCompletedGame();
        }

        // đưa item về vị trí ban đầu
        transform.position = originalPosition;
    }
}
