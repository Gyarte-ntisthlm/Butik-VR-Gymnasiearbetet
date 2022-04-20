using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SS_MixedController : MonoBehaviour
{
    private void Start() {
        // Register the Purchase events
        GameManager.instance.onPurchaseBegin += OnPurchaseBegin;   
    }

    private void OnDestroy() {
        GameManager.instance.onPurchaseBegin -= OnPurchaseBegin;
    }

    public void TogglePurchase() {
        GameManager.instance.OnPurchaseBegin();
    }

    public void PressBuy() {
        GameManager.instance.OnPurchaseCompleted();
    }

    private void OnPurchaseBegin()
    {
        print("Started purchase");   
    }
}
