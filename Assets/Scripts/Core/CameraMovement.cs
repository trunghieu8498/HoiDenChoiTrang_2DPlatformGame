using System;
using UnityEngine;
using UnityEngine.Events;

public class CameraMovement : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 5f;
    public float offsetX = 0f;

    [Header("Camera Bounds")]
    public float minX;
    public float maxX;

    [Header("Zoom Focus")]
    public bool isFocusing = false;
    public Vector3 focusPosition;
    public float focusZoomSpeed = 5f;
    public float focusZoomSize = 6.5f;
    private float normalZoomSize;

    private Camera cam;
    private Vector3 defaultPosition = new Vector3(0, 0, -10);
    private float defaultZoom = 10f;

    // ---- Move A → B ----
    private bool isMovingAB = false;
    private Vector3 moveStart;
    private Vector3 moveEnd;
    private float moveDuration;
    private float moveTimer;

    public UnityEvent onPreviewMapCompleted;

    void Start()
    {
        cam = GetComponent<Camera>();
        normalZoomSize = cam.orthographicSize;

        defaultPosition = transform.position;
        defaultZoom = cam.orthographicSize;
    }

    public void SetDefault()
    {
        defaultPosition = transform.position;
        defaultZoom = cam.orthographicSize;
    }

    void LateUpdate()
    {
        if (isMovingAB)
        {
            moveTimer += Time.deltaTime;
            float t = Mathf.Clamp01(moveTimer / moveDuration);
            transform.position = Vector3.Lerp(moveStart, moveEnd, t);

            if (t >= 1f)
            {
                isMovingAB = false;
                onPreviewMapCompleted.Invoke();
            }
            return;
        }

        Vector3 targetPos;
        float targetZoom;

        if (isFocusing)
        {
            targetPos = new Vector3(focusPosition.x, focusPosition.y, transform.position.z);
            targetZoom = focusZoomSize;
        }
        else
        {
            if (player == null) return;
            float targetX = Mathf.Clamp(player.position.x + offsetX, minX, maxX);
            targetPos = new Vector3(targetX, transform.position.y, transform.position.z);
            targetZoom = normalZoomSize;
        }

        transform.position = Vector3.Lerp(transform.position, targetPos, smoothSpeed * Time.deltaTime);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, focusZoomSpeed * Time.deltaTime);
    }

    public void FocusOnPoint(Vector3 point)
    {
        focusPosition = point;
        isFocusing = true;
    }

    public void FocusOnPoint(Vector3 point, float zoomSize)
    {
        focusPosition = point;
        focusZoomSize = zoomSize;
        isFocusing = true;
    }

    public void FollowPlayer(Transform player)
    {
        this.player = player;
        isFocusing = false;
    }

    public void ResetCamera()
    {
        isFocusing = false;
        player = null;
        transform.position = defaultPosition;
        cam.orthographicSize = defaultZoom;
    }

    // ------------ NEW FEATURE ------------
    public void MoveFromTo(Vector3 pointA, Vector3 pointB, float duration)
    {
        isFocusing = false;
        player = null;

        moveStart = new Vector3(pointA.x, pointA.y, transform.position.z);
        moveEnd = new Vector3(pointB.x, pointB.y, transform.position.z);
        moveDuration = duration;
        moveTimer = 0f;
        isMovingAB = true;
    }
}
