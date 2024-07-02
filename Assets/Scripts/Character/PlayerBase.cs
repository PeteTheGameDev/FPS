using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;
using UnityEditor.Experimental.GraphView;

public class PlayerBase : MonoBehaviour
{
    public virtual void Start()
    {
        inputManager = FindObjectOfType<InputManager>();
        rb = GetComponent<Rigidbody>();
        col = GetComponentInChildren<CapsuleCollider>();
    }

    public virtual void Update()
    {      
        SprintControl();
    }

    public virtual void FixedUpdate()
    {
        GroundControl();
        GroundCheck();
        JumpControl();
    }

    private void GroundControl()
    {
        if (!isGrounded)
        {
            
        }

        // Get camera forward relative
        Vector3 camForward = orientation.forward;
        camForward.y = 0f;
        camForward.Normalize();

        // Get camera right relative
        Vector3 camRight = orientation.right;
        camRight.y = 0f;
        camRight.Normalize(); 

        // Calculate relative direction
        Vector3 relativeDirection = inputManager.moveInput.x * camRight + inputManager.moveInput.y * camForward;

        // Calculate speed
        currentSpeed = strafeSpeed;
        if (isSprinting && inputManager.moveInput.y > 0) // Allow sprinting for when only moving forward
        {
            currentSpeed *= sprintSpeedMultiplier;
        }

        // Apply velocity
        Vector3 scaledVelocity = relativeDirection * currentSpeed;
        Vector3 velocity = rb.velocity;
        velocity.x = scaledVelocity.x;
        velocity.z = scaledVelocity.z;
        rb.velocity = velocity;
    }

    private void GroundCheck()
    {
        // Improve ground check so that you you detect ground normal
        // check for other layers
        // set up a layer construct to get various
        // Create functionality to smoothly go up stairs
        // Need to find a way so that player will stay connected to ground (staying connected to ground normal)
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, distanceToGround + 0.1f, groundLayer))
        {
            isGrounded = true;
            canJump = true;
        }
        else
        {
            isGrounded = false;
            canJump = false;
        }

        // Activate gravity
        if (!isGrounded)
        {
            GravityControl();
        }
    }

    internal LayerMask CollisionMask
    {
        get
        {
            return LayerMask.GetMask(new string[]
            {
                "Ground",
                // Add other layers player will interact with
            });
        }
    }

    private void GravityControl()
    {
        rb.velocity += Vector3.down * gravity * Time.fixedDeltaTime;
    }

    private void SprintControl()
    {
        if (toggleSprint)
        {
            // Activate sprinting when pressed, only when toggle is on
            if (inputManager.sprintPressed && inputManager.moveInput.y > 0)
            {
                isSprinting = !isSprinting;
            }

            // Disable sprinting if the player stops moving forward
            if (inputManager.moveInput.y == 0)
            {
                isSprinting = false;
            }
        }

        if (alwaysSprint)
        {
            // If alwaysSprint is on, moving forward will always activate sprint
            if (alwaysSprint && inputManager.moveInput.y > 0)
            {
                isSprinting = true;
            }
            // Disable sprint
            else if (alwaysSprint && inputManager.moveInput.y == 0)
            {
                isSprinting = false;
            }
        }

        if (!toggleSprint)
        {
            // If holding sprint, remain true, so long as conditions are met
            if (!toggleSprint && inputManager.sprintHeld && inputManager.moveInput.y > 0)
            {
                isSprinting = true;
            }
            // Disable sprint if there is not input, or if sprint is released
            else if (!toggleSprint && inputManager.moveInput.y == 0 || inputManager.sprintReleased)
            {
                isSprinting = false;
            }
        }
    }

    private void JumpControl()
    {
         
    }

    // Components
    private InputManager inputManager;
    private Rigidbody rb;
    private CapsuleCollider col;
    public Transform orientation;

    [Header("Movement")]
    public float currentSpeed;
    public float strafeSpeed;
    public float sprintSpeedMultiplier;
    public float adsStrafeSpeed;
    
    public bool canMove;
    public bool isSprinting;
    public bool toggleSprint;
    public bool alwaysSprint;

    // Ground Check
    public bool isGrounded;
    private float distanceToGround = 2f;
    public LayerMask groundLayer;

    // Gravity
    private float gravity = 50f;

    [Header("Jump")]
    public float jumpForce;
    public float jumpCooldown;
    public bool canJump;
}
