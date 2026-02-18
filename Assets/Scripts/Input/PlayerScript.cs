using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    Rigidbody2D body;
    public float jump_strength = 10.0f;
    public float acceleration_speed = 10.0f;
    public float air_acceleration_speed = 4.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        jump = InputSystem.actions.FindAction("Jump");
        move = InputSystem.actions.FindAction("Move");
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 intent = move.ReadValue<Vector2>();

        if (hasJumped)
        {
            if (jump.ReadValue<float>() == 0.0f)
            {
                hasJumped = false;
            }
        }
        else if (onFloor && jump.ReadValue<float>() != 0.0f)
        {
            hasJumped = true;
            body.linearVelocityY += jump_strength;
            onFloor = false;
            body.linearVelocityX += intent.x * jump_strength / 4;
        }

        body.linearVelocity += intent * (onFloor ? acceleration_speed : air_acceleration_speed) * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        onFloor = true;
    }

    bool onFloor = false;
    bool hasJumped = false;
    InputAction jump;
    InputAction move;
}
