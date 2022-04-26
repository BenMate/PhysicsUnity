using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagDollTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Touch");
        RagDoll ragdoll = other.GetComponentInParent<RagDoll>();
        if (ragdoll != null)
            ragdoll.RagDollOn = true;
    }
}
