using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//must have a rigidbody, will freak out if it cant find the attached one on the object.
[RequireComponent(typeof(Rigidbody))]
public class PhysicsObject : MonoBehaviour
{
    public Material awakeMaterial = null;
    public Material sleepingMaterial = null;

    private Rigidbody rb = null;
    private bool wasSleeping = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (rb.IsSleeping() && !wasSleeping && sleepingMaterial != null)
        {
            wasSleeping = true;
            GetComponent<MeshRenderer>().material = sleepingMaterial;
        }
        if (!rb.IsSleeping() && wasSleeping && awakeMaterial != null)
        {
            wasSleeping = false;
            GetComponent<MeshRenderer>().material = awakeMaterial;
        }
    }
}
