using UnityEngine;

public class Scene4 : MonoBehaviour
{
    // Inspector에 노출될 오브젝트 이동 속도
    public float speed = 5.0f;

    [Range(0.01f, 2.0f)]
    public float timeSpeed = 1.0f;

    private const float NormalTimeScale = 1.0f;
    private const float SlowTimeScale = 0.2f;

    private Rigidbody rb;

    public enum MovementType { FrameRateDependent, DeltaTimeIndependent, DeltaTimeDependentRigidbody }
    public MovementType movementType;

    // 초기 설정
    void Start()
    {
        // Cube C가 Rigidbody를 가지고 있는지 확인합니다.
        rb = GetComponent<Rigidbody>();
        Time.timeScale = timeSpeed;
        SetColor();
    }

    // 모든 큐브의 이동 및 T 키 입력 처리
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            ToggleTimeScale();
        }

        // 모든 큐브의 이동을 Update에서 처리합니다.
        MoveObject();
    }

    // FixedUpdate는 더 이상 사용하지 않습니다.
    /*
    void FixedUpdate()
    {
        // ...
    }
    */

    // ---------------------- 이동 로직 --------------------------

    // Cube A, B, C의 이동을 모두 처리하는 통합 함수
    private void MoveObject()
    {
        Vector3 moveDirection = Vector3.right;
        float moveDistance = 0f;

        switch (movementType)
        {
            case MovementType.FrameRateDependent:
                // CubeA (빨강): 프레임률 기반 이동 (Time.deltaTime 미사용)
                moveDistance = speed;
                break;

            case MovementType.DeltaTimeIndependent:
                // CubeB (초록): Time.deltaTime 사용 (Time.timeScale 영향 받음)
                moveDistance = speed * Time.deltaTime;
                break;

            case MovementType.DeltaTimeDependentRigidbody:
                // CubeC (파랑): Rigidbody는 유지하되, Update에서 Time.deltaTime으로 이동
                // 이로써 CubeB와 동일하게 Time.timeScale의 영향을 받으며 안정적으로 움직입니다.
                moveDistance = speed * Time.deltaTime;
                break;
        }

        // Transform.Translate를 사용하여 오른쪽으로 이동
        transform.Translate(moveDirection * moveDistance, Space.World);
    }

    // ------------------- 시간 제어 및 시각화 -------------------

    private void ToggleTimeScale()
    {
        if (Mathf.Approximately(Time.timeScale, NormalTimeScale))
        {
            Time.timeScale = SlowTimeScale;
        }
        else
        {
            Time.timeScale = NormalTimeScale;
        }
        Debug.Log("Time.timeScale: " + Time.timeScale);
    }

    private void SetColor()
    {
        Renderer rend = GetComponent<Renderer>();
        if (rend != null)
        {
            switch (movementType)
            {
                case MovementType.FrameRateDependent:
                    rend.material.color = Color.red;
                    break;
                case MovementType.DeltaTimeIndependent:
                    rend.material.color = Color.green;
                    break;
                case MovementType.DeltaTimeDependentRigidbody:
                    rend.material.color = Color.blue;
                    break;
            }
        }
    }
}