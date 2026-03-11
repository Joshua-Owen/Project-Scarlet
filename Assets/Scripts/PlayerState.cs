using UnityEngine;

public abstract class PlayerState
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected PlayerStateMachine stateMachine;
    public PlayerController player;
    public PlayerInput input;    
    public PlayerState(PlayerStateMachine stateMachine,PlayerController player, PlayerInput input)
    {
        this.stateMachine = stateMachine;
        this.player = player;
        this.input = input;
    }



    public virtual void OnEnter(){}
    public virtual void OnUpdate(){}
    public virtual void OnFixedUpdate(){}
    public virtual void OnExit(){}
}
