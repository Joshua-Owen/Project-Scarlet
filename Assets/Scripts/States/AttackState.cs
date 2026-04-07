using UnityEditor;
using UnityEngine;

public class AttackState : GroundedState
{
    public AttackData[] combo;
    bool canChain;
    bool inputQueued;
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
        canChain = false;
        inputQueued = false;

        PlayAttack();
    }

    public override void OnUpdate()
    {  
        // queue input (don't consume instantly)
        if (stateMachine.HasLightAttackInput())
        {
            Debug.Log("next attack is queued");
            inputQueued = true;
        }
       
    }

    void PlayAttack()
    { 
        Debug.Log("play next attack");
        player.animator.CrossFade(combo[comboIndex].animationName, 0.1f);  
       
    }
    public void EnableCombo()
    {
        canChain = true;

        if (inputQueued)
        {
            inputQueued = false;
            NextAttack();
        }
    }

    public void DisableCombo()
    {
        canChain = false;
        if (inputQueued)
        {
            inputQueued = false;
            FinishAttack();
        }
        //FinishAttack();
    }
    void NextAttack()
    {
        Debug.Log("Play next attack");
        comboIndex++;
        Debug.Log($"comboIndex {comboIndex}");
        if (comboIndex >= combo.Length)
        {
            Debug.Log("combo index is greater or equal to combo length");
            FinishAttack();
            return;
        }
       
        canChain = false;
        PlayAttack();
    }
    public void FinishAttack()
    {
        if (!canChain && !inputQueued)
        {
            stateMachine.ChangeState(stateMachine.IdleState);
        }
    }

}