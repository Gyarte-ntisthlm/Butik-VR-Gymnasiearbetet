using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class SS_InteractiveController : MonoBehaviour
{
    private bool hasMoney = false;
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

        if (hasMoney && purchaseObjects.Count > 0) StartCoroutine(Purchase());
    }

    IEnumerator Purchase()
    {
        yield return new WaitForSeconds(2f);
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
