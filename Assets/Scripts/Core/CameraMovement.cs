using UnityEngine;

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
    public float focusZoomSize = 6.5f;      // orthographic size khi zoom
    private float normalZoomSize;          // lưu size bình thường

    private Camera cam;
    private Vector3 defaultPosition;
    private float defaultZoom = 10f;


    void Start()
    {
        cam = GetComponent<Camera>();
        normalZoomSize = cam.orthographicSize;

        // Lưu vị trí và zoom mặc định để reset
        defaultPosition = transform.position;
        defaultZoom = cam.orthographicSize;
    }

    void LateUpdate()
    {
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

        // Di chuyển mượt
        transform.position = Vector3.Lerp(transform.position, targetPos, smoothSpeed * Time.deltaTime);
        // Zoom mượt
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, focusZoomSpeed * Time.deltaTime);
    }

    public void FocusOnPoint(Vector3 point)
    {
        focusPosition = point;
        isFocusing = true;
    }

    public void FollowPlayer()
    {
        isFocusing = false;
    }

    public void ResetCamera()
    {
        isFocusing = false;

        // Đặt lại vị trí và zoom
        transform.position = defaultPosition;
        cam.orthographicSize = defaultZoom;
    }
}
