
public class IdleState : GroundedState
{
    public IdleState(PlayerStateMachine playerStateMachine, PlayerController player, PlayerInput input) : base(playerStateMachine, player, input)
    {
    }
    public override void OnEnter()
    {
        base.OnEnter();
        player.anim.CrossFade("Idle", 0.2f);
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        
        if (sm.HasMoveInput() ) sm.ChangeState(sm.MoveState); return;
        
    }
    public override void OnExit()
    {
        base.OnExit();
    }
}