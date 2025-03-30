using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public MovementJoystick movementJoystick;
    public float speed;
    private Rigidbody2D rb;

    private Animator animator;

    enum Directions{
        Up,
        Down,
        Left,
        Right
    }

    private Directions facingDirection = Directions.Down;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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

        handleAnimations(movementJoystick.joystickVector.x, movementJoystick.joystickVector.y);
    }

    void handleAnimations(float x, float y){
        if(x != 0 || y != 0){
            animator.SetFloat("x", (int)x);
            animator.SetFloat("y", (int)y);
            animator.SetBool("isMoving", true);
        }
        else{
            animator.SetBool("isMoving", false);
        }
    }
}
