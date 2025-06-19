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
