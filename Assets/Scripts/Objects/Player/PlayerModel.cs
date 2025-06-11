using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    /// <summary>
    /// Joystick GUI component for player movement.
    /// </summary>
    public MovementJoystick movementJoystick;
    /// <summary>
    /// Base Movement speed of the player.
    /// </summary>
    private float speed;
    /// <summary>
    /// Running Upgrade Level of the player.
    /// This is used to increase the player's speed based on the upgrade level.
    /// </summary>
    int runningUpgradeLevel;

    /// <summary>
    /// Player's Rigidbody2D component for physics interactions.
    /// </summary>
    private Rigidbody2D rb;

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

    private PlayerController playerController;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        runningUpgradeLevel = PlayerPrefs.GetInt("unsavedRunningUpgradeLevel");
        speed = 5 * (1 + runningUpgradeLevel * 0.1f); //Speed is multiplied by the upgrade level
        playerController = GetComponent<PlayerController>();
    }

    public MovementJoystick getMovementJoystick()
    {
        return movementJoystick;
    }

    public UnityEngine.Vector2 getJoystickVector()
    {
        return movementJoystick.joystickVector;
    }
    public Rigidbody2D getRigidbody()
    {
        return rb;
    }

    public float getSpeed()
    {
        return speed;
    }

    public Directions getFacingDirection()
    {
        return facingDirection;
    }

    public void setLinearVelocity(UnityEngine.Vector2 velocity)
    {
        if (rb != null)
        {
            rb.linearVelocity = velocity;
        }
    }
}
