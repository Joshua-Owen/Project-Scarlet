using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AttackState : BaseState
{
    public bool isAttacking;
    Animator anim;
    PlayerInput input;
    float lastClickedTime;
    public int comboCounter;
    public bool canCombo;
    Coroutine comboResetCoroutine;
    public AttackState(PlayerStateMachine playerStateMachine, PlayerController player, PlayerInput input) : base(playerStateMachine, player, input)
    {
    }
    public override void OnEnter()
    {
        base.OnEnter();
        anim = player.GetComponentInChildren<Animator>();
        input = player.GetComponent<PlayerInput>();
        Attack();
        //isAttacking = false;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        Debug.Log($"isAttacking : {isAttacking}");
        Debug.Log($"canCombo: {canCombo}");
       
        if ((sm.HasAttackInput() && !isAttacking) || (sm.HasAttackInput() && canCombo))
        {
            Attack();
        }
        if (!isAttacking){ sm.ChangeState(sm.IdleState);}
        
        // if(input.attackValue){
        //     
        // }
      
    }

    public override void OnExit()
    {
        base.OnExit();
    }

    void Attack()
    {
       
        isAttacking = true;
        canCombo = false;
        anim.CrossFade(player.combo[comboCounter].clip.name, 0.1f);
        if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime == 100f) isAttacking = false;
      
        comboCounter++;
        if (comboCounter >= player.combo.Count) comboCounter = 0;
        
        // Start combo reset timer
        if (comboResetCoroutine != null)
            player.StopCoroutine(comboResetCoroutine);
        comboResetCoroutine = player.StartCoroutine(ComboResetTimer());
    }

    IEnumerator ComboResetTimer()
    {
        yield return new WaitForSeconds(player.ComboEndBuffer);
        
        if (!sm.HasAttackInput())
        {
            comboCounter = 0;
        }
    }
           
        
    


}