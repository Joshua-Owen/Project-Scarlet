

public abstract class BaseState
{
    protected PlayerStateMachine sm;
    public PlayerController player;
    public PlayerInput input;

    public BaseState(PlayerStateMachine playerStateMachine, PlayerController player, PlayerInput input)
    {
        this.sm = playerStateMachine;
        this.player = player;
        this.input = input;
    }

    protected BaseState()
    {
    }

    public virtual void OnEnter(){}
    public virtual void OnUpdate(){}
    public virtual void OnExit(){}
}
