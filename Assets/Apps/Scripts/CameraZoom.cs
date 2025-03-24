using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class CameraZoom : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public float zoomSpeed = 5f;
    public float minZoom = 20f;
    public float maxZoom = 60f;

    void Update()
    {
        //float scroll = Input.GetAxis("Mouse ScrollWheel"); // Get scroll input
        float scroll = Mouse.current.scroll.ReadValue().y;
        if (scroll != 0f)
        { 
                // Adjust field of view (zoom in and out)
                float newFOV = virtualCamera.m_Lens.FieldOfView - (scroll * zoomSpeed * Time.deltaTime);
                virtualCamera.m_Lens.FieldOfView = Mathf.Clamp(newFOV, minZoom, maxZoom);
        }
    }
}
