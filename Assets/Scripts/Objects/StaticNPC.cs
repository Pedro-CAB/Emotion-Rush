using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticNPC : MonoBehaviour
{
    /// <summary>
    /// NPC's Animator component
    /// </summary>
    private Animator animator;

    /// <summary>
    /// Datatype representing the possible directions the NPC can face.
    /// </summary>
    public enum Directions
    {
        Up, //0
        Down, //1
        Left, //2
        Right //3
    }

    /// <summary>
    /// Represents the direction the NPC is facing in the static scene.
    /// This is used to set the animation state of the NPC.
    /// </summary>
    public Directions staticFacingDirection = Directions.Down;

    

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isStaticScene", true);
    }

    void FixedUpdate()
    {
        handleAnimations();
    }

    /// <summary>
    /// Handles animations based on the NPC's static facing direction.
    /// </summary>
    void handleAnimations(){
        animator.SetInteger("direction", (int)staticFacingDirection);
    }
}
