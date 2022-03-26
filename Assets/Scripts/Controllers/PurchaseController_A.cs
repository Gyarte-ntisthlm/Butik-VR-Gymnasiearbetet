using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseController_A : MonoBehaviour
{
    private List<GameObject> coins = new List<GameObject>();
    private List<GameObject> purchaseObjects = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Item")
        {
            if (purchaseObjects.Count == 0)
                GameManager.instance.OnPurchaseBegin();

            purchaseObjects.Add(other.gameObject);
        }
        else if (other.gameObject.tag == "Coin")
        {
            coins.Add(other.gameObject);
        }

        if (other.gameObject.tag == "Coin" && purchaseObjects.Count > 0)
            GameManager.instance.OnPurchaseCompleted();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Item")
        {
            purchaseObjects.Remove(other.gameObject);

            if (purchaseObjects.Count == 0)
                GameManager.instance.OnPurchaseAborted();
        }
        else if (other.gameObject.tag == "Coin")
        {
            coins.Remove(other.gameObject);
        }
    }


}
