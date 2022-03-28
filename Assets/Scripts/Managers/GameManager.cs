using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public string order = "int-gui";
    public int currentWorldID = 0;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this);
        order = DetermainOrder();
    }

    private static string DetermainOrder(){
        System.Random rnd = new System.Random();
        int r = rnd.Next(0, 1);
        return r == 0 ? "int-gui" : "gui-int";
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
    public void OnReset() => onReset?.Invoke();
}