using UnityEngine;

public class Scene7_Player : MonoBehaviour
{
    // 💡 Inspector에서 설정할 변수들
    public float MoveAcceleration = 10f;
    public float MaxMovePower = 5f;
    public float JumpAcceleration = 500f;
    public float MaxJumpPower = 8f;

    private Rigidbody2D rb;
    private Animator anim;
    private bool isGrounded;

    // --- 초기화 ---
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // --- 게임 로직 ---
    void Update()
    {
        // 🚀 Space 점프 입력 감지
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                // ⭐ 점프 애니메이션 시작
                if (anim != null)
                {
                    anim.SetBool("IsJumping", true);
                }

                rb.AddForce(Vector2.up * JumpAcceleration);
                isGrounded = false; // 점프 직후 접지 상태 해제
            }
        }
    }

    // --- 물리 연산 ---
    void FixedUpdate()
    {
        // isGrounded 관리를 OnCollisionEnter2D와 Stay2D에 의존하므로, 여기서는 초기화하지 않습니다.

        float moveInput = Input.GetAxis("Horizontal");

        // 1, 2, 3번 로직 (이동, 속도 제한, 멈춤)
        if (Mathf.Abs(moveInput) > 0.01f)
        {
            rb.AddForce(Vector2.right * moveInput * MoveAcceleration * rb.mass);
        }

        Vector2 currentVelocity = rb.velocity;
        currentVelocity.x = Mathf.Clamp(currentVelocity.x, -MaxMovePower, MaxMovePower);
        currentVelocity.y = Mathf.Clamp(currentVelocity.y, -MaxJumpPower, MaxJumpPower);
        rb.velocity = currentVelocity;

        if (Mathf.Abs(moveInput) < 0.01f && isGrounded)
        {
            float stopForce = -rb.velocity.x * MoveAcceleration * 0.5f;
            rb.AddForce(Vector2.right * stopForce);
        }
    }

    // --- 충돌 처리 1: Enter (isGrounded 관리 및 Spike 감지) ---
    void OnCollisionEnter2D(Collision2D collision)
    {
        // ⭐ 점프 조건 관리: 땅에 닿는 순간 isGrounded를 true로 설정
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        // 🚨 스파이크 충돌 체크 (Enter에서 최초 충돌 시 감지)
        if (collision.gameObject.CompareTag("Spike"))
        {
            Debug.Log("⚠️ 스파이크와 충돌했습니다! ⚠️");
        }

        // 빨간 벽 처리 (안 뚫림)
        if (collision.gameObject.CompareTag("RedWall"))
        {
            Debug.Log("빨간 벽과 충돌 (Collision): 뚫을 수 없음");
        }
    }

    // --- 충돌 처리 2: Stay (애니메이션 종료 제어) ---
    void OnCollisionStay2D(Collision2D collision)
    {
        // ⭐ 착지 애니메이션 종료: 땅에 닿아있는 동안 점프 상태를 해제
        if (collision.gameObject.CompareTag("Ground"))
        {
            // isGrounded는 Enter에서 이미 true가 되었지만,
            // Stay에서 다시 한번 조건을 확인하여 점프 애니메이션을 끕니다.
            if (anim != null && anim.GetBool("IsJumping"))
            {
                anim.SetBool("IsJumping", false);
            }
        }
    }

    // --- Trigger 처리 ---
    void OnTriggerEnter2D(Collider2D other)
    {
        // 녹색 벽 처리 (뚫림)
        if (other.CompareTag("GreenWall"))
        {
            Debug.Log("녹색 벽과 충돌 (Trigger): 뚫고 들어감");
        }
    }
}