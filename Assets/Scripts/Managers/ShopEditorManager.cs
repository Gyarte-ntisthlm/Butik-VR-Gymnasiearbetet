using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteAlways]
public class ShopEditorManager : MonoBehaviour
{
  public List<GameObject> items = new List<GameObject>();
  public List<GameObject> shops = new List<GameObject>();


  private void Start()
  {
    // Get all game objects with the tag "Item" and add them to the items list.
    foreach (GameObject item in GameObject.FindGameObjectsWithTag("Item"))
    {
      items.Add(item);
    }

    // Get all game objects with the tag "Shop" and add them to the shops list.
    foreach (GameObject shop in GameObject.FindGameObjectsWithTag("Shop"))
    {
      shops.Add(shop);
    }
  }

#if UNITY_EDITOR
  private void OnDrawGizmos()
  {
    // Draw a line between the Manager and each item.
    foreach (GameObject item in items)
    {
      Handles.color = Color.green;
      Handles.DrawAAPolyLine(2, new Vector3[] { transform.position, item.transform.position });

      Gizmos.color = Color.green;
      //Draw singular Circle around the items base.
      Gizmos.DrawWireSphere(item.transform.position, item.transform.localScale.x);
    }

    // Draw a line between the Manager and each shop.
    foreach (GameObject shop in shops)
    {
      Handles.color = Color.red;
      Handles.DrawAAPolyLine(2, new Vector3[] { transform.position, shop.transform.position });

      Gizmos.color = Color.red;
      // Draw a wire cube the size of the box at its position.
      Gizmos.DrawWireCube(shop.transform.position, shop.transform.localScale);

    }

    Gizmos.color=Color.white;

  }
#endif

}
