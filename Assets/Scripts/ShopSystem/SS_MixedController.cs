using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SS_MixedController : MonoBehaviour
{
    // Refrence to text mesh pro text object
    [SerializeField] private TMPro.TextMeshProUGUI text;
    [SerializeField] private GameObject buyButton;
    [SerializeField] private GameObject UI;

    private void Start() {
        // Register the Purchase events
        GameManager.instance.onPurchaseBegin += OnPurchaseBegin;  
        GameManager.instance.onPurchaseAborted += OnPurchaseAborted; 
    }

    private void OnDestroy() {
        GameManager.instance.onPurchaseBegin -= OnPurchaseBegin;
        GameManager.instance.onPurchaseAborted -= OnPurchaseAborted;
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
        UI.SetActive(true);
    }

    private void OnPurchaseAborted()
    {
        print("Purchase aborted");
        UI.SetActive(false);
    }

    public void ChangeInfo(string info)
    {
        text.text = info;
        buyButton.SetActive(true);
    }
}
