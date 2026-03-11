using UnityEngine;

public class RollState : GroundedState
{
    private float rollDuration = 0.8f;
    private float rollTimer;
    public RollState(PlayerStateMachine stateMachine, PlayerController player, PlayerInput input) : base(stateMachine, player, input)
    {
    }

    public override void OnEnter()
    {

        rollTimer = rollDuration;

        // Lock input
        stateMachine.LockRoll();
        player.animator.applyRootMotion = true;
        player.animator.CrossFade("Roll", 0f);

    }

     public override void OnUpdate()
    {
        rollTimer -= Time.deltaTime;

        // Apply gravity manually if needed
        player.HandleGravity();

        if (rollTimer <= 0f)
        {
            ExitRoll();
        }
    }
    public override void OnExit()
    {
         player.animator.applyRootMotion = false;
    }
    private void ExitRoll()
    {
        if (player.isGrounded)
            stateMachine.ChangeState(stateMachine.IdleState);
        else
            stateMachine.ChangeState(stateMachine.FallState);
    }
}