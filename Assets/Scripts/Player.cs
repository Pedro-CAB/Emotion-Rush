using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public MovementJoystick movementJoystick;
    public float speed;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    void FixedUpdate()
    {
        if(movementJoystick.joystickVector.y != 0)
        {
            rb.linearVelocity = new Vector2(movementJoystick.joystickVector.x * speed, movementJoystick.joystickVector.y * speed);
        }

        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }
}
