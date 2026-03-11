using Unity.VisualScripting;
using UnityEngine;

public class IdleState : LocomotionState
{
    public IdleState(PlayerStateMachine stateMachine, PlayerController player, PlayerInput input) : base(stateMachine, player, input)
    {
    }

    public override void OnEnter()
    {
       base.OnEnter();
    }
    public override void OnUpdate()
    {
        player.character.Move(Vector3.zero * Time.deltaTime);
        base.OnUpdate();
        if(stateMachine.HasMovementInput())
        {
            stateMachine.ChangeState(stateMachine.MoveState);
        }
    }
}