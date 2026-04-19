public class MoveState : GroundedState
{
    public MoveState(PlayerStateMachine playerStateMachine, PlayerController player, PlayerInput input) : base(playerStateMachine, player, input)
    {
    }

    public override void OnEnter() { base.OnEnter(); } 
    public override void OnUpdate()
    {
        base.OnUpdate();
       
        player.HandlePlayerMove(player.HandlePlayerRotation());

        if (!sm.HasMoveInput()) sm.ChangeState(sm.IdleState); return;
         
    }
    public override void OnExit() { base.OnExit(); }

}