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
  
        player.animator.SetFloat("Velocity", Mathf.Abs(input.GetMovementInput().magnitude)/*Mathf.Abs(new Vector3(player.moveDir.x, 0, player.moveDir.z).magnitude)*/);
        
        if (!player.isGrounded)
        {
            stateMachine.ChangeState(stateMachine.FallState);
            return;
        }
      
       
    }
}