using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool isMovable = false;
    [Header("Movement")]
    public float moveSpeed = 6f;
    public float jumpForce = 12f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask groundLayer;

    Rigidbody2D rb;
    float moveInput;
    bool isGrounded;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        isMovable = false;
    }

    void Update()
    {
        if (!isMovable) return;

        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);

        if (Input.GetButtonDown("Jump") && isGrounded)
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

        Flip();
    }

    void Flip()
    {
        if (moveInput != 0)
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * Mathf.Sign(moveInput); // giữ y, chỉ đổi x
            transform.localScale = scale;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Deadzone"))
        {
            GameManager.Instance.RestartGame();
        }
        if (collision.CompareTag("FinishLine"))
        {
            Debug.Log("Level Completed!");
            isMovable = false;
            GameManager.Instance.FinishJumpGame();
        }
    }
}
