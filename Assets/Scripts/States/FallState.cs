using Unity.VisualScripting;

public class FallState : AirbourneState
{
    public FallState(PlayerStateMachine stateMachine, PlayerController player, PlayerInput input) : base(stateMachine, player, input)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        
    }
    public override void OnUpdate()
    {
       base.OnUpdate();
     
        if (player.isGrounded)
        {
            stateMachine.ChangeState(stateMachine.IdleState);
        }
    }
}