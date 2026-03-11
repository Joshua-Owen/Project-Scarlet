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
