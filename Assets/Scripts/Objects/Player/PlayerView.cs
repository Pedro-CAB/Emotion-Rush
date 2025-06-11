using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    private PlayerController playerController;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    /// <summary>
    /// Translates joystick input into player movement.
    /// </summary>
    public void handleJoystickInput(){
        MovementJoystick movementJoystick = playerController.getMovementJoystick();
        Rigidbody2D rb = playerController.getRigidbody();
        float speed = playerController.getSpeed();
        if (movementJoystick.joystickVector.y != 0)
        {
            playerController.setLinearVelocity(new Vector2(movementJoystick.joystickVector.x * speed, movementJoystick.joystickVector.y * speed));
        }

        else
        {
            playerController.setLinearVelocity(Vector2.zero);
        }
    }

    /// <summary>
    /// Handles player animations based on movement and direction.
    /// </summary>
    public void handleAnimations(){
        MovementJoystick movementJoystick = playerController.getMovementJoystick();
        Animator animator = GetComponent<Animator>();
        PlayerModel.Directions facingDirection = playerController.getFacingDirection();
        float x = movementJoystick.joystickVector.x;
        float y = movementJoystick.joystickVector.y;
        //Check if the player is moving
        if(x != 0 || y != 0){
            animator.SetBool("isMoving", true);
        }
        else{
            animator.SetBool("isMoving", false);
        }
        
        //Check the direction the player is facing
        if(Mathf.Abs(x) > Mathf.Abs(y)){
            if (x > 0){
                facingDirection = PlayerModel.Directions.Right;
            }
            else{
                facingDirection = PlayerModel.Directions.Left;
            }
        }
        else if(Mathf.Abs(y) >= Mathf.Abs(x)){
            if (y > 0){
                facingDirection = PlayerModel.Directions.Up;
            }
            else{
                facingDirection = PlayerModel.Directions.Down;
            }
        }

        animator.SetInteger("direction", (int)facingDirection);
    }
}
