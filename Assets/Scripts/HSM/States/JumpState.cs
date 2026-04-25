using System.Dynamic;
using UnityEngine;

public class JumpState : BaseState
{
    public JumpState(PlayerStateMachine playerStateMachine, PlayerController player, PlayerInput input) : base(playerStateMachine, player, input)
    {
    }

    public override void OnEnter()
    {
        base.OnEnter();
        player.anim.CrossFade("Jump", 0.0f);
    }
    public override void OnUpdate()
    {
        player.HandlePlayerJump();
        base.OnUpdate();
        player.HandlePlayerGravity();
        player.HandleGroundCheck(); 
        if (!player.isGrounded && player.Velocity.y < 0f) sm.ChangeState(sm.AirbourneState); return;
        
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}