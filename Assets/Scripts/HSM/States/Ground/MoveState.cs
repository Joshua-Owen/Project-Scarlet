public class MoveState : GroundedState
{
    public MoveState(PlayerStateMachine playerStateMachine, PlayerController player, PlayerInput input) : base(playerStateMachine, player, input)
    {
    }

    public override void OnEnter() { base.OnEnter(); player.anim.Play("Run",0);} 
    public override void OnUpdate()
    {
        base.OnUpdate();
       
        player.HandlePlayerMove();

        if (!sm.HasMoveInput()) sm.ChangeState(sm.IdleState); return;
         
    }
    public override void OnExit() { base.OnExit(); }

}