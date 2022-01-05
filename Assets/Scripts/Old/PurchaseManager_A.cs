using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class PurchaseManager_A : MonoBehaviour
{
  public List<BoxCollider> purchaseAreas;
  private List<GameObject> purchaseObjects;

  // Start is called before the first frame update
  private void Start()
  {
    GameManager.instance.onPurchaseBegin += OnPurchaseBegin;
    GameManager.instance.onPurchaseCompleted += OnPurchaseCompleted;
  }

  private void OnPurchaseBegin()
  {
    Debug.Log("Purchase Begin");
    // Do stuff here

  }

  private void OnPurchaseCompleted()
  {
    Debug.Log("Purchase Begin");
    // Do stuff here

  }

  private void OnTriggerEnter(Collider other)
  {
    Debug.Log(other.gameObject.name);


    // Check if the other has the tag of item and also check if there is a gameobject with the tag of Coin contained in the purchaseObjects list.
    if (other.gameObject.tag == "Item")
    {
      // Add the other gameobject to the purchaseObjects list.
      purchaseObjects.Add(other.gameObject);

      // Begin the purchase process.
      GameManager.instance.OnPurchaseBegin();

      // Check if the purchaseObjects list contains a gameobject with the tag of Coin using a for loop.
      for (int i = 0; i < purchaseObjects.Count; i++)
      {
        Debug.Log(purchaseObjects[i].tag);

        if (purchaseObjects[i].tag == "Coin")
        {
          // If the purchaseObjects list contains a gameobject with the tag of Coin, then call the OnPurchase event.
          GameManager.instance.OnPurchaseCompleted();
        }
      }

    }
  }

#if UNITY_EDITOR
  // Some editor/Gizmo stuff
  private void OnDrawGizmos()
  {
    Gizmos.color = Color.green;
    foreach (BoxCollider purchaseArea in purchaseAreas)
    {
      Gizmos.DrawWireCube(purchaseArea.transform.position, purchaseArea.size);

      // Draw a Anti-Aliased line between the purchaseArea and the Controller using a Handle.
      Handles.color = Color.green;
      Handles.DrawAAPolyLine(5, purchaseArea.transform.position, transform.position);
    }
  }
#endif

}
