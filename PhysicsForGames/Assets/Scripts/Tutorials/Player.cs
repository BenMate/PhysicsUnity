using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]

public class Player : MonoBehaviour
{
    private CharacterController controller = null;
    private Animator animator = null;

    public float speed = 100.0f;
    public float pushPower = 2.0f;


    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        float verticle = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

     
        controller.SimpleMove(transform.up * Time.fixedDeltaTime);
        transform.Rotate(transform.up, horizontal * speed * Time.fixedDeltaTime);
        animator.SetFloat("Speed", verticle * speed * Time.fixedDeltaTime);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        if (body == null || body.isKinematic) 
            return;

        if (hit.moveDirection.y < -0.3f)
            return;

        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        body.velocity = pushDir * pushPower;   
    }


}
