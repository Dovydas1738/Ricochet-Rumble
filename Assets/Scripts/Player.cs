using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    [SerializeField] int damage = 3;

    private void Start()
    {
        TurnToPlay = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Unit unit = collision.gameObject.GetComponent<Unit>();

        if (unit != null && unit != this)
        {
            unit.TakeDamage(damage);
        }
    }
}
