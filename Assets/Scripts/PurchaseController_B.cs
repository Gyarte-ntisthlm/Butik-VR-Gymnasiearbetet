using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PurchaseController_B : MonoBehaviour
{
  [Tooltip("The button on the controller that activates the purchase whilst in the trigger/purchase area.")]
  public List<InputActionReference> purchaseActionRef;
  private bool purchaseActive = false;


  private void Start()
  {
    foreach (InputActionReference actionRef in purchaseActionRef)
    {
      actionRef.action.performed += PurchaseActivate;
      actionRef.action.canceled += PurchaseCancel;
    }
  }

  private void PurchaseActivate(InputAction.CallbackContext obj) => purchaseActive = true;
  private void PurchaseCancel(InputAction.CallbackContext obj) => purchaseActive = false;

  private void OnTriggerEnter(Collider other)
  {
    // Check if the other is the Player, if so invoke the OnPurchaseBegin event.
    if (other.gameObject.tag == "Player" && purchaseActive) GameManager.instance.OnPurchaseBegin();
  }


  private void OnTriggerExit(Collider other)
  {
    // Check if the other is the Player, if so invoke the OnPurchaseBegin event.
    if (other.gameObject.tag == "Player") GameManager.instance.OnPurchaseAborted();
  }
}
