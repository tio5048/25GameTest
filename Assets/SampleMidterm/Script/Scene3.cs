using UnityEngine;
using UnityEngine.InputSystem; // 💡 Input System 사용을 위해 이 네임스페이스를 추가합니다.

public class Scene3 : MonoBehaviour
{
    // 💡 (1) 인스펙터에 세팅할 변수들
    [Header("Movement Settings")]
    public float MoveAcceleration = 50f; // 이동 가속도
    public float MaxMovePower = 10f;     // 최고 이동 속도
    public float StopDamping = 0.9f;     // 키를 뗐을 때 속도 감쇠율 (1에 가까울수록 느리게 멈춤)

    [Header("Jump Settings")]
    public float JumpAcceleration = 500f; // 점프 가속도 (힘)
    public float MaxJumpPower = 15f;      // 최고 점프 속도 (수직 속도 제한)
    public LayerMask GroundLayer;         // 바닥 레이어 마스크
    public Transform GroundCheck;         // 바닥 체크 위치 트랜스폼 (Inspector에서 할당 필수!)
    public float GroundCheckRadius = 0.2f; // 바닥 체크 반경

    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // 💡 중력 적용 (1.5로 적용)
        rb.gravityScale = 1.5f;
    }

    void Update()
    {
        // 💡 바닥 체크 (점프 가능 여부 확인)
        if (GroundCheck != null)
        {
            isGrounded = Physics2D.OverlapCircle(GroundCheck.position, GroundCheckRadius, GroundLayer);
        }

        // 💡 Space 키로 점프 (Input System의 Key.current.spaceKey.wasPressedThisFrame 사용)
        if (Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
        {
            Jump();
        }
    }

    // --- 요청하신 FixedUpdate 로직을 반영합니다. ---
    void FixedUpdate()
    {
        float moveX = 0f;
        // float moveY = 0f; // 2D 플랫폼 게임에서는 중력과 점프로 Y축 이동을 관리합니다.

        // 💡 (1) 좌우 화살표키로 이동
        if (Keyboard.current.leftArrowKey.isPressed) moveX -= 1f;
        if (Keyboard.current.rightArrowKey.isPressed) moveX += 1f;

        // 이동 벡터 생성 (2D 횡스크롤이므로 Y축은 0으로 고정)
        Vector2 movement = new Vector2(moveX, 0f).normalized;

        // MoveWithAddForce 로직을 여기에 통합하여 사용합니다.
        Move(movement.x);

        // 💡 키를 떼면 서서히 멈추도록 (속도 감쇠)
        if (moveX == 0)
        {
            // 수평 속도에 감쇠율 적용
            rb.linearVelocity = new Vector2(rb.linearVelocity.x * StopDamping, rb.linearVelocity.y);
        }
    }
    // --------------------------------------------------

    private void Move(float moveInput)
    {
        // 💡 이동 가속도 적용 (AddForce)
        // moveInput은 -1 또는 1입니다. (FixedUpdate에서 가져옴)
        if (Mathf.Abs(rb.linearVelocity.x) < MaxMovePower)
        {
            rb.AddForce(new Vector2(moveInput * MoveAcceleration, 0f));
        }
    }

    private void Jump()
    {
        // 기존 수직 속도를 0으로 초기화하고 Impulse 모드로 강한 힘을 가함
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
        rb.AddForce(new Vector2(0f, JumpAcceleration), ForceMode2D.Impulse);

        // 💡 최고 점프 속도 제한
        if (rb.linearVelocity.y > MaxJumpPower)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, MaxJumpPower);
        }
    }

    // 💡 (2) 녹색 벽 (뚫리는 벽) - Trigger를 이용
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("GreenWall"))
        {
            Debug.Log("✅ Player가 녹색 벽(Trigger)에 충돌하여 뚫고 지나갑니다.");
        }
    }

    // 💡 (3) 빨간 벽 (안 뚫리는 벽) - Collision을 이용
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("RedWall"))
        {
            Debug.Log("⛔ Player가 빨간 벽(Collision)에 충돌하여 멈춥니다.");
        }
    }
}