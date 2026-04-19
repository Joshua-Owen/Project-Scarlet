using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerStateMachine : MonoBehaviour
{
    public PlayerState CurrentState {get; private set;}

    public IdleState IdleState { get; private set; }
    public MoveState MoveState { get; private set; }
    public JumpState JumpState {get; private set; }
    public FallState FallState {get; private set; }
    public RollState RollState {get; private set; }
    public AttackState AttackState {get; private set; }

    // cache the player reference so states can be created
    public PlayerController player { get; private set; }
    public PlayerInput input { get; private set; }




    void Awake()
    {
        // Unity will ensure the PlayerController component exists on the same GameObject
        player = GetComponent<PlayerController>();
        if (player == null)
        {
            Debug.LogError("PlayerStateMachine requires a PlayerController component.");
            return;
        }
        input = GetComponent<PlayerInput>();
        if (input == null)
        {
            Debug.LogError("PlayerStateMachine requires a PlayerInput component.");
            return;
        }
        // CurrentState is null initially, so don't read from it.
        IdleState = new IdleState(this, player, input);
        MoveState = new MoveState(this, player, input);
        JumpState = new JumpState(this, player, input);
        FallState = new FallState(this, player, input);
        RollState = new RollState(this, player, input);
        AttackState = new AttackState(this, player, input, player.lightCombo);
    }


    void Start()
    {
        // start in idle after states have been constructed
        ChangeState(IdleState);
    }
    public void ChangeState(PlayerState nextState)
    {
        CurrentState?.OnExit();
        CurrentState = nextState;
        CurrentState?.OnEnter();
        Debug.Log($"current state is {CurrentState}");
    }

    public void Update()
    {
        CurrentState?.OnUpdate();
    }

    public void FixedUpdate()
    {
        CurrentState?.OnFixedUpdate();
    }

    

    #region "Locomotion"


    public bool HasMovementInput()
    {
        return input.GetMovementInput().magnitude >= 0.01f;
    }

    public bool CanJump()
    {
        return player.isGrounded || player.coyoteTimer > 0f;
    }
    public bool HasJumpInput()
    {
        if (input.jumpPressed)
        {
            input.jumpPressed = false;
            return true;
        }
        return false;
    }


    internal bool HasRollInput()
    {
        if (input.rollPressed)
        {
            input.rollPressed = false;
            return true;
        }

        return false;
    }

    public float rollCooldown = 0.25f;
    private float rollLockedUntil;

    public bool CanRoll()
    {
        return Time.time >= rollLockedUntil;
    }

    public void LockRoll()
    {
        rollLockedUntil = Time.time + rollCooldown;
    }


    #endregion

    #region Attack

    public bool HasLightAttackInput()
    {
        if (input.lightPressed)
        {
            //Debug.Log($"light pressed is {input.lightPressed}");
            input.lightPressed = false;
            return true;
        }
        return false;
    }



    public bool HasSpecialAttackInput()
    {
        if (input.specialPressed)
        {
            //Debug.Log($"special pressed is {input.specialPressed}");
            input.specialPressed = false;
            return true;
        }
        return false;
    }

    public bool HasAnyAttackInput()
    {
        if( HasLightAttackInput() || HasSpecialAttackInput()) return true;
        else return false;
    }


    public void LockAttack()
    {
        
    }
    #endregion
}