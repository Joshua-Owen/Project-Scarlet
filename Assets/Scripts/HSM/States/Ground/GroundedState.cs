public class GroundedState : BaseState
{
    public GroundedState(PlayerStateMachine playerStateMachine, PlayerController player, PlayerInput input) : base(playerStateMachine, player, input)
    {
        
    }
    public override void OnEnter()
    {
       base.OnEnter();
       player.Velocity.y = 0;
       player.airTime = 0;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        player.HandleGroundCheck();

        if (!player.isGrounded) sm.ChangeState(sm.AirbourneState);
        if (sm.CanJump()) sm.ChangeState(sm.JumpState); 
        if (sm.HasAttackInput()) sm.ChangeState(sm.AttackState); return;
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}

