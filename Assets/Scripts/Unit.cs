using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class Unit : MonoBehaviour
{
    [SerializeField] private int health = 10;
    [HideInInspector] public bool IsAbleToAct = false;
    [HideInInspector] public bool IsShot = false;

    [HideInInspector] public TurnManager turnManager;
    CinemachineVirtualCamera virtualCamera;
    Transform cameraTransform;

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

                Vector3 positionToMove = new Vector3(transform.position.x, cameraTransform.transform.position.y, transform.position.z);

                float distance = Vector3.Distance(cameraTransform.transform.position, positionToMove);
                // Dynamic transition speed, so that it's shorter if the camera doesn't move far.
                float transitionSpeed = Mathf.Clamp(distance / 5f, 0.1f, 2f);

                cameraTransform.DOMove(positionToMove, transitionSpeed);

                StartCoroutine(EnableActionAfterTransition(transitionSpeed));
            }
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
        virtualCamera = GameObject.Find("Camera").GetComponent<CinemachineVirtualCamera>();
        cameraTransform = GameObject.Find("Camera").GetComponent<Transform>();
    }
}
