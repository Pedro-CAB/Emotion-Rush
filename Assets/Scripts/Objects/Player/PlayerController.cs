using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// How far the player can detect objects in front of them.
    /// This is used for detecting doors and other interactive objects.
    /// </summary>
    public float detect_distance;
    
    /// <summary>
    /// Current door being detected by the player.
    /// </summary>
    private DoorController detectedDoor;
    private PlayerModel playerModel;
    private PlayerView playerView;

    void Start()
    {
        playerModel = GetComponent<PlayerModel>();
        playerView = GetComponent<PlayerView>();
    }

    void FixedUpdate()
    {
        handleJoystickInput();
        handleObjectDetection();
        playerView.handleAnimations();
    }

    public MovementJoystick getMovementJoystick()
    {
        return playerModel.getMovementJoystick();
    }

    public Rigidbody2D getRigidbody()
    {
        return playerModel.getRigidbody();
    }

    public float getSpeed()
    {
        return playerModel.getSpeed();
    }

    public void setLinearVelocity(Vector2 velocity)
    {
        playerModel.setLinearVelocity(velocity);
    }

    /// <summary>
    /// Translates joystick input into player movement.
    /// </summary>
    public void handleJoystickInput(){
        MovementJoystick movementJoystick = playerModel.getMovementJoystick();
        Rigidbody2D rb = playerModel.getRigidbody();
        float speed = playerModel.getSpeed();
        if (movementJoystick.joystickVector.y != 0)
        {
            playerModel.setLinearVelocity(new Vector2(movementJoystick.joystickVector.x * speed, movementJoystick.joystickVector.y * speed));
        }

        else
        {
            playerModel.setLinearVelocity(Vector2.zero);
        }
    }


    public PlayerModel.Directions getFacingDirection()
    {
        return playerModel.getFacingDirection();
    }

    /// <summary>
    /// Defines what the player is detecting in front of them, depending on the direction they are facing.
    /// </summary>
    void handleObjectDetection(){
        Vector3 position = transform.position; //Get Player position
        Vector3 direction = playerModel.getJoystickVector(); //Get direction from joystick
        PlayerModel.Directions facingDirection = playerModel.getFacingDirection();
        if (direction == Vector3.zero)
        { //If it's not moving
            if (facingDirection == PlayerModel.Directions.Up)
            {
                direction = Vector3.up;
            }
            else if (facingDirection == PlayerModel.Directions.Down)
            {
                direction = Vector3.down;
            }
            else if (facingDirection == PlayerModel.Directions.Left)
            {
                direction = Vector3.left;
            }
            else if (facingDirection == PlayerModel.Directions.Right)
            {
                direction = Vector3.right;
            }
        }
        else
        {
            detectedDoor = null;
        }
        RaycastHit2D hit = Physics2D.Raycast(position, direction, detect_distance, 1 << 3); //Raycast to detect objects in the direction of the joystick in Layer 3: NPC's

        if (hit)
        {
            detectedDoor = hit.collider.gameObject.GetComponent<DoorController>();
        }
    }

    /// <summary>
    /// Handles player interaction with detected objects.
    /// </summary>
    public void interact()
    {
        if (detectedDoor != null)
        {
            detectedDoor.whenInteracted();
        }
    }
}
