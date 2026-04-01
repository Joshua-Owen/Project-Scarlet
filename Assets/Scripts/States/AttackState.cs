using UnityEngine;

public class AttackState : GroundedState
{
    public AttackData[] combo;
    
    int comboIndex;
    bool queuedNextAttack;
    float bufferTimer;
    const float bufferTime = 0.2f;

    public AttackState(PlayerStateMachine stateMachine, PlayerController player, PlayerInput input, AttackData[] combo) : base(stateMachine, player, input)
    {
        this.combo = combo;
    }

    public override void OnEnter()
    {
        comboIndex = 0;
        
        PlayAttack();
    }

    public override void OnUpdate()
    {
        
        
        
    }

    void PlayAttack()
    {
        
        player.animator.CrossFade(combo[comboIndex].animationName, 0.1f);
       
    }

    void NextAttack()
    {
        
    }


}