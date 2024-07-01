using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class PlayerBase : MonoBehaviour
{
    public virtual void Start()
    {
        inputManager = FindObjectOfType<InputManager>();
        rb = GetComponent<Rigidbody>();
        col = GetComponentInChildren<CapsuleCollider>();
        cam = GetComponentInChildren<Camera>();
    }

    public virtual void Update()
    {
        AimControl();
        SprintControl();
    }

    public virtual void FixedUpdate()
    {
        GroundControl();
        GroundCheck();
    }

    private void GroundControl()
    {
        if (!isGrounded)
        {
            // Minimize directional control
            // Allow air control
        }

        // Get camera forward relative
        Vector3 camForward = cam.transform.forward;
        camForward.y = 0f;
        camForward.Normalize();

        // Get camera right relative
        Vector3 camRight = cam.transform.right;
        camRight.y = 0f;
        camRight.Normalize();

        // Calculate relative direction
        Vector3 relativeDirection = (inputManager.moveInput.x * camRight +  inputManager.moveInput.y * camForward);

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
        }
        else
        {
            isGrounded = false;
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
        rb.velocity -= Vector3.up * gravity * Time.fixedDeltaTime;
    }

    private void AimControl()
    {
        if (inputManager.isUsingGamepad)
        {
            schemeSensitivityX = controllerSensitivityX;
            schemeSensitivityY = controllerSensitivityY;
        }
        else if (inputManager.isUsingKBM)
        {
            schemeSensitivityX = mouseAimX;
            schemeSensitivityY = mouseAimY;
        }

        float aimInputX = inputManager.aimInput.x * schemeSensitivityX * Time.deltaTime;
        float aimInputY = inputManager.aimInput.y * schemeSensitivityY * Time.deltaTime;

        aimX += aimInputX;
        aimY -= aimInputY;
        aimY = Mathf.Clamp(aimY, -90f, 90f);

        cam.transform.localRotation = Quaternion.Euler(aimY, 0f, 0f);
        transform.rotation = Quaternion.Euler(0f, aimX, 0f);

        Cursor.lockState = CursorLockMode.Locked;
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

    // Components
    private InputManager inputManager;
    private Rigidbody rb;
    private CapsuleCollider col;
    private Camera cam;

    // Movement
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

    // Camera Aim
    public float controllerSensitivityX;
    public float controllerSensitivityY;
    public float mouseAimX;
    public float mouseAimY;
    private float aimX;
    private float aimY;
    private float schemeSensitivityX;
    private float schemeSensitivityY;
    
}
