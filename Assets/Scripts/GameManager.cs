using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    // Purchase events
    public event Action onPurchaseBegin;
    public event Action onPurchaseCompleted;
    public event Action onPurchaseAborted;

    // Eval events
    // The "onPurchaseCompleted" dubles as the "onEvalBegin" event.
    public event Action onEvalCompleted;
    public event Action onSceneCompleted;

    // Reset events
    public event Action onReset;

    public event Action onActivateDoor;
    public event Action onDecideOrder;

    // Purchase events.
    public void OnPurchaseBegin() => onPurchaseBegin?.Invoke();
    public void OnPurchaseCompleted() => onPurchaseCompleted?.Invoke();
    public void OnPurchaseAborted() => onPurchaseAborted?.Invoke();

    // Eval events.
    public void OnEvalCompleted() => onEvalCompleted?.Invoke();

    /// <summary>
    /// Use this command when the scene is done, this will trigger a scene change as well as trigger the reset event.
    /// </summary>
    public void OnSceneCompleted() => onSceneCompleted?.Invoke();
    public void OnActivateDoor() => onActivateDoor?.Invoke();
    public void OnDecideOrder() => onDecideOrder?.Invoke();
    public void OnReset() => onReset?.Invoke();
}
