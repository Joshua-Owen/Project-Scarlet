using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController character;
    public PlayerInput input;
    public new Transform camera;
    public Transform cameraTarget;
    public float gravity = -9.81f;
    [Header("Movement")]
    public float moveSpeed = 5.0f;
    public float rotationSpeed = 10f;
    [Header("GroundCheck")]
    RaycastHit hit;
    public bool isGrounded;
    public float SphereRadius =.2f;
    public float maxDistance = 1f;
    public LayerMask groundLayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camera = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HandleMoveValue() => new Vector3(input.GetMoveInput().x, 0, input.GetMoveInput().y);

    public void HandlePlayerDirection()
    {
        Vector3 playerDirection = transform.position - new Vector3(camera.position.x, transform.position.y, camera.position.z); 
        cameraTarget.forward = playerDirection.normalized;
    }

    public Vector3 HandlePlayerRotation()
    {
        HandlePlayerDirection();

        Vector3 inputVector = cameraTarget.forward * input.GetMoveInput().y + cameraTarget.right * input.GetMoveInput().x;
        transform.forward = Vector3.Slerp(transform.forward, inputVector.normalized, rotationSpeed * Time.deltaTime);
        
        return inputVector;
    }

    public void HandlePlayerMove(Vector3 inputDirection) => character.Move(inputDirection * moveSpeed * Time.deltaTime);
    
    public void HandlePlayerGravity() => character.Move(Vector3.up * gravity * Time.deltaTime);
    
    public void HandleGroundCheck() => isGrounded = Physics.SphereCast(transform.position, SphereRadius,Vector3.down,out hit,maxDistance,groundLayer);
        
    

    void OnDrawGizmos()
    {
        Gizmos.color = isGrounded ? Color.green : Color.red;
        Vector3 GizmosOrigin = new Vector3(transform.position.x, transform.position.y - maxDistance, transform.position.z);
        Gizmos.DrawWireSphere(GizmosOrigin, SphereRadius);
    }
}
