using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class SS_InteractiveController : MonoBehaviour
{
    private bool hasMoney = false;
    private bool isInvoked = false;
    private List<GameObject> purchaseObjects = new List<GameObject>();

    public void AddMoney()
    {
        hasMoney = true;
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
        isInvoked = true;
        if (hasMoney && purchaseObjects.Count > 0) StartCoroutine(Purchase());
    }

    IEnumerator Purchase()
    {
        yield return new WaitForSeconds(10f);
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
    }
}
