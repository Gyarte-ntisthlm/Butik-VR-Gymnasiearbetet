using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRRayInteractor))]
public class RayToggleController : MonoBehaviour
{
    [SerializeField] private InputActionReference activateRefrence = null;

    private XRRayInteractor xrRayInteractor = null;
    private bool isEnabled = false;

    // Start is called before the first frame update
    private void Awake()
    {
        xrRayInteractor = GetComponent<XRRayInteractor>();
    }
    
    private void OnEnable() {
        activateRefrence.action.started += ToggleRay;
        activateRefrence.action.canceled += ToggleRay;
    }

    private void OnDisable() {
        activateRefrence.action.started -= ToggleRay;
        activateRefrence.action.canceled -= ToggleRay;
    }

    private void ToggleRay(InputAction.CallbackContext context)
    {
        isEnabled = context.control.IsPressed();

    }

    private void LateUpdate() {
        ApplyStatus();
    }

    private void ApplyStatus()
    {
        if(xrRayInteractor.enabled != isEnabled)
            xrRayInteractor.enabled = isEnabled;
    }
}
