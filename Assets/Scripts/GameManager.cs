using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake() {
        instance = this;
    }
    
    public event Action onPurchaseBegin;
    public event Action onPurchaseCompleted;
    public event Action onPurchaseAborted;
    public event Action onReset;
    
    public void OnPurchaseBegin() => onPurchaseBegin?.Invoke();
    public void OnPurchaseCompleted() => onPurchaseCompleted?.Invoke();
    public void OnPurchaseAborted() => onPurchaseAborted?.Invoke();
    public void OnReset() => onReset?.Invoke();



}
