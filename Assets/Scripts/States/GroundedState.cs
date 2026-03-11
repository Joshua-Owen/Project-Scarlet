using UnityEngine;

public abstract class GroundedState : PlayerState
{
    protected GroundedState(PlayerStateMachine stateMachine, PlayerController player, PlayerInput input) : base(stateMachine, player, input)
    {
    }

    public override void OnEnter()
    {
        player.animator.CrossFade("Grounded Locomotion", 0.2f);
    }
    public override void OnUpdate()
    {
        player.HandleGravity();
  
        
        if (!player.isGrounded)
        {
            stateMachine.ChangeState(stateMachine.FallState);
            return;
        }
      
       
    }
}