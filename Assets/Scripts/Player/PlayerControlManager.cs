using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

// Singleton for player input so can use in any script
public class PlayerControlManager : MonoBehaviour
{
    public PlayerControls playerControls;

    private InputAction aim;
    private InputAction move;
    private InputAction jump;
    private InputAction sprint;
    private InputAction dash;
    private InputAction climb;
    private InputAction interact;
    private InputAction inventory;
    private InputAction primaryAttack;
    private InputAction secondaryAttack;
    private InputAction pause;
    private InputAction moveDown;

    [field: SerializeField] public Vector2 Aim { get; private set; }
    [field: SerializeField] public Vector2 MoveDir { get; private set; }
    [field: SerializeField] public bool IsJumping { get; private set; }
    [field: SerializeField] public bool IsSprinting { get; private set; }
    public bool IsClimbing { get { return climb.phase == InputActionPhase.Performed; } }
    [field: SerializeField] public bool IsInteracting { get; private set; }
    [field: SerializeField] public bool IsOpeningInventory { get; private set; }
    [field: SerializeField] public bool IsUsingPrimaryAttack { get; private set; }
    [field: SerializeField] public bool IsUsingSecondaryAttack { get; private set; }

    // Events
    public event Action<InputAction.CallbackContext> OnDash;
    public event Action<InputAction.CallbackContext> OnClimb;
    public event Action<InputAction.CallbackContext> OnClimbReleased;
    public event Action<InputAction.CallbackContext> OnJumpReleased;
    public event Action<InputAction.CallbackContext> OnPause;
    public event Action<InputAction.CallbackContext> OnMoveDown;

    // Singleton
    public static PlayerControlManager instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            Debug.LogWarning("More than 1 PlayerControlManager. Destroying this. Name: " + name);
            return;
        }

        playerControls = new PlayerControls();

        aim = playerControls.Player.Aim;
        move = playerControls.Player.Move;
        jump = playerControls.Player.Jump;
        dash = playerControls.Player.Dash;
        climb = playerControls.Player.Climb;
        sprint = playerControls.Player.Sprint;
        interact = playerControls.Player.Interact;
        inventory = playerControls.Player.Inventory; 
        primaryAttack= playerControls.Player.PrimaryAttack;
        secondaryAttack= playerControls.Player.SecondaryAttack;
        pause = playerControls.Player.Pause;
        moveDown = playerControls.Player.MoveDown;
    }

    private void OnEnable()
    {
        aim.Enable();
        move.Enable();
        jump.Enable();
        dash.Enable();
        climb.Enable();
        sprint.Enable();
        interact.Enable();
        inventory.Enable();
        primaryAttack.Enable();
        secondaryAttack.Enable();
        pause.Enable();
        moveDown.Enable();

        dash.performed += (InputAction.CallbackContext context) => OnDash?.Invoke(context);
        climb.performed += (InputAction.CallbackContext context) => OnClimb?.Invoke(context);
        climb.canceled += (InputAction.CallbackContext context) => OnClimbReleased?.Invoke(context);
        jump.canceled += (InputAction.CallbackContext context) => OnJumpReleased?.Invoke(context);
        pause.performed += (InputAction.CallbackContext context) => OnPause?.Invoke(context);
        moveDown.performed += (InputAction.CallbackContext context) => OnMoveDown?.Invoke(context);
    }

    private void OnDisable()
    {
        aim.Disable();
        move.Disable();
        jump.Disable();
        dash.Disable();
        climb.Disable();
        sprint.Disable();
        interact.Disable();
        inventory.Disable();
        primaryAttack.Disable();
        secondaryAttack.Disable();
        pause.Disable();
    }

    private void Update()
    {
        Aim = aim.ReadValue<Vector2>();
        MoveDir = move.ReadValue<Vector2>();
        IsJumping = jump.IsPressed(); // Check holding
        IsSprinting = sprint.phase == InputActionPhase.Performed;
        IsInteracting = interact.WasPressedThisFrame();
        IsOpeningInventory = inventory.WasPressedThisFrame();
        IsUsingPrimaryAttack = primaryAttack.WasPressedThisFrame();
        IsUsingSecondaryAttack = secondaryAttack.WasPressedThisFrame();
    }
}
