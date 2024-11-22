using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class Unit : MonoBehaviour
{
    [HideInInspector] public TurnManager turnManager;
    CameraController cameraController;

    [SerializeField] private int health = 10;

    [HideInInspector] public bool IsShot = false;

    protected bool _turnToPlay = false;
    [HideInInspector] public bool TurnToPlay
    {
        get
        {
            return _turnToPlay;
        }
        set
        {
            _turnToPlay = value;

            // When it's unit's turn to play, move the camera to it and enable action.
            if (value)
            {
                GetComponents();

                cameraController.EnableCamera(transform);

                // 1.2f is cinemachine brain easing time.
                StartCoroutine(EnableActionAfterTransition(1.2f));
            }
        }
    }

    private bool _isAbleToAct = false;
    [HideInInspector] public bool IsAbleToAct
    {
        get
        {
            return _isAbleToAct;
        }
        set
        {
            _isAbleToAct = value;
        }
    }

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

        IsAbleToAct = false;
        IsShot = true;
    }

    // Ensure that unit is only able to act when the camera is in position.
    private IEnumerator EnableActionAfterTransition(float transitionSpeed)
    {
        yield return new WaitForSeconds(transitionSpeed);

        IsAbleToAct = true;
    }

    private void GetComponents()
    {
        turnManager = GameObject.Find("TurnManager").GetComponent<TurnManager>();
        cameraController = GameObject.Find("Camera").GetComponent<CameraController>();
    }
}
