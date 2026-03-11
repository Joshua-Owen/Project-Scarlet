using UnityEngine;

public class AirbourneState : PlayerState
{
    public AirbourneState(PlayerStateMachine stateMachine, PlayerController player, PlayerInput input) : base(stateMachine, player, input)
    {
    }
    public override void OnEnter()
    {
       player.animator.CrossFade("Airbourne Locomotion", 0.2f);
    }
    public override void OnUpdate()
    {
        player.animator.SetFloat("Vert.Velocity", Mathf.Clamp01(player.verticalVelocity));
        player.HandleMovement();
        player.HandleGravity();

       /* if (player.character.isGrounded && stateMachine.HasJumpInput())
        {
            stateMachine.ChangeState(stateMachine.IdleState);
            return;
        }*/

    }
}