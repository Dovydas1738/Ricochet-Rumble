using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class Unit : MonoBehaviour
{
    [HideInInspector] public TurnManager turnManager;
    CinemachineVirtualCamera virtualCamera;
    Transform cameraTransform;

    [SerializeField] private int health = 10;
    [SerializeField] private float cameraZOffset = -5f;

    [HideInInspector] public bool IsShot = false;

    private bool isFollowedByCamera = false;

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

                Vector3 positionToMove = new Vector3(transform.position.x, cameraTransform.transform.position.y, transform.position.z + cameraZOffset);

                float distance = Vector3.Distance(cameraTransform.transform.position, positionToMove);

                // Dynamic transition speed, so that it's shorter if the camera doesn't move far.
                float transitionSpeed = Mathf.Clamp(distance / 5f, 0.1f, 2f);

                cameraTransform.DOMove(positionToMove, transitionSpeed);

                StartCoroutine(EnableActionAfterTransition(transitionSpeed));
            }
            else
            {
                isFollowedByCamera = false;
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

            if (value)
            {
                isFollowedByCamera = true;
            }
        }
    }

    // TODO add a death VFX and SFX fields to play on death

    private void LateUpdate()
    {
        if (isFollowedByCamera)
        {
            CameraFollow();
        }
    }

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

    private void CameraFollow()
    {
        virtualCamera.transform.position = new Vector3(transform.position.x, cameraTransform.transform.position.y, transform.position.z + cameraZOffset);
    }

    private void GetComponents()
    {
        turnManager = GameObject.Find("TurnManager").GetComponent<TurnManager>();
        virtualCamera = GameObject.Find("Camera").GetComponent<CinemachineVirtualCamera>();
        cameraTransform = GameObject.Find("Camera").GetComponent<Transform>();
    }
}
