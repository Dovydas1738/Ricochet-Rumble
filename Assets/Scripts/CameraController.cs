using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    CinemachineVirtualCamera virtualCamera;
    Transform cameraTransform;

    [SerializeField] private float cameraZOffset = -5f;
    [SerializeField] private float rotationSpeed = 1f;

    [HideInInspector] public float TransitionSpeed;

    private void Start()
    {
        GetComponents();
    }

    public void MoveCameraToActiveUnit(Vector3 unitPosition)
    {
        Vector3 positionToMove = new Vector3(unitPosition.x, cameraTransform.position.y, unitPosition.z + cameraZOffset);

        float distance = Vector3.Distance(cameraTransform.position, positionToMove);

        // Dynamic transition speed, so that it's shorter if the camera doesn't move far.
        TransitionSpeed = Mathf.Clamp(distance / 5f, 0.1f, 2f);

        cameraTransform.DOMove(positionToMove, TransitionSpeed);
    }

    public void CameraFollow(Vector3 unitPosition)
    {
        transform.position = new Vector3(unitPosition.x, cameraTransform.position.y, unitPosition.z + cameraZOffset);
    }

    public void RotateCamera(Transform anchorObject)
    {
        Debug.Log("rotating");

        float mouseX = Input.GetAxis("Mouse X");

        virtualCamera.transform.RotateAround(anchorObject.transform.position, Vector3.up, mouseX * rotationSpeed);
    }

    public void LookAt(Transform unit)
    {
        virtualCamera.LookAt = unit;
    }

    private void GetComponents()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        cameraTransform = GetComponent<Transform>();
    }
}
