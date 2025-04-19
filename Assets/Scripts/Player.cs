using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public MovementJoystick movementJoystick;
    public float speed;

    public float detect_distance;
    private Rigidbody2D rb;
    private Animator animator;

    private InteractiveNPC detectedNPC;
    public DialogueBox dialogueBox;

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
        handleJoystickInput();
        handleObjectDetection();
        handleAnimations(movementJoystick.joystickVector.x, movementJoystick.joystickVector.y);
    }

    //Receiving input from joystick and translating it to player movement
    void handleJoystickInput(){
        if(movementJoystick.joystickVector.y != 0)
        {
            rb.linearVelocity = new Vector2(movementJoystick.joystickVector.x * speed, movementJoystick.joystickVector.y * speed);
        }

        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

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
            detectedNPC = null;
        }
        RaycastHit2D hit = Physics2D.Raycast(position, direction, detect_distance, 1 << 3); //Raycast to detect objects in the direction of the joystick in Layer 3: NPC's

        if (hit)
        {
            detectedNPC = hit.collider.gameObject.GetComponent<InteractiveNPC>();
        }
    }

    // Handle animation parameters for animator
    void handleAnimations(float x, float y){

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

    public void interact(){
        if (detectedNPC != null){
            Debug.Log("Interacting with: " + detectedNPC.name);
            List<string> lines = detectedNPC.whenInteracted();
            dialogueBox.StartLinearDialogue(lines); //Start the dialogue with the lines returned from the NPC
        }
        else{
            Debug.Log("No object detected to interact with.");	
        }
    }
}
