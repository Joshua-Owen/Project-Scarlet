
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    public PlayerController player;
    public PlayerInput input;

    public BaseState CurrentState {get; private set;}
    public GroundedState GroundedState {get; private set;}
    public IdleState IdleState  {get; private set;}
    public MoveState MoveState  {get; private set;}
    public JumpState JumpState {get; private set;}
    public AirbourneState AirbourneState {get; private set;}
    public AttackState AttackState {get; private set;}
    void Awake()
    {
        input = GetComponent<PlayerInput>();
        GroundedState = new GroundedState(this, player, input);
        IdleState = new IdleState(this, player, input);
        MoveState = new MoveState(this, player, input);
        JumpState = new JumpState(this, player, input);
        AirbourneState = new AirbourneState(this, player, input);
        AttackState = new AttackState(this, player, input);
    }
    public void Start()
    {
        ChangeState(IdleState);
    }
    public void Update()
    {
        CurrentState?.OnUpdate();
        HasMoveInput();
    }

    public void ChangeState(BaseState nextState)
    {
        if (nextState == CurrentState) return;
        CurrentState?.OnExit();
        CurrentState = nextState;
        CurrentState?.OnEnter();
        Debug.Log($"current state is {CurrentState}");
    }

    //StateChecks
    public bool HasMoveInput() => input.GetMoveInput().magnitude >= 0.001f;
    public bool HasJumpInput() => input.GetJumpInput();
    public bool CanJump() => !player.isJumping && HasJumpInput(); 
    public bool HasAttackInput() => input.GetAttackInput();
}
