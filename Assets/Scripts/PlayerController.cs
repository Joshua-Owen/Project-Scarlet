using Unity.Cinemachine;
using UnityEngine;

//[RequireComponent(typeof(PlayerStateMachine))]
public class PlayerController : MonoBehaviour
{
    public CharacterController character {get; private set;}
    public Animator animator { get; private set;}
    public PlayerStateMachine stateMachine { get; private set;}
    public PlayerInput input { get; private set;}
    public Transform camera;

    public float moveSpeed;
    public float walkSpeed = 2.5f;
    public float runSpeed = 10f;
    public float airResistance = 1.75f;
    public float rotationSpeed = 10f;
     public Vector3 moveDir;
    public float jumpForce = 10f;
    public float coyoteTime = 0.1f;
    public float coyoteTimer;
    public float gravity = -9.81f;
    public float verticalVelocity;

   

    public bool isGrounded;
    RaycastHit hit;
    public LayerMask groundLayer;
    public float sphereRadius;
    public float castDistance;
    
    
    
    public Collider hurtbox;
    public GameObject[] weapons;
    public AttackData[] lightCombo;

    public CinemachineCamera virtualCamera;
    Transform lockedTarget;
    bool isLockedOn;
    public float lockOnRange = 15f;
    public LayerMask enemyLayer;



    public Transform enemyt;
    void Awake()
    {
        character = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        input = GetComponent<PlayerInput>();
        // grab the attached state machine instead of creating a new one
        stateMachine = GetComponent<PlayerStateMachine>();
        camera = Camera.main.transform;
        
   
    }
    void Start()
    {
        foreach (var weapon in weapons)
        {
            weapon.SetActive(false);
        }
    }
    void GroundCheck()
    {
       isGrounded = Physics.SphereCast(transform.position, sphereRadius, Vector3.down, out hit, castDistance, groundLayer);
    }
    void Update()
    {
        GroundCheck();
        input.GetLockOnInput();
        HandleLockOn();

        
    }
    void FixedUpdate()
    {
        if (isGrounded)
        {
            coyoteTimer = coyoteTime;
        }
        else
        {
            coyoteTimer -= Time.deltaTime;
            coyoteTimer = Mathf.Max(coyoteTimer, 0f);
        }
    }
    public virtual void HandleMovement()
    {
        Vector3 horizontalMove;
        Vector2 inputValue = input.GetMovementInput();
        horizontalMove =  new Vector3(inputValue.x, 0f , inputValue.y);
        moveDir = horizontalMove;
        
        if(stateMachine.CurrentState == stateMachine.RollState) return;

        if(stateMachine.CurrentState == stateMachine.JumpState || stateMachine.CurrentState == stateMachine.FallState)
        {
            //lock to currrent speed
            moveSpeed = moveSpeed;
            if (!isLockedOn)
                horizontalMove = CameraRelativeDirection(horizontalMove);
            else
            {
                Vector3 right = camera.right;
                Vector3 forward = Vector3.Cross(Vector3.up, right);
                horizontalMove = right * inputValue.x + forward * inputValue.y;
            }
            HandleRotation();
            //air resistance
            character.Move(horizontalMove * moveSpeed * airResistance * Time.deltaTime);
        }
        else
        {
            moveSpeed = inputValue.magnitude >= 0.5f ? runSpeed : walkSpeed;
            
            if (!isLockedOn)
                horizontalMove = CameraRelativeDirection(horizontalMove);
            else
            {
                Vector3 right = camera.right;
                Vector3 forward = Vector3.Cross(Vector3.up, right);
                horizontalMove = right * inputValue.x + forward * inputValue.y;
            }
            HandleRotation();

            character.Move(horizontalMove * moveSpeed * Time.deltaTime);
        }
    }

    private void HandleRotation()
    {
        if (isLockedOn && lockedTarget != null)
        {
            Vector3 lookDir = lockedTarget.position - transform.position;
            lookDir.y = 0;
            if (lookDir.sqrMagnitude > 0.001f)
            {
                Quaternion targetRot = Quaternion.LookRotation(lookDir);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime);
            }
        }
        else
        {
            if(moveDir.sqrMagnitude < 0.001f) return;
            moveDir = CameraRelativeDirection(moveDir);

            Quaternion targetRot = Quaternion.LookRotation(moveDir);
            transform.rotation = isGrounded ?
                Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * Time.deltaTime)
                : Quaternion.Slerp(transform.rotation, targetRot, rotationSpeed * airResistance * Time.deltaTime);
        }
    }

    private void HandleLockOn()
    {
        if (input.lockOnPressed && !isLockedOn)
        {
            // Toggle on: lock to nearest target
            LockOn(FindLockOnTarget());
            input.lockOnPressed = false; // consume input
        }
        else if (!input.lockOnPressed && isLockedOn)
        {
            // Toggle off: unlock
            Unlock();
        }
        
        if (isLockedOn && lockedTarget == null)
        {
            Unlock();
        }
    }

    private Transform FindLockOnTarget()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, lockOnRange, enemyLayer);
        Transform best = null;
        float bestScore = float.MaxValue;

        foreach (var hit in hits)
        {
            Transform t = hit.transform;
            Vector3 dir = t.position - transform.position;
            float dist = dir.magnitude;
            if (dist >= bestScore) continue;

            float forwardDot = Vector3.Dot(transform.forward, dir.normalized);
            if (forwardDot < 0.2f) continue;

            best = t;
            bestScore = dist;
        }

        return best;
    }
    
    private void LockOn(Transform target)
    {
        lockedTarget = target;
        isLockedOn = target != null;

        if (virtualCamera != null)
        {
            virtualCamera.LookAt = lockedTarget;
            var composer = (CinemachineRotationComposer)virtualCamera.GetCinemachineComponent(CinemachineCore.Stage.Aim);
            if (composer != null)
            {
                composer.TargetOffset = Vector3.up * 1.2f;
            }
        }
    }

    void Unlock()
    {
        isLockedOn = false;
        lockedTarget = null;
        if ( virtualCamera != null)
        {
            virtualCamera.LookAt = transform;
        }
    }
    Vector3 CameraRelativeDirection(Vector3 initialDir)
    {
        // Convert a local input direction into a world-space direction
        // that takes the camera's orientation into account. This allows
        // the player to move relative to the camera (WASD relative to
        // the view) rather than world axes.
        if (this == null || camera == null)
        {
            // fallback to raw direction if we don't have camera info
            return initialDir.normalized;
        }

        // Project camera forward/right onto the XZ plane (ignore vertical)
        Vector3 camForward = camera.forward;
        camForward.y = 0f;
        camForward.Normalize();

        Vector3 camRight = camera.right;
        camRight.y = 0f;
        camRight.Normalize();

        // Combine input axes with camera axes. initialDir.x is horizontal
        // (right/left) and initialDir.z is vertical (forward/back).
        Vector3 worldDir = camForward * initialDir.z + camRight * initialDir.x;
        return worldDir.normalized;
    }

        public void HandleGravity()
    {
        
        if (isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f; // stick to ground
        }

         verticalVelocity += gravity * Time.deltaTime;

        Vector3 verticalMove = Vector3.up * verticalVelocity * Time.deltaTime;
        character.Move(verticalMove);
    }

    
    public void HandleJump()
    {
        verticalVelocity = jumpForce;
        coyoteTimer = 0f;

        //Vector2 inputValue = input.GetMovementInput();
        //player.moveDir = new Vector3(inputValue.x, 0 , inputValue.y);
    }






    void OnDrawGizmos()
    {
        Gizmos.color = isGrounded ? Color.blue : Color.red;
        Vector3 origin = transform.position;
        origin.y -= castDistance;
        Gizmos.DrawWireSphere(origin,sphereRadius);
    }
}