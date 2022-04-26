using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]

public class PlayerMovement : MonoBehaviour
{
    public float jumpVelocity = 10;
    public float speed = 10;
    public float pushPower = 2.0f;
    public bool isGrounded = false;

    public Vector3 velocity = new Vector3();
    public Vector3 hitDirection;

    bool jumpInput = false;
    CharacterController cc;
    Vector2 moveInput;

    void Start()
    {
        cc = GetComponent<CharacterController>();
    }


    void Update()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");
        jumpInput = Input.GetButton("Jump");
    }

    void FixedUpdate()
    {
        //move character with wasd
        Vector3 move;
        move = (moveInput.x * transform.right + moveInput.y * transform.forward) * speed * Time.fixedDeltaTime;

        if (isGrounded || moveInput.x != 0 || moveInput.y != 0)
        {
            velocity.x = move.x;
            velocity.z = move.z;
        }

        //jump
        if (jumpInput)     
            velocity.y = jumpVelocity;
      
        // check if we've hit ground from falling. If so, remove our velocity 
        if (isGrounded && velocity.y < 0)
            velocity.y = 0;

        //apply gravity
        velocity += Physics.gravity * Time.fixedDeltaTime;

        if (!isGrounded)
            hitDirection = Vector3.zero;

        // slide objects off surfaces they're hanging on to 
        if (moveInput.x == 0 && moveInput.y == 0)
        {
            Vector3 horizontalHitDirection = hitDirection;
            horizontalHitDirection.y = 0;
            float displacement = horizontalHitDirection.magnitude;
            if (displacement > 0)
                velocity -= 0.2f * horizontalHitDirection / displacement;
        }

        move += velocity * Time.fixedDeltaTime;
       
        cc.Move(move);
        isGrounded = cc.isGrounded;
    }

    //send a raycast under the player
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        hitDirection = hit.point - transform.position;

        //if component has a rigid body attached - using push power you can move the item
        Rigidbody body = hit.collider.attachedRigidbody;
        if (body == null || body.isKinematic)
            return;

        if (hit.moveDirection.y < -0.3f)
            return;

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        body.velocity = pushDir * pushPower;
        //--------------------------------------------------------------------------------
    }

    
}