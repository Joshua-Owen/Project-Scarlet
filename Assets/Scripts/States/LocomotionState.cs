using UnityEngine;

public class LocomotionState : GroundedState
{
    public LocomotionState(PlayerStateMachine stateMachine, PlayerController player, PlayerInput input) : base(stateMachine, player, input)
    {
    }
    public override void OnUpdate()
    {
        input.GetJumpInput();
        input.GetRollInput();
        base.OnUpdate();
        player.animator.SetFloat("Velocity", Mathf.Abs(input.GetMovementInput().magnitude)/*Mathf.Abs(new Vector3(player.moveDir.x, 0, player.moveDir.z).magnitude)*/);

        if(stateMachine.CanJump() && stateMachine.HasJumpInput())
        {
            stateMachine.ChangeState(stateMachine.JumpState);
            return;
        }
          if (stateMachine.HasRollInput() && stateMachine.CanRoll())
        {
            stateMachine.ChangeState(stateMachine.RollState);
            return;
        }
        if(stateMachine.HasAnyAttackInput())
        {
            stateMachine.ChangeState(stateMachine.AttackState);
            return;
        }
    }
}