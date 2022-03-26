using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using System;

public class TeleportationController : MonoBehaviour
{
    public GameObject baseControllerGameObject;
    public GameObject teleportationGameObject;

    public InputActionReference teleportActionRef;

    [Space]
    public UnityEvent onTeleportActivate;
    public UnityEvent onTeleportCancel;


    private void Start()
    {
        teleportActionRef.action.performed += TeleportActivate;
        teleportActionRef.action.canceled += TeleportCancel;
    }

    private void TeleportActivate(InputAction.CallbackContext obj)
    {
        onTeleportActivate.Invoke();
    }

    private void TeleportCancel(InputAction.CallbackContext obj)
    {
        Debug.Log("TeleportCancel");
        Invoke("DeactivateTeleportation", 0.1f);
    }

    private void DeactivateTeleportation()
    {
        onTeleportCancel.Invoke();
    }
}
