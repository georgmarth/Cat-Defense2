using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMovement : MonoBehaviour
{

    public float minFrustumSize = 2f;
    public float maxFrustumSize = 10f;
    public float scrollSpeed = 1f;
    public float moveSpeed = 1f;
    public float maxRadius = 30f;

    public LayerMask draggingMask;

    private Vector3 mouseDown;
    private bool dragging = false;

    private void Update()
    {
        // if the game is paused, don't drag
        if (PauseMenu.pauseMenu != null && PauseMenu.pauseMenu.pause)
        {
            return;
        }

        // Scrolling
        Scroll(Input.GetAxis("Mouse ScrollWheel"));

        // Dragging
        if (Input.GetButtonDown("Mouse01"))
        {
            BeginDrag();
        }
        else if (dragging && Input.GetButton("Mouse01"))
        {
            Drag();
        }
        else if (Input.GetButtonUp("Mouse01"))
        {
            EndDrag();
        }

        // Moving
        MoveCamera(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        ClampPosition();
    }

    private void BeginDrag()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000, draggingMask))
        {
            mouseDown = hit.point;
            dragging = true;
        }


    }

    private void Drag()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000, draggingMask))
        {
            transform.position = transform.position - (hit.point - mouseDown);
        }

    }

    private void EndDrag()
    {
        dragging = false;
    }

    private void Scroll(float amount)
    {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize + (-amount * scrollSpeed), minFrustumSize, maxFrustumSize);
    }

    private void MoveCamera(float horizontal, float vertical)
    {
        if (!dragging)
        {
            transform.Translate(new Vector3(
                horizontal * moveSpeed * Time.deltaTime,
                0f,
                vertical * moveSpeed * Time.deltaTime));
        }
    }
        
    private void ClampPosition()
    {
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, -maxRadius, maxRadius),
            transform.position.y,        
            Mathf.Clamp(transform.position.z, -maxRadius, maxRadius)
        );
    }
}
