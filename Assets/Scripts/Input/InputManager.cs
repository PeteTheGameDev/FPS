using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        this.playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        this.UpdateActions();
        this.UpdateControlSchemeBooleans();
    }

    private void UpdateActions()
    {
        this.moveInput = this.playerInput.actions["Move"].ReadValue<Vector2>();
        this.aimInput = this.playerInput.actions["Aim"].ReadValue<Vector2>();

        this.jumpPressed = this.playerInput.actions["Jump"].WasPressedThisFrame();
        this.jumpHeld = this.playerInput.actions["Jump"].IsPressed();
        this.jumpReleased = this.playerInput.actions["Jump"].WasReleasedThisFrame();

        this.crouchPressed = this.playerInput.actions["Crouch"].WasPressedThisFrame();
        this.crouchHeld = this.playerInput.actions["Crouch"].IsPressed();
        this.crouchReleased = this.playerInput.actions["Crouch"].WasReleasedThisFrame();

        this.sprintPressed = this.playerInput.actions["Sprint"].WasPressedThisFrame();
        this.sprintHeld = this.playerInput.actions["Sprint"].IsPressed();
        this.sprintReleased = this.playerInput.actions["Sprint"].WasReleasedThisFrame();

        this.firePressed = this.playerInput.actions["Fire"].WasPressedThisFrame();
        this.fireHeld = this.playerInput.actions["Fire"].IsPressed();
        this.fireReleased = this.playerInput.actions["Fire"].WasReleasedThisFrame();

        this.adsPressed = this.playerInput.actions["ADS"].WasPressedThisFrame();
        this.adsHeld = this.playerInput.actions["ADS"].IsPressed();
        this.adsReleased = this.playerInput.actions["ADS"].WasReleasedThisFrame();
    }

    private void UpdateControlSchemeBooleans()
    {
        string currentScheme = this.playerInput.currentControlScheme;

        this.isUsingGamepad = currentScheme == "Gamepad";
        this.isUsingKBM = currentScheme == "KBM";
    }

    // Input Actions Asset
    private PlayerInput playerInput;
    public static InputManager instance { get; private set; }

    // Directional Movement
    public Vector2 moveInput { get; private set;}

    // Camera ControlStick
    public Vector2 aimInput { get; private set;}

    // Jump "Spacebar, Playstation X, Xbox A, Switch B"
    public bool jumpPressed { get; private set; }
    public bool jumpHeld{ get; private set; }
    public bool jumpReleased { get; private set; }

    // Crouch "Playstation Circle, Xbox B, Switch A"
    public bool crouchPressed { get; private set; }
    public bool crouchHeld { get; private set; }
    public bool crouchReleased { get; private set; }

    // Sprint Button
    public bool sprintPressed { get; private set; }
    public bool sprintHeld { get; private set ; }
    public bool sprintReleased { get; private set; }

    // Fire Button
    public bool firePressed { get; private set; }
    public bool fireHeld { get; private set; }
    public bool fireReleased { get; private set; }

    // ADS Button
    public bool adsPressed { get; private set; }
    public bool adsHeld { get; private set; }
    public bool adsReleased { get; private set; }

    // Control Scheme Booleans
    public bool isUsingGamepad;
    public bool isUsingKBM;

    /// <summary>
    /// Be sure to not have .normalize at the end of Vector2 reads.
    /// This makes it so that when tilting the control stick, you will not hit full speed instantly
    /// </summary>
}
