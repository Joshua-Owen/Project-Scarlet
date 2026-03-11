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
    }
}