using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public Transform cameraTransform;
    public float parallaxY = 0.5f;
    private Vector3 startPosY;

    void Start()
    {
        startPosY = transform.position;
        
    }

    void LateUpdate()
    {
        float newY = startPosY.y + cameraTransform.position.y * parallaxY;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
