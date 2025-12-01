using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool isMovable = false;

    [Header("Jump Forces (horizontal, vertical)")]
    public float horizontalForce = 10f;        // single horizontal
    public float verticalForce = 16f;          // single vertical
    public float doubleHorizontalForce = 12f;  // double horizontal
    public float doubleVerticalForce = 20f;   // double vertical

    [Header("Double click")]
    public float doubleClickTime = 0.25f;     // cửa sổ double click (giây)
    public bool requireSameSideForDouble = true; // nếu true, 2 click phải cùng bên để tính double

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask groundLayer;

    [Header("Animation")]
    public Animator animator; // gán trong Inspector hoặc tự GetComponent nếu không gán

    Rigidbody2D rb;
    Vector3 originalScale;

    // trạng thái click
    bool isGrounded;
    int pendingClicks = 0;
    float firstClickTime = 0f;
    bool firstClickDirectionRight = true;
    Coroutine clickCoroutine = null;
    bool isClickLocked = false; // lock để tránh spam liên tục (tuỳ biến)

    // animator hashes (tối ưu)
    int isGroundedHash;
    int jumpTriggerHash;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale;

        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        isGroundedHash = Animator.StringToHash("isGrounded");
        jumpTriggerHash = Animator.StringToHash("Jump");
    }

    void Start()
    {
        FreezePlayer();
    }

    void Update()
    {
        if (!isMovable) return;

        GroundCheck();

        // Chỉ nhận click khi đang grounded (theo yêu cầu)
        if (Input.GetMouseButtonDown(0) && isGrounded && !isClickLocked)
        {
            HandleClick();
        }

        // (option) update thêm param khác nếu cần (ví dụ vertical velocity) - hiện tại chỉ dùng isGrounded + Jump trigger
    }

    void HandleClick()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        bool clickRight = mouseWorldPos.x > transform.position.x;


        if (pendingClicks == 0)
        {
            pendingClicks = 1;
            firstClickTime = Time.time;
            firstClickDirectionRight = clickRight;


            if (clickCoroutine != null) StopCoroutine(clickCoroutine);
            clickCoroutine = StartCoroutine(DoubleClickWait(clickRight));
        }
        else if (pendingClicks == 1)
        {
            // đã có 1 click đang chờ
            // nếu requireSameSideForDouble == true thì bắt buộc cùng hướng
            if (!requireSameSideForDouble || clickRight == firstClickDirectionRight)
            {
                // click thứ 2 trong thời gian chờ -> double
                pendingClicks = 2;
                if (clickCoroutine != null)
                {
                    StopCoroutine(clickCoroutine);
                    clickCoroutine = null;
                }
                DoDoubleClickJump(clickRight);
            }
            else
            {
                // click 2 khác hướng: coi như bắt đầu lại - huỷ hành vi trước, bắt 1 click mới
                if (clickCoroutine != null)
                {
                    StopCoroutine(clickCoroutine);
                    clickCoroutine = null;
                }
                // reset và bắt lại như lần đầu
                pendingClicks = 1;
                firstClickTime = Time.time;
                firstClickDirectionRight = clickRight;
                clickCoroutine = StartCoroutine(DoubleClickWait(clickRight));
            }
        }
    }

    void PlayJumpAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("Jump");
        }
        Debug.Log("Play Jump Animation");
    }

    IEnumerator DoubleClickWait(bool clickRight)
    {
        // chờ xem có click thứ 2 không trong cửa sổ doubleClickTime
        float t0 = Time.time;
        while (Time.time - t0 < doubleClickTime)
        {
            yield return null;
        }

        // hết thời gian, không có click thứ 2 -> single click action
        clickCoroutine = null;
        pendingClicks = 0;
        DoSingleClickJump(clickRight);
    }

    void DoSingleClickJump(bool goRight)
    {
        // lock ngắn để tránh nhận input ngay lập tức khi rơi (tuỳ bạn có muốn)
        StartCoroutine(TemporaryClickLock(0.05f));
        StartCoroutine(DelayJumpAnimation());


        Jump(goRight, horizontalForce, verticalForce);
        Rotate(goRight);
    }

    void DoDoubleClickJump(bool goRight)
    {
        // lock hơi lâu hơn để tránh spam double
        StartCoroutine(TemporaryClickLock(0.12f));
        pendingClicks = 0;
        StartCoroutine(DelayJumpAnimation());


        Jump(goRight, doubleHorizontalForce, doubleVerticalForce);
        Rotate(goRight);
    }

    IEnumerator TemporaryClickLock(float duration)
    {
        isClickLocked = true;
        yield return new WaitForSeconds(duration);
        isClickLocked = false;
    }

    void GroundCheck()
    {
        bool prevGrounded = isGrounded;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);

        // cập nhật animator param nếu có

        if (animator != null && prevGrounded != isGrounded)
        {
            animator.SetBool("isGrounded", isGrounded);
        }

        // (tuỳ chọn) nếu vừa chạm đất (landing), có thể reset trigger hoặc play landing fx
        // if (!prevGrounded && isGrounded) { /* landed */ }
    }

    IEnumerator DelayJumpAnimation()
    {
        yield return new WaitForFixedUpdate(); // đợi physics cập nhật
        PlayJumpAnimation();
    }

    void Jump(bool goRight, float hForce, float vForce)
    {
        rb.velocity = Vector2.zero;
        float horizontal = goRight ? hForce : -hForce;
        float vertical = vForce;
        rb.AddForce(new Vector2(horizontal, vertical), ForceMode2D.Impulse);
    }

    void Rotate(bool goRight)
    {
        float sign = goRight ? 1f : -1f;
        transform.localScale = new Vector3(Mathf.Abs(originalScale.x) * sign, originalScale.y, originalScale.z);
    }

    private void OnDisable()
    {
        if (clickCoroutine != null)
        {
            StopCoroutine(clickCoroutine);
            clickCoroutine = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Deadzone"))
        {
            UIManager.Instance.OpenLoseBoard();
            FreezePlayer();
        }

        if (collision.CompareTag("FinishLine"))
        {
            Debug.Log("Level Completed!");
            FreezePlayer();
            GameManager.Instance.FinishJumpGame();
        }
    }

    public void FreezePlayer()
    {
        isMovable = false;
        rb.bodyType = RigidbodyType2D.Static;

        // tuỳ chọn: tắt animator khi freeze (nếu muốn giữ frame hiện tại thì không)
        // if (animator != null) animator.enabled = false;
    }

    public void UnfreezePlayer()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        isMovable = true;

        // nếu đã tắt animator khi freeze, bật lại
        // if (animator != null) animator.enabled = true;
    }
}
