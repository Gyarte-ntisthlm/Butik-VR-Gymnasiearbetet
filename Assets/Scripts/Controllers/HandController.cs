using UnityEngine;
using UnityEngine.InputSystem;

public class HandController : MonoBehaviour
{
    [SerializeField]
    InputActionReference controllerActionGrip;

    [SerializeField]
    InputActionReference controllerActionTrigger;

    private Animator _handAnimator;

    private void Awake()
    {
        controllerActionGrip.action.performed += OnGrip;
        controllerActionTrigger.action.performed += OnTrigger;

        _handAnimator = GetComponent<Animator>();
    }

    private void OnTrigger(InputAction.CallbackContext obj)
    {
        _handAnimator.SetFloat("Trigger", obj.ReadValue<float>());
    }

    private void OnGrip(InputAction.CallbackContext obj)
    {
        _handAnimator.SetFloat("Grip", obj.ReadValue<float>());
    }
}
