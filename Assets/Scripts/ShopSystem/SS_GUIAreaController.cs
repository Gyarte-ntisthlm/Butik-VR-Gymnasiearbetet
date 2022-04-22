using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
public class SS_GUIAreaController : MonoBehaviour
{
    [Tooltip("The button on the controller that activates the purchase whilst in the trigger/purchase area.")]
    [SerializeField] private InputActionReference purchaseActionRefRight;
    [SerializeField] private InputActionReference purchaseActionRefLeft;

    [SerializeField] private XRRayInteractor rightHandRay;
    [SerializeField] private XRRayInteractor leftHandRay;

    [SerializeField] private GameObject rightHandHelper;
    [SerializeField] private GameObject leftHandHelper;
    

    private void OnTriggerExit(Collider other)
    {
        // Check if the other is the Player, if so invoke the OnPurchaseBegin event.
        if (other.gameObject.tag != "Player") return;
        GameManager.instance.OnPurchaseAborted();
        rightHandRay.enabled = false;
        leftHandRay.enabled = false;  

        rightHandHelper.SetActive(false);
        leftHandHelper.SetActive(false);      

    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the other is the Player, if so show the tooltip for how to activate the purchase menu.
        if (other.gameObject.tag != "Player") return;
        rightHandRay.enabled = true;
        leftHandRay.enabled = true;

        rightHandHelper.SetActive(true);      
        leftHandHelper.SetActive(true);      
    }

    private void OnTriggerStay(Collider other)
    {
        // Check if the other is the Player, if so invoke the OnPurchaseBegin event.
        if (other.gameObject.tag != "Player") return;
        // Check if the purchase is active, if so invoke the OnPurchaseCompleted event.


        //// This causes the purchase to be registered multiple times. Fix this.
        if (purchaseActionRefRight.action.triggered || purchaseActionRefLeft.action.triggered) FindObjectOfType<SS_GuiController>().TogglePurchase();
    }
}
