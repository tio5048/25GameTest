using UnityEngine;

public class Scene7_Destroy : MonoBehaviour
{
    // ?? 이 스크립트는 Destroyer 오브젝트에 부착됩니다.
    // Destroyer 오브젝트의 Collider2D는 반드시 'Is Trigger'가 체크되어 있어야 합니다.

    void OnTriggerEnter2D(Collider2D other)
    {
        // ? 충돌한 오브젝트의 Tag를 확인하여 'Spike' 또는 'Obstacle'인지 검사합니다.
        // 스파이크 프리팹의 Tag가 "Spike"라고 가정합니다.
        if (other.CompareTag("Spike"))
        {
            Debug.Log("Spike가 Destroyer 영역에 닿아 파괴되었습니다.");

            // ? 충돌한 상대방(스파이크)을 파괴합니다.
            Destroy(other.gameObject);
        }
    }

    // 이 스크립트는 플레이어나 다른 중요한 오브젝트를 파괴하는 것을 방지하기 위해
    // 태그를 확인하는 것이 중요합니다.
}