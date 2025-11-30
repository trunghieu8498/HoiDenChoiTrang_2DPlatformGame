using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 12f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;

    [Header("Fall Settings")]
    public float fastFallMultiplier = 3f;

    [Header("Raycast Settings")]
    public int horizontalRayCount = 3;
    public float skinWidth = 0.05f;

    private Rigidbody2D rb;
    private Animator animator;
    private CapsuleCollider2D capsule;
    private PlayerSound playerSound;
    private bool wasGrounded;

    private float moveInput;
    private bool isGrounded;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        capsule = GetComponent<CapsuleCollider2D>();
        playerSound = GetComponent<PlayerSound>();

        rb.gravityScale = 3f;
        rb.freezeRotation = true;
    }

    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        isGrounded = CheckGrounded();

        // ========================
        //        Jump Input
        // ========================
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            // ✔ Bật Jump animation ngay khi nhấn nút
            animator.SetBool("isJumping", true);
        }

        // Fast fall
        if (!isGrounded && rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fastFallMultiplier - 1) * Time.deltaTime;
        }

        // ========================
        //     Animation States
        // ========================
        animator.SetBool("isRunning", moveInput != 0);
        animator.SetBool("isGrounded", isGrounded);

        // Jump animation OFF khi vừa chạm đất
        if (isGrounded)
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", false);
        }
        else
        {
            // Đang đi lên (jump)
            if (rb.velocity.y > 0.1f)
            {
                animator.SetBool("isJumping", true);
                animator.SetBool("isFalling", false);
            }
            // Đang rơi (fall)
            else if (rb.velocity.y < -1f) // bạn chọn mode B
            {
                animator.SetBool("isJumping", false);
                animator.SetBool("isFalling", true);
            }
        }

        // Flip sprite
        if (moveInput != 0)
            transform.localScale = new Vector3(Mathf.Sign(moveInput), 1, 1);

        if (!wasGrounded && isGrounded)
        {
            playerSound.PlayLand();
        }
        wasGrounded = isGrounded;
    }

    void FixedUpdate()
    {
        // Horizontal movement with side collision check
        float targetVelX = moveInput * moveSpeed;

        if (moveInput != 0)
        {
            if (!CheckHorizontalCollision(moveInput))
                rb.velocity = new Vector2(targetVelX, rb.velocity.y);
            else
                rb.velocity = new Vector2(0, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    bool CheckGrounded()
    {
        Collider2D hit = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        return hit != null;
    }

    bool CheckHorizontalCollision(float dir)
    {
        float rayLength = capsule.size.x / 2 + skinWidth;
        Vector2 bottom = (Vector2)transform.position + new Vector2(0, -capsule.size.y / 2 + skinWidth);
        Vector2 top = (Vector2)transform.position + new Vector2(0, capsule.size.y / 2 - skinWidth);

        for (int i = 0; i < horizontalRayCount; i++)
        {
            float t = i / (float)(horizontalRayCount - 1);
            Vector2 origin = Vector2.Lerp(bottom, top, t);
            RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.right * dir, rayLength, groundLayer);
            Debug.DrawRay(origin, Vector2.right * dir * rayLength, Color.red);

            if (hit.collider != null)
                return true;
        }
        return false;
    }
}
