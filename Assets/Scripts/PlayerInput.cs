using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerInput : MonoBehaviour
{
    PlayerController player;
    InputAction moveAction;
    InputAction jumpAction;
    InputAction rollAction;
    InputAction lightAction;
    InputAction heavyAction;
    InputAction specialAction;

    public Vector2 moveValue;

    // cached flag that represents whether a jump was triggered this frame
    public bool jumpPressed;
    public bool rollPressed;
    public bool lightPressed;
    public bool specialPressed;


    void Start()
    {
        player = GetComponent<PlayerController>();
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        rollAction = InputSystem.actions.FindAction("Roll");
        lightAction = InputSystem.actions.FindAction("Light Attack");
        specialAction = InputSystem.actions.FindAction("Special Attack");


        // make sure the actions are enabled so they start updating
        moveAction?.Enable();
        jumpAction?.Enable();
        rollAction?.Enable();
        lightAction?.Enable();
        heavyAction?.Enable();
        specialAction?.Enable();
    }

    // we no longer need to poll in Update; jumpPressed will be set by the
    // callback.  The field is still reset when GetJumpInput is called.
    void Update()
    {
        // nothing here for jump; leave empty in case we want to later
        // cache movement or other one-shot inputs.
        //GetJumpInput();
        //GetRollInput();
        GetLightInput();
        GetSpecialInput();
    }

    /// <summary>
    /// Returns whether the jump button was pressed this frame.  The stored
    /// value is cleared after reading so that callers don't accidentally
    /// process the same press multiple times.
    /// </summary>
    public void GetJumpInput()
    {
        if (jumpAction.WasPressedThisFrame())
        {
            if(!jumpPressed)
            {
                jumpPressed = true;
            }
        }
       
    }
    public Vector2 GetMovementInput()
    {
        return moveAction.ReadValue<Vector2>();

    }

    public void GetRollInput()
    {
        if (rollAction.WasPressedThisFrame())
        {
            if(!rollPressed)
            {
                rollPressed = true;
            }
        }
    }

    public void GetLightInput()
    {
        if(lightAction.WasPressedThisFrame()) lightPressed = true;
    }


    public void GetSpecialInput()
    {
        if(specialAction.WasPressedThisFrame()) specialPressed = true;
    }



}