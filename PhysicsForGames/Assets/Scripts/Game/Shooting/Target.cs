
using UnityEngine;
using UnityEngine.Events;

public class Target : MonoBehaviour
{
    public float health = 50.0f;
    public UnityEvent onTargetDied;

    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0.0f)
        {
            Die();
            onTargetDied?.Invoke();
        }
    }

    void Die()
    {
        //check if target has a ragdoll... 
        RagDoll rd = GetComponentInParent<RagDoll>();

        if (rd != null)
        {
            rd.RagDollOn = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
