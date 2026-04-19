public class AirbourneState : BaseState
{

    public AirbourneState(PlayerStateMachine playerStateMachine, PlayerController player, PlayerInput input) : base(playerStateMachine, player, input)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        player.HandlePlayerGravity();
        if (player.character.isGrounded) sm.ChangeState(sm.IdleState); return;
    }
    public override void OnExit()
    {
        base.OnExit();
    }
}