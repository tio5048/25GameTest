using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class ForceMovement2D : MonoBehaviour
{
    public float Speed = 5f;

    public enum MovementType { AddForce, VelocityDirect, VelocityAddForceEquivalent }
    
    public MovementType currentMovementType = MovementType.AddForce;

    private Rigidbody2D rb2D;

    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        rb2D.gravityScale = 0f; 
        rb2D.linearDamping = 0f;
        rb2D.angularDamping = 0f;
    }

    void FixedUpdate()
    {
        float moveX = 0f;
        float moveY = 0f;
        
        if (Keyboard.current.upArrowKey.isPressed) moveY += 1f;
        if (Keyboard.current.downArrowKey.isPressed) moveY -= 1f;
        if (Keyboard.current.leftArrowKey.isPressed) moveX -= 1f;
        if (Keyboard.current.rightArrowKey.isPressed) moveX += 1f;

        Vector2 movement = new Vector2(moveX, moveY).normalized;

        switch (currentMovementType)
        {
            case MovementType.AddForce:
                MoveWithAddForce(movement);
                break;
            case MovementType.VelocityDirect:
                MoveWithVelocityDirect(movement);
                break;
            case MovementType.VelocityAddForceEquivalent:
                MoveWithVelocityAddForceEquivalent(movement);
                break;
        }
    }

    void MoveWithAddForce(Vector2 direction)
    {
        rb2D.AddForce(direction * Speed * 50f * Time.fixedDeltaTime, ForceMode2D.Force);
        
        if (rb2D.linearVelocity.magnitude > Speed)
        {
            rb2D.linearVelocity = rb2D.linearVelocity.normalized * Speed;
        }
    }


    void MoveWithVelocityDirect(Vector2 direction)
    {
        rb2D.linearVelocity = direction * Speed;
    }


    void MoveWithVelocityAddForceEquivalent(Vector2 direction)
    {
        rb2D.linearVelocity = direction * Speed;
    }
}