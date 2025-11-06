using UnityEngine;
using UnityEngine.InputSystem; // ❗ 새 Input System 사용을 위해 추가

public class BackgroundSlider : MonoBehaviour
{
    public float scrollSpeed = 0.5f;

    private Material backgroundMaterial;
    private Vector2 offset;

    // 키보드 입력을 처리하는 방식이 완전히 달라집니다.
    // Update() 대신 Input System의 이벤트나 Polling 방식을 사용해야 합니다.

    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            backgroundMaterial = renderer.material;
            offset = backgroundMaterial.mainTextureOffset;
        }
    }

    void Update()
    {
        float dirX = 0f;
        float dirY = 0f;

        // --- 새로운 Input System을 이용한 입력 처리 (Polling 방식 예시) ---
        // 'Keyboard' 클래스를 직접 사용하여 키 상태를 확인합니다.
        Keyboard keyboard = Keyboard.current;
        if (keyboard != null)
        {
            if (keyboard.upArrowKey.isPressed)
            {
                dirY = 1f;
            }
            else if (keyboard.downArrowKey.isPressed)
            {
                dirY = -1f;
            }

            if (keyboard.rightArrowKey.isPressed)
            {
                dirX = 1f;
            }
            else if (keyboard.leftArrowKey.isPressed)
            {
                dirX = -1f;
            }
        }

        // --- Offset 계산 및 적용 (동일) ---
        if (dirX != 0f || dirY != 0f)
        {
            offset.x += dirX * scrollSpeed * Time.deltaTime;
            offset.y += dirY * scrollSpeed * Time.deltaTime;
            backgroundMaterial.mainTextureOffset = offset;
        }
    }
}