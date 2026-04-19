public class GroundedState : BaseState
{
    public GroundedState(PlayerStateMachine playerStateMachine, PlayerController player, PlayerInput input) : base(playerStateMachine, player, input)
    {
        
    }
    public override void OnEnter()
    {
       base.OnEnter();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        player.HandleGroundCheck();
        if (!player.isGrounded) sm.ChangeState(sm.AirbourneState);
        
        if (sm.HasJumpInput()) sm.ChangeState(sm.JumpState); return;
        
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}

