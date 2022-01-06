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

  // Intro events
  // The tutorial is split into 3 parts, the movement, grab and start.
  public event Action onIntroMovement;
  public event Action onIntroTeleportCompleted;
  public event Action onIntroSnapCompleted;

  public event Action onIntroGrabCompleted;
  public event Action onSceneCompleted;

  // Reset events
  public event Action onReset;

  // Purchase events.
  public void OnPurchaseBegin() => onPurchaseBegin?.Invoke();
  public void OnPurchaseCompleted() => onPurchaseCompleted?.Invoke();
  public void OnPurchaseAborted() => onPurchaseAborted?.Invoke();

  // Eval events.
  public void OnEvalCompleted() => onEvalCompleted?.Invoke();

  // Intro events.
  public void OnIntroMovement() => onIntroMovement?.Invoke();
  public void OnIntroTeleportCompleted() => onIntroTeleportCompleted?.Invoke();
  public void OnIntroSnapCompleted() => onIntroSnapCompleted?.Invoke();
  public void OnIntroGrabCompleted() => onIntroGrabCompleted?.Invoke();

  /// <summary>
  /// Use this command when the scene is done, this will trigger a scene change as well as trigger the reset event.
  /// </summary>
  public void OnSceneCompleted() => onSceneCompleted?.Invoke();



  public void OnReset() => onReset?.Invoke();
}
