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
        Up, //0
        Down, //1
        Left, //2
        Right //3
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

        //Check if the player is moving
        if(x != 0 || y != 0){
            animator.SetFloat("x", (int)x);
            animator.SetFloat("y", (int)y);
            animator.SetBool("isMoving", true);
        }
        else{
            animator.SetBool("isMoving", false);
        }
        
        //Check the direction the player is facing
        if(Mathf.Abs(x) > Mathf.Abs(y)){
            if (x > 0){
                facingDirection = Directions.Right;
            }
            else{
                facingDirection = Directions.Left;
            }
        }
        else if(Mathf.Abs(y) >= Mathf.Abs(x)){
            if (y > 0){
                facingDirection = Directions.Up;
            }
            else{
                facingDirection = Directions.Down;
            }
        }

        animator.SetInteger("direction", (int)facingDirection);
    }
}
