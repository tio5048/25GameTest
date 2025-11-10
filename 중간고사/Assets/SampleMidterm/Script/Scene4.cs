using UnityEngine;

public class Scene4 : MonoBehaviour
{
    // ... (나머지 변수 및 Start 함수는 동일)

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

        // ? 수정된 부분: Time.timeScale과 Time.deltaTime을 함께 출력
        Debug.Log("TimeScale: " + Time.timeScale + " | DeltaTime: " + Time.deltaTime);

        // 모든 큐브의 이동을 Update에서 처리합니다.
        MoveObject();
    }

    // ... (MoveObject, ToggleTimeScale, SetColor 함수는 동일)

    // ---------------------- 이동 로직 --------------------------

    private void MoveObject()
    {
        Vector3 moveDirection = Vector3.right;
        float moveDistance = 0f;

        switch (movementType)
        {
            case MovementType.FrameRateDependent:
                moveDistance = speed;
                break;

            case MovementType.DeltaTimeIndependent:
                moveDistance = speed * Time.deltaTime;
                break;

            case MovementType.DeltaTimeDependentRigidbody:
                moveDistance = speed * Time.deltaTime;
                break;
        }

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