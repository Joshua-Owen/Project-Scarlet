using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    InputAction attackAction, moveAction, jumpAction;
    
    public bool attackValue =  false;
    public void Awake()
    {
        attackAction = InputSystem.actions.FindAction("Attack");
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
        
        moveAction?.Enable();
        jumpAction?.Enable();
        attackAction?.Enable();
            
    }

    void Update()
    {
       
                
        
    }
    public bool GetAttackInput() => attackAction.WasPressedThisFrame();
    public Vector2 GetMoveInput() => moveAction.ReadValue<Vector2>();
    
    public bool GetJumpInput() => jumpAction.WasPressedThisFrame();


}