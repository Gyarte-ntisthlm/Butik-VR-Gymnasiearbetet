using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportationManager : MonoBehaviour
{
    [Header("Providers")]
    [SerializeField] private InputActionAsset inputActionAsset;
    [SerializeField] private TeleportationProvider teleportationProvider;

    [Header("XR Ray Interactors")]
    [SerializeField] private XRRayInteractor xrRayInteractorRight;
    [SerializeField] private XRRayInteractor xrRayInteractorLeft;

    
    [Space(2f)]
    [Header("Thumbsticks")]

    [Tooltip("The 'Move' action")] [SerializeField] private InputActionReference thumbstickRight;
    
    [Tooltip("The 'Move' action")] [SerializeField] private InputActionReference thumbstickLeft;


    [Space(2f)]
    [Header("Teleportation Actions - Right Hand")]
    // This is the right handed actions
    [SerializeField] private InputActionReference activateTeleportationActionRight;
    [SerializeField] private InputActionReference cancelTeleportationActionRight;

    [Header("Teleportation Actions - Left Hand")]
    // This is the left handed actions
    [SerializeField] private InputActionReference activateTeleportationActionLeft;
    [SerializeField] private InputActionReference cancelTeleportationActionLeft;

    
    // Customizable events to trigger when the teleportation is activated or canceled
    [Space(2f)]
    [Header("Events")]
    [SerializeField] private UnityEvent onTeleportationActivated;
    [SerializeField] private UnityEvent onTeleportationCanceled;

    private void Start()
    {
        xrRayInteractorLeft.enabled = false;
        xrRayInteractorRight.enabled = false;

        // Register the actions
        inputActionAsset.Enable();

        // Register the events
        activateTeleportationActionRight.action.performed += OnActivateTeleportationRight;
        cancelTeleportationActionRight.action.performed += OnCancelTeleportationRight;
        activateTeleportationActionLeft.action.performed += OnActivateTeleportationLeft;
        cancelTeleportationActionLeft.action.performed += OnCancelTeleportationLeft;
    }

    private void OnDestroy()
    {
        // Unregister the actions
        inputActionAsset.Disable();

        // Unregister the events
        activateTeleportationActionRight.action.performed -= OnActivateTeleportationRight;
        cancelTeleportationActionRight.action.performed -= OnCancelTeleportationRight;
        activateTeleportationActionLeft.action.performed -= OnActivateTeleportationLeft;
        cancelTeleportationActionLeft.action.performed -= OnCancelTeleportationLeft;
    }

    private bool isActiveRight = false;
    private bool isActiveLeft = false;
    private void Update()
    {
        StartCoroutine(HandleTeleportationLeft());
        StartCoroutine(HandleTeleportationRight());
    }


    private IEnumerator HandleTeleportationRight()
    {  
        if(!isActiveRight) yield break;
        
        // If both the controllers are centered, it means that the player don't want to teleport anymore.
        if(thumbstickRight.action.ReadValue<Vector2>() != Vector2.zero) yield break;

        // If the target is valid, teleport the player to the target.
        xrRayInteractorRight.TryGetHitInfo(out Vector3 position, out Vector3 normal, out int positionInLine, out bool isValidTarget);
        // This always return true except if there isn't any object at all, but we can check if the target is valid.
        xrRayInteractorRight.TryGetCurrent3DRaycastHit(out RaycastHit hit);
        
        print(isValidTarget);
        
        if(!isValidTarget)
        {
            isActiveRight = false;
            onTeleportationCanceled.Invoke();
            xrRayInteractorRight.enabled = false;
            yield break;
        }

        // Teleport the player to the hit position
        TeleportRequest teleportRequest = new TeleportRequest(){
            // destinationPosition = hitLeft.point == null ? hitLeft.point : hitRight.point,
            destinationPosition = hit.point
        };

        // Invoke the teleportation events
        onTeleportationActivated.Invoke();

        teleportationProvider.QueueTeleportRequest(teleportRequest);
        

        isActiveRight = false;
        xrRayInteractorRight.enabled = false;

        yield break;
    }

    private IEnumerator HandleTeleportationLeft()
    {  
        if(!isActiveLeft) yield break;
        
        // If both the controllers are centered, it means that the player don't want to teleport anymore.
        if(thumbstickLeft.action.ReadValue<Vector2>() != Vector2.zero) yield break;

        xrRayInteractorLeft.TryGetHitInfo(out Vector3 position, out Vector3 normal, out int positionInLine, out bool isValidTarget);
        
        xrRayInteractorLeft.TryGetCurrent3DRaycastHit(out RaycastHit hit);

        if(!isValidTarget)
        {
            isActiveLeft = false;
            onTeleportationCanceled.Invoke();
            xrRayInteractorLeft.enabled = false;
            yield break;
        }

        // Teleport the player to the hit position
        TeleportRequest teleportRequest = new TeleportRequest(){
            // destinationPosition = hitLeft.point == null ? hitLeft.point : hitRight.point,
            destinationPosition = hit.point
        };

        // Invoke the teleportation events
        onTeleportationActivated.Invoke();

        teleportationProvider.QueueTeleportRequest(teleportRequest);
        
        isActiveLeft = false;
        xrRayInteractorLeft.enabled = false;

        yield break;
    }

    private void OnActivateTeleportationRight(InputAction.CallbackContext context)
    {
        // Enable the ray interactor
        xrRayInteractorRight.enabled = true;

        // Set the flag
        isActiveRight = true;
    }

    private void OnCancelTeleportationRight(InputAction.CallbackContext context)
    {
        // Disable the ray interactor
        xrRayInteractorRight.enabled = false;

        // Set the flag
        isActiveRight = false;
    }

    private void OnActivateTeleportationLeft(InputAction.CallbackContext context)
    {
        // Enable the ray interactor
        xrRayInteractorLeft.enabled = true;

        // Set the flag
        isActiveLeft = true;
    }

    private void OnCancelTeleportationLeft(InputAction.CallbackContext context)
    {
        // Disable the ray interactor
        xrRayInteractorLeft.enabled = false;

        // Set the flag
        isActiveLeft = false;
    }

}
