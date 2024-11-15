using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private int health = 10;

    // TODO add a death VFX and SFX fields to play on death

    public virtual void TakeDamage(int amount)
    {
        health -= amount;

        if (health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    public virtual void Shoot(Rigidbody rb, float force, Vector3 direction)
    {
        rb.AddForce(direction.normalized *  force, ForceMode.Impulse);
    }
}
