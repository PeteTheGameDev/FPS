using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private void Awake()
    {
        inputManager = FindObjectOfType<InputManager>();
    }

    private void Update()
    {  
        AimControl();
        MoveCamera();
    }

    private void MoveCamera()
    {
        transform.position = cameraPosition.position;
    }

    private void AimControl()
    {
        // Switch sensitivity depending on current device
        if (inputManager.isUsingGamepad)
        {
            schemeSensitivityX = controllerSensitivityX;
            schemeSensitivityY = controllerSensitivityY;
        }
        else if (inputManager.isUsingKBM)
        {
            schemeSensitivityX = mouseSensitivityX;
            schemeSensitivityY = mouseSensitivityY;
        }

        // Calculate aim input
        float aimInputX = inputManager.aimInput.x * schemeSensitivityX * Time.deltaTime;
        float aimInputY = inputManager.aimInput.y * schemeSensitivityY * Time.deltaTime;

        aimRotationY += aimInputX;
        aimRotationX -= aimInputY;
        aimRotationX = Mathf.Clamp(aimRotationX, -90f, 90f);

        // Rotate camera and orientation of player
        transform.rotation = Quaternion.Euler(aimRotationX, aimRotationY, 0f);
        orientation.rotation = Quaternion.Euler(0, aimRotationY, 0f);

        // Hide mouse cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    [Header("Components")]
    public Transform cameraPosition;
    public Transform orientation;
    private InputManager inputManager;
    
    [Header("Variables")]
    public float controllerSensitivityX;
    public float controllerSensitivityY;
    public float mouseSensitivityX;
    public float mouseSensitivityY;
    private float aimRotationX;
    private float aimRotationY;
    private float schemeSensitivityX;
    private float schemeSensitivityY;
}
