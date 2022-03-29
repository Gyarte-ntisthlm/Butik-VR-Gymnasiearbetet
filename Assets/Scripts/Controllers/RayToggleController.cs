using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRRayInteractor))]
public class RayToggleController : MonoBehaviour
{
    [SerializeField] private InputActionReference activateReference = null;

    private XRRayInteractor xrRayInteractor = null;
    private bool isEnabled = false;

    // Start is called before the first frame update
    private void Awake()
    {
        xrRayInteractor = GetComponent<XRRayInteractor>();
    }
    
    private void OnEnable() {
        activateReference.action.started += ToggleRay;
        activateReference.action.canceled += ToggleRay;
    }

    private void OnDisable() {
        activateReference.action.started -= ToggleRay;
        activateReference.action.canceled -= ToggleRay;
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