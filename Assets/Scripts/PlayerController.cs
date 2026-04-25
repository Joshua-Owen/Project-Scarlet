


using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController character;
    public PlayerInput input;
    public Animator anim;
    public new Transform camera;
    public Transform cameraTarget;
    [Header("Movement")]
    public float moveSpeed = 5.0f;
    public float rotationSpeed = 10f;
    [Header("GroundCheck")]
    RaycastHit hit;
    public bool isGrounded;
    public float SphereRadius =.2f;
    public float maxDistance = 1f;
    public LayerMask groundLayer;
    public float airTime = 0;
    [Header("Jump")]
    public bool isJumping;
    public float gravity = -9.81f;
    public float jumpheight;
    public Vector3 Velocity;

    public List<AttackSO> combo;
    public float ComboEndBuffer = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        camera = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
      
        
        
    }

    public Vector3 HandleMoveValue() => new Vector3(input.GetMoveInput().x, 0, input.GetMoveInput().y);



    public void HandlePlayerRotation()
    {
        //player direction
        Vector3 playerDirection = transform.position - new Vector3(camera.position.x, transform.position.y, camera.position.z); 
        cameraTarget.forward = playerDirection.normalized;
       

        Vector3 inputVector = cameraTarget.forward * HandleMoveValue().z + cameraTarget.right * HandleMoveValue().x;
        transform.forward = Vector3.Slerp(transform.forward, inputVector.normalized, rotationSpeed * Time.deltaTime);
        
        Velocity.x = inputVector.x;
        Velocity.z = inputVector.z;
    }

    public void HandlePlayerMove()
    {
        HandlePlayerRotation();
        //if (!isGrounded){Velocity.x /= 2; Velocity.z /= 2;}
        character.Move(Velocity * moveSpeed * Time.deltaTime);
        
    } 
        
    
    public void HandlePlayerGravity() 
    {
       //Velocity = character.velocity;
        airTime += Time.deltaTime;
        
        Mathf.Clamp(Velocity.y += gravity * airTime * Time.deltaTime, 0,0);

        
       
        HandlePlayerMove();
    }
        
    
    public void HandleGroundCheck()
    {
        isGrounded = Physics.SphereCast(transform.position, SphereRadius, Vector3.down, out hit, maxDistance, groundLayer);
        isJumping = !isGrounded;
   
        
            
    }
        
    public void HandlePlayerJump()
    {
        if(!isJumping)
        {
            isJumping = true;
       
            Velocity.y += jumpheight * Time.deltaTime;
            
           
        }
        
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = isGrounded ? Color.green : Color.red;
        Vector3 GizmosOrigin = new Vector3(transform.position.x, transform.position.y - maxDistance, transform.position.z);
        Gizmos.DrawWireSphere(GizmosOrigin, SphereRadius);
    }
}
