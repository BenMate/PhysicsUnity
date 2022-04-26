using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CannonBall : MonoBehaviour
{

    public float forceOfFire = 400.0f;

    private bool canFire = true;

    Rigidbody rigidbody = null;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.isKinematic = true;
    }

    void Update()
    {
        if (Input.anyKeyDown && canFire)
        {
            rigidbody.isKinematic = false;
            rigidbody.AddForce(transform.forward * forceOfFire);
            canFire = false;
        }
    }
}
