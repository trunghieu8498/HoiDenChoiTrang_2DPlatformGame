using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Target")]
    public Transform target;

    [Header("Follow Settings")]
    public Vector2 offset = new Vector2(0f, 2f);
    public float smoothTime = 0.2f;

    private Vector3 velocity = Vector3.zero;
    private bool firstFrame = true;

    void Start()
    {
        if (GameManager.Instance != null)
        {
            Vector3 savedCamPos = GameManager.Instance.LoadCamera(transform.position);
            transform.position = new Vector3(savedCamPos.x, savedCamPos.y, transform.position.z);
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 targetPosition = new Vector3(
            target.position.x + offset.x,
            target.position.y + offset.y,
            transform.position.z
        );

        if (firstFrame)
        {
            // lần đầu load, đặt camera ngay lập tức
            transform.position = targetPosition;
            firstFrame = false;
            velocity = Vector3.zero; // reset velocity để SmoothDamp không lướt
        }
        else
        {
            // di chuyển mượt như bình thường
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }
}
