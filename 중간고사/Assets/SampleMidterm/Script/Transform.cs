using UnityEngine;
using UnityEngine.InputSystem; // 🔹 새 입력 시스템 사용

// 🎮 TransformMover
// Transform.position 을 직접 변경하여 이동.
// ⚠️ 물리엔진 영향 X → Collider 겹침, 중력 무시.
public class TransformMover : MonoBehaviour
{
    public float moveSpeed = 5.0f; // 이동 속도

    void Update()
    {
        // 🔹 새 입력 시스템: Keyboard.current 사용
        float moveX = 0f;
        float moveY = 0f;

        if (Keyboard.current.wKey.isPressed) moveY += 1f;
        if (Keyboard.current.sKey.isPressed) moveY -= 1f;
        if (Keyboard.current.aKey.isPressed) moveX -= 1f;
        if (Keyboard.current.dKey.isPressed) moveX += 1f;

        // 정규화 (대각선 이동 시 속도 일정)
        Vector3 moveDir = new Vector3(moveX, moveY, 0f).normalized;

        // 이동
        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }
}
