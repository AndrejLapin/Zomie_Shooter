using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class WeaponZoom : MonoBehaviour
{

    Camera myCamera;
    RigidbodyFirstPersonController fpController;
    [SerializeField] float fovDefault = 70.5328f;
    [SerializeField] float fovZoomed = 40f;
    [SerializeField] float sensDefault = 1f;
    [SerializeField] float sensZoomed = 0.5f;

    private void OnDisable()
    {
        SetDefaultZoom();
    }

    private void Awake()
    {
        myCamera = FindObjectOfType<Camera>();
        fpController = GetComponentInParent<RigidbodyFirstPersonController>();
    }
    
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            ZoomIn();
        }
        else
        {
            SetDefaultZoom();
        }
    }

    private void SetDefaultZoom()
    {
        myCamera.fieldOfView = fovDefault;
        fpController.mouseLook.XSensitivity = 1f;
        fpController.mouseLook.YSensitivity = 1f;
    }

    private void ZoomIn()
    {
        myCamera.fieldOfView = fovZoomed;
        fpController.mouseLook.XSensitivity = 0.5f;
        fpController.mouseLook.YSensitivity = 0.5f;
    }
}
