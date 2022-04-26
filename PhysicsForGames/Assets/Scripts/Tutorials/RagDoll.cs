using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class RagDoll : MonoBehaviour
{
    private Animator m_animator = null;
    public List<Rigidbody> m_rigidBodies = new List<Rigidbody>();

    public bool RagDollOn
    {
        get { return !m_animator.enabled; }
        set 
        { 
            m_animator.enabled = !value; 
            foreach(Rigidbody rigidbody in m_rigidBodies)
            {
                rigidbody.isKinematic = !value;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_animator = GetComponent<Animator>();
        foreach (Rigidbody rigidbody in m_rigidBodies)
        {
            rigidbody.isKinematic = true;
        }
    }
}
