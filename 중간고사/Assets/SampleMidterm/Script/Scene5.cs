using UnityEngine;

public class Scene5 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            Debug.Log("Player : Floor Ãæµ¹");
            animator.Play("5", -1, 0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
