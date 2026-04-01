public class MoveState : LocomotionState
{
    public MoveState(PlayerStateMachine stateMachine, PlayerController player, PlayerInput input) : base(stateMachine, player, input)
    {
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        player.HandleMovement();
        if(!stateMachine.HasMovementInput())
        {

            stateMachine.ChangeState(stateMachine.IdleState);
        }
          if(stateMachine.HasLightAttackInput())
        {
            stateMachine.ChangeState(stateMachine.AttackState);
            return;
        }
    }
}