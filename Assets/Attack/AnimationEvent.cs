using UnityEditor.Rendering.Universal;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    public PlayerStateMachine stateMachine;
    public PlayerController player;
    public PlayerInput input;
    public AttackState attack;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        attack = stateMachine.AttackState;
        //attack = new AttackState(stateMachine,player, input); 
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void CanCombo() => attack.canCombo = true;
    public void CanAttack()
    {
        if(attack.isAttacking)
        {
            attack.isAttacking = false;
        }
        
    }
}
