using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /// <summary>
    /// Joystick GUI component for player movement.
    /// </summary>
    public MovementJoystick movementJoystick;
    /// <summary>
    /// Base Movement speed of the player.
    /// </summary>
    public float speed;
    /// <summary>
    /// Running Upgrade Level of the player.
    /// This is used to increase the player's speed based on the upgrade level.
    /// </summary>
    int runningUpgradeLevel;

    /// <summary>
    /// How far the player can detect objects in front of them.
    /// This is used for detecting doors and other interactive objects.
    /// </summary>
    public float detect_distance;

    /// <summary>
    /// Player's Rigidbody2D component for physics interactions.
    /// </summary>
    private Rigidbody2D rb;

    /// <summary>
    /// Player's Animator component for handling animations.
    /// </summary>
    private Animator animator;

    /// <summary>
    /// Current door being detected by the player.
    /// </summary>
    private Door detectedDoor;

    /// <summary>
    /// Datatype representing the possible directions the player can face.
    /// </summary>
    public enum Directions
    {
        Up, //0
        Down, //1
        Left, //2
        Right //3
    }

    /// <summary>
    /// Initial Direction the player is facing.
    /// </summary>
    Directions facingDirection = Directions.Down;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        runningUpgradeLevel = PlayerPrefs.GetInt("unsavedRunningUpgradeLevel");
        speed = speed * (1+ runningUpgradeLevel * 0.1f); //Speed is multiplied by the upgrade level
    }

    void FixedUpdate()
    {
        handleJoystickInput();
        handleObjectDetection();
        handleAnimations();
    }

    /// <summary>
    /// Translates joystick input into player movement.
    /// </summary>
    void handleJoystickInput(){
        if(movementJoystick.joystickVector.y != 0)
        {
            rb.linearVelocity = new Vector2(movementJoystick.joystickVector.x * speed , movementJoystick.joystickVector.y * speed);
        }

        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    /// <summary>
    /// Defines what the player is detecting in front of them, depending on the direction they are facing.
    /// </summary>
    void handleObjectDetection(){
        Vector3 position = transform.position; //Get Player position
        Vector3 direction = movementJoystick.joystickVector; //Get direction from joystick
        if (direction == Vector3.zero){ //If it's not moving
            if (facingDirection == Directions.Up){
                direction = Vector3.up;
            }
            else if (facingDirection == Directions.Down){
                direction = Vector3.down;
            }
            else if (facingDirection == Directions.Left){
                direction = Vector3.left;
            }
            else if (facingDirection == Directions.Right){
                direction = Vector3.right;
            }
        }
        else{
            detectedDoor = null;
        }
        RaycastHit2D hit = Physics2D.Raycast(position, direction, detect_distance, 1 << 3); //Raycast to detect objects in the direction of the joystick in Layer 3: NPC's

        if (hit)
        {
            detectedDoor = hit.collider.gameObject.GetComponent<Door>();
        }
    }

    /// <summary>
    /// Handles player animations based on movement and direction.
    /// </summary>
    void handleAnimations(){
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
