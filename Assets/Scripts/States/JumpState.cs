using UnityEngine;

public class JumpState : AirbourneState
{
    public JumpState(PlayerStateMachine stateMachine, PlayerController player, PlayerInput input) : base(stateMachine, player, input)
    {
        
    }
    public override void OnEnter()
    {
        base.OnEnter();
        player.verticalVelocity = Mathf.Sqrt(player.jumpForce * -2f * player.gravity);
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
       /* if (player.isGrounded && player.verticalVelocity <= 0f)
        {
            stateMachine.ChangeState(stateMachine.IdleState);
            return;
        }*/
        
        if (player.verticalVelocity <= 0f )
        {
            stateMachine.ChangeState(stateMachine.FallState);
            return;
        }
    }
}