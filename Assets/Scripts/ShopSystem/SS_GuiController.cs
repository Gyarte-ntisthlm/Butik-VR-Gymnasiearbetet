using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SS_GuiController : MonoBehaviour
{
        // Refrence to text mesh pro text object
    [SerializeField] private TMPro.TextMeshProUGUI text;
    [SerializeField] private GameObject buyButton;
    [SerializeField] private GameObject UI;

    [SerializeField] private GameObject RightRayInteractor;
    [SerializeField] private GameObject LeftRayInteractor;

    private bool isPurchasing = false;

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
        if (isPurchasing) return;
        GameManager.instance.OnPurchaseBegin();
        isPurchasing = true;
    }

    public void PressBuy() {
        GameManager.instance.OnPurchaseCompleted();
    }

    private void OnPurchaseBegin()
    {
        print("Started purchase");
        UI.SetActive(true);        
        LeftRayInteractor.SetActive(true);
        RightRayInteractor.SetActive(true);
    }

    private void OnPurchaseAborted()
    {
        print("Purchase aborted");
        UI.SetActive(false);
        LeftRayInteractor.SetActive(false);
        RightRayInteractor.SetActive(false);
        isPurchasing = false;
    }

    public void ChangeInfo(string info)
    {
        text.text = info;
        buyButton.SetActive(true);
    }
}
