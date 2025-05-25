using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticNPC : MonoBehaviour
{
    private Animator animator;

    public enum Directions{
        Up, //0
        Down, //1
        Left, //2
        Right //3
    }

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

    // Handle animation parameters for animator
    void handleAnimations(){
        animator.SetInteger("direction", (int)staticFacingDirection);
    }
}
