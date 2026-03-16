using UnityEngine;

public class AttackState : GroundedState
{
    public AttackData[] combo;
    
    int comboIndex;
    bool queuedNextAttack;

    public AttackState(PlayerStateMachine stateMachine, PlayerController player, PlayerInput input)
        : base(stateMachine, player, input)
    {
    }

    public override void OnEnter()
    {
        combo = player.lightCombo;

        comboIndex = 0;
        queuedNextAttack = false;

        PlayAttack();
    }

    public override void OnUpdate()
    {
        float time = GetNormalizedTime();

        if (stateMachine.HasLightAttackInput())
        {
            queuedNextAttack = true;
        }

        AttackData attack = combo[comboIndex];

        if (queuedNextAttack &&
            time >= attack.comboWindowStart &&
            time <= attack.comboWindowEnd)
        {
            queuedNextAttack = false;
            NextAttack();
        }

        if (time >= 1f)
        {
            stateMachine.ChangeState(stateMachine.IdleState);
        }
    }

    void PlayAttack()
    {
        player.animator.CrossFade(combo[comboIndex].animationName, 0.1f);
    }

    void NextAttack()
    {
        comboIndex++;

        if (comboIndex >= combo.Length)
        {
            stateMachine.ChangeState(stateMachine.IdleState);
            return;
        }

        PlayAttack();
    }

    float GetNormalizedTime()
    {
        AnimatorStateInfo info = player.animator.GetCurrentAnimatorStateInfo(0);
        return info.normalizedTime % 1f;
    }
}