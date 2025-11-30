using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float moveDistance = 3f;   // khoảng cách di chuyển lên xuống
    public float moveSpeed = 2f;      // tốc độ
    public bool startUp = true;       // true: bắt đầu đi lên, false: bắt đầu đi xuống

    private Vector3 startPos;
    private Vector3 targetPos;

    void Start()
    {
        startPos = transform.position;
        targetPos = startPos + (startUp ? Vector3.up : Vector3.down) * moveDistance;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPos) < 0.01f)
        {
            targetPos = (targetPos == startPos)
                ? startPos + (startUp ? Vector3.up : Vector3.down) * moveDistance
                : startPos;
        }
    }
}
