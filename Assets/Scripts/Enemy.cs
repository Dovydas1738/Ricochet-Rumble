using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    [SerializeField] private float shotForce = 100;

    Transform playerTransform;
    Rigidbody rb;

    private void Start()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Vector3 directionToShoot = playerTransform.position - transform.position;

        if (IsAbleToAct)
        {
            Shoot(rb, shotForce, directionToShoot);
        }

        // End turn when enemy stops moving.
        if (IsShot && rb.IsSleeping())
        {
            IsShot = false;
            turnManager.EndTurn();
        }

    }

    public override void Shoot(Rigidbody rb, float force, Vector3 direction)
    {
        base.Shoot(rb, force, direction);
    }
}
