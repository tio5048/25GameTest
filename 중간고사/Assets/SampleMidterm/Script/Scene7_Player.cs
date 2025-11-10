using UnityEngine;

public class Scene7_Player : MonoBehaviour
{
    // 💡 Inspector에서 설정할 변수들
    public float MoveAcceleration = 10f;
    public float MaxMovePower = 5f;
    public float JumpAcceleration = 500f;
    public float MaxJumpPower = 8f;

    private Rigidbody2D rb;
    private Animator anim; // ⭐ Animator 변수
    private bool isGrounded;

    // --- 초기화 ---
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>(); // ⭐ Animator 컴포넌트 가져오기
    }

    // --- 게임 로직 ---
    void Update()
    {
        // 🚀 Space 점프 입력 감지
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                rb.AddForce(Vector2.up * JumpAcceleration);
                isGrounded = false;

                // ⭐ 점프 애니메이션 시작: IsJumping을 True로 설정
                if (anim != null)
                {
                    anim.SetBool("IsJumping", true);
                }
            }
        }
    }

    // --- 물리 연산 ---
    void FixedUpdate()
    {
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

    // --- 충돌 처리 ---
    void OnCollisionEnter2D(Collision2D collision)
    {
        // 바닥 충돌 체크 (접지 확인)
        if (collision.gameObject.CompareTag("Ground"))
        {
            // ⭐ 착지 시 애니메이션 끝내기: IsJumping을 False로 설정
            if (!isGrounded && anim != null)
            {
                anim.SetBool("IsJumping", false);
            }

            isGrounded = true;
        }

        // 빨간 벽 처리 (안 뚫림)
        if (collision.gameObject.CompareTag("RedWall"))
        {
            Debug.Log("빨간 벽과 충돌 (Collision): 뚫을 수 없음");
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