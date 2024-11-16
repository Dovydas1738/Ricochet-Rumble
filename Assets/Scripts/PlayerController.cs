using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody), typeof(Player))]
public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    Player player;
    PlayerInput playerInput;

    public float MaxForce = 100f;
    [SerializeField] float maxDragDistance = 300f;

    float minForce = 0f;

    Vector3 dragStartPosition;
    float dragStartPositionX;
    float dragStartPositionZ;

    float dragEndPositionX;
    float dragEndPositionZ;

    Vector3 mousePosition;

    bool isMouseOnPlayer = false;
    bool isMouseClickedOnPlayer = false;

    float shotForce = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GetComponent<Player>();
        playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        mousePosition = GetCenteredMousePosition();

        if (isMouseOnPlayer && Input.GetMouseButtonDown(0) && player.IsAbleToAct)
        {
            GetDragStartPosition();

            isMouseClickedOnPlayer = true;
        }

        // Shoot and end turn on mouse release.
        if (Input.GetMouseButtonUp(0) && isMouseClickedOnPlayer && player.IsAbleToAct)
        {
            isMouseClickedOnPlayer = false;

            player.Shoot(rb, shotForce, player.transform.forward);
        }

        // Rotate the character's forward position while dragging to allow aiming and set force for the shot based on drag distance.
        if (isMouseClickedOnPlayer && player.IsAbleToAct)
        {
            player.transform.forward = new Vector3(-mousePosition.x, 0f, -mousePosition.y);

            float dragDistance = Vector3.Distance(dragStartPosition, GetDragEndPosition());
            float normalizedDrag = Mathf.Clamp01(dragDistance / maxDragDistance);

            shotForce = Mathf.Lerp(minForce, MaxForce, normalizedDrag);

            Debug.Log(shotForce);
        }

        // When player stops moving after the shot, end turn.
        if (player.IsShot && rb.IsSleeping())
        {
            player.IsShot = false;
            player.turnManager.EndTurn();
        }
    }

    private Vector2 GetCenteredMousePosition()
    {
        Vector2 rawMousePosition = Mouse.current.position.ReadValue();

        Vector2 origin = new Vector2(Screen.width / 2, Screen.height / 2);

        return rawMousePosition - origin;
    }

    private void GetDragStartPosition()
    {
        dragStartPositionX = mousePosition.x;
        dragStartPositionZ = mousePosition.y;

        dragStartPosition = new Vector3(dragStartPositionX, 0, dragStartPositionZ);
    }

    private Vector3 GetDragEndPosition()
    {
        dragEndPositionX = mousePosition.x;
        dragEndPositionZ = mousePosition.y;

        Vector3 dragEndPosition = new Vector3(dragEndPositionX, 0, dragEndPositionZ);

        return dragEndPosition;
    }
    private void OnMouseOver()
    {
        isMouseOnPlayer = true;
    }

    private void OnMouseExit()
    {
        isMouseOnPlayer = false;
    }
}
