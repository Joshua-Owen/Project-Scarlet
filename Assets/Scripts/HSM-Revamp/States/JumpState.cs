public class JumpState : BaseState
{
    public JumpState(PlayerStateMachine playerStateMachine, PlayerController player, PlayerInput input) : base(playerStateMachine, player, input)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (player.character.velocity.y < 0 && !player.isGrounded) sm.ChangeState(sm.AirbourneState); return;
        
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}