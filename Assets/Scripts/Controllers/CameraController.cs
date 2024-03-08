using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private InputActionReference clickR, drag, scroll;
    private Vector3 targetFocusPoint = Vector3.zero;
    private Vector3 currentFocusPoint  = Vector3.zero;
    
    [SerializeField]
    private float targetFocusLerpSpeed = 1f;

    [SerializeField]
    private float rotationSpeed = 1f;

    [SerializeField]
    private float scrollSpeed = 1f;
    [SerializeField]
    private float zoomLerpSpeed = .5f;

    private Quaternion targetRotation;
    private Quaternion currentRotation;
    
    private float zoom = -.3f;
    [SerializeField]
    private float targetZoom = -.3f;
    [SerializeField]
    private Vector2 zoomClamp;

    void Start()
    {
        zoom = targetZoom;

        targetRotation = transform.rotation;
        currentRotation = targetRotation;
        
        Zoom();
    }
    
    void Update()
    {
        if(clickR.action.IsPressed())
        {
            ToggleCursor(false);
            Rotate();
        }
        else
        {
            ToggleCursor(true);
        }

        LerpTargetPoint();
        Zoom();
    }

    private void LerpTargetPoint()
    {
        currentFocusPoint = Vector3.Lerp(currentFocusPoint, targetFocusPoint, targetFocusLerpSpeed * Time.deltaTime);
    }

    private void Zoom()
    {
        targetZoom = Mathf.Clamp((targetZoom += scroll.action.ReadValue<float>() * scrollSpeed * Time.deltaTime), zoomClamp.x, zoomClamp.y);
        zoom = Mathf.Lerp(zoom, targetZoom, zoomLerpSpeed * Time.deltaTime);

        transform.position = currentFocusPoint;
        transform.Translate(new Vector3(0,0,zoom));
    }

    private void Rotate()
    {
        Vector2 dragDir = drag.action.ReadValue<Vector2>();

        transform.Rotate(new Vector3(-dragDir.y * rotationSpeed, 0, 0));
        transform.Rotate(new Vector3(0, dragDir.x * rotationSpeed, 0), Space.World);
    }

    private void SetFocusTarget(Vector3 focus){ targetFocusPoint = focus; }
    private void SetFocusCurrent(Vector3 focus){ currentFocusPoint = focus; targetFocusPoint = focus;}


    private void ToggleCursor(bool toggle)
    {
        Cursor.visible = toggle;
        Cursor.lockState = toggle? CursorLockMode.None : CursorLockMode.Locked;
    }
}