using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class SS_InteractiveController : MonoBehaviour
{
    private bool hasMoney = false;
    private bool isInvoked = false;
    private List<GameObject> purchaseObjects = new List<GameObject>();
    private bool isPurchasing = false;

    private void Start() {
        GameManager.instance.onPurchaseBegin += OnPurchaseBegin;
    }

    private void OnDestroy() {
        GameManager.instance.onPurchaseBegin -= OnPurchaseBegin;
    }

    private void OnPurchaseBegin() => isPurchasing = true;

    public void AddMoney()
    {
        hasMoney = true;
        
        if(isPurchasing) return;
        GameManager.instance.OnPurchaseBegin();
    }

    public void RemoveMoney()
    {
        hasMoney = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Item")
        {
            if (purchaseObjects.Count == 0)
                GameManager.instance.OnPurchaseBegin();

            purchaseObjects.Add(other.gameObject);
            print("Added " + other.gameObject.name + " to purchaseObjects");
        }

    }


    public void InvokePurchase()
    {
        if(isInvoked) return; // Don't invoke twice.
        if (!hasMoney && purchaseObjects.Count == 0) return; // Don't invoke if there's no money and no items.
        isInvoked = true;
        GameManager.instance.OnPurchaseCompleted();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Item")
        {
            purchaseObjects.Remove(other.gameObject);

            if (purchaseObjects.Count == 0 && !hasMoney)
                GameManager.instance.OnPurchaseAborted();
        }
    }
}
