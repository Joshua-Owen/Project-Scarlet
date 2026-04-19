public class IdleState : GroundedState
{
    public IdleState(PlayerStateMachine playerStateMachine, PlayerController player, PlayerInput input) : base(playerStateMachine, player, input)
    {
    }
    public override void OnEnter()
    {
        base.OnEnter();
    }
    public override void OnUpdate()
    {
        base.OnUpdate();

        if (sm.HasMoveInput()) sm.ChangeState(sm.MoveState); return;
        
    }
    public override void OnExit()
    {
        base.OnExit();
    }
}