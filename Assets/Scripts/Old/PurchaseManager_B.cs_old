using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class PurchaseManager_B : MonoBehaviour
{

  [Tooltip("The button on the controller that activates the purchase whilst in the trigger/purchase area.")]
  public List<InputActionReference> purchaseActionRef;
  private bool purchaseActive = false;
  [Space]
  [Tooltip("The list of purchase areas. In this case that would be the NPC that the player can purchase from.")]
  public List<BoxCollider> purchaseAreas = new List<BoxCollider>();
  private List<GameObject> purchaseObjects = new List<GameObject>();

  // Start is called before the first frame update
  private void Start()
  {
    GameManager.instance.onPurchaseBegin += OnPurchaseBegin;
    GameManager.instance.onPurchaseCompleted += OnPurchaseCompleted;


    foreach(InputActionReference actionRef in purchaseActionRef)
    {
      actionRef.action.performed += PurchaseActivate;
      actionRef.action.canceled += PurchaseCancel;
    }
  }

  private void OnPurchaseBegin()
  {
    Debug.Log("Purchase Begin");
    // Do stuff here
  }

  private void OnPurchaseCompleted()
  {
    Debug.Log("Purchase Completed");

    // Do stuff here
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

  // Some editor/Gizmo stuff
#if UNITY_EDITOR
  private void OnDrawGizmosSelected()
  {
    Gizmos.color = Color.blue;

    foreach (BoxCollider purchaseArea in purchaseAreas)
    {
      Gizmos.DrawWireCube(purchaseArea.transform.position, purchaseArea.size);

      // Draw a Anti-Aliased line between the purchaseArea and the Controller using a Handle.
      Handles.color = Color.blue;
      Handles.DrawAAPolyLine(5, purchaseArea.transform.position, transform.position);
    }
  }
#endif

}
