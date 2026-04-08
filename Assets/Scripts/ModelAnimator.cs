using NUnit.Framework;
using UnityEngine;

public class ModelAnimator : MonoBehaviour
{
     public PlayerStateMachine stateMachine { get; private set;}
     public PlayerController player;
     public Animator animator;
     public CharacterController character;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stateMachine = GetComponentInParent<PlayerStateMachine>();
        character = GetComponentInParent<CharacterController>();
        player = GetComponentInParent<PlayerController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnAttackFinished()
    {
        if (stateMachine.CurrentState is AttackState attack)
        {
            attack.FinishAttack();
        }
    }


    public void OnComboWindowOpen()
    {
        Debug.Log("is combo window is open");
        if (stateMachine.CurrentState is AttackState attack)
        {
            attack.EnableCombo();
            Debug.Log("is combo is enabled");
        }
    }

    public void OnComboWindowClose()
    {   
        Debug.Log("is combo window is closed");
        if (stateMachine.CurrentState is AttackState attack)
        {
            attack.DisableCombo();
        }
    }

    
    public void OnRollFinished()
    {
        Debug.Log("roll is finished");
        if (player.isGrounded)
        {
            stateMachine.ChangeState(stateMachine.IdleState);
           
        }
        else
        {
            stateMachine.ChangeState(stateMachine.FallState);
            
        }
    }

    public void IFramesActive()
    {
        player.hurtbox.enabled = false;
    }
    public void IFramesInactive()
    {
        player.hurtbox.enabled = true;
    }

    void OnAnimatorMove()
    {
        if (stateMachine.CurrentState is RollState)
        {
            Vector3 delta = animator.deltaPosition;
            character.Move(delta);

            player.transform.rotation *= animator.deltaRotation;
        }
    }

}
