using UnityEngine;

public class Scene7_BackGround : MonoBehaviour
{
    public float scrollSpeed = 0.5f; // 슬라이딩 속도
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        // 시간을 기반으로 X축 Offset을 지속적으로 증가시켜 배경이 슬라이딩되게 함
        float offsetX = Time.time * scrollSpeed;
        rend.material.SetTextureOffset("_MainTex", new Vector2(offsetX, 0));
    }
}