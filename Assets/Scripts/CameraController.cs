using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    [SerializeField] CinemachineVirtualCamera overviewCamera;
    [SerializeField] Transform cameraTransform;

    [SerializeField] private float rotationSpeed = 1f;

    public void MoveCameraToPosition(Transform unitTransform)
    {
        if(unitTransform.gameObject.GetComponent<Player>() != null)
        {
            overviewCamera.enabled = false;
            virtualCamera.enabled = true;
        }
        else
        {
            virtualCamera.enabled = false;
            overviewCamera.enabled = true;

            overviewCamera.LookAt = unitTransform;
        }
    }

    public void RotateCamera(Transform anchorObject)
    {
        Debug.Log("rotating");

        float mouseX = Input.GetAxis("Mouse X");

        virtualCamera.transform.RotateAround(anchorObject.position, Vector3.up, mouseX * rotationSpeed);
    }

    private void GetComponents()
    {
        virtualCamera = GameObject.Find("YourVirtualCameraName").GetComponent<CinemachineVirtualCamera>();
        cameraTransform = GetComponent<Transform>();
    }

    private IEnumerator SetCameraFollowAfterTransition(Transform unit, float transitionSpeed)
    {
        if(virtualCamera.Follow != null)
        {
            virtualCamera.Follow = null;
        }

        yield return new WaitForSeconds(transitionSpeed);

        virtualCamera.Follow = unit;
    }
}
