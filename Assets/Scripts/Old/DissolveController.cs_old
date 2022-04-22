using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveController : MonoBehaviour
{
  //Get the shared material.
  private Material material;

  [Range(0.01f, 1f)]
  public float dissolveSpeed = 0.05f;
  public bool reverseDirection = false;

  public float dissolveFrom = 100f;

  public float dissolveTo = -5f;

  public eventNames eventName = eventNames.OnPurchaseCompleted;
  public enum eventNames
  {
    OnPurchaseCompleted,
    onIntroGrabCompleted,
  }

  private void Start()
  {
    // use the eventName to switch between the events.
    switch (eventName)
    {
      case eventNames.OnPurchaseCompleted:
        // Subscribe to the event.
        GameManager.instance.onPurchaseCompleted += Dissolve;
        break;
      case eventNames.onIntroGrabCompleted:
        // Subscribe to the event.
        GameManager.instance.onIntroGrabCompleted += Dissolve;
        break;
    }

    GameManager.instance.onReset += OnReset;

    //Get the shared material.
    material = GetComponent<Renderer>().material;
  }
  private void OnDestroy()
  {
    // use the eventName to switch between the events.
    switch (eventName)
    {
      case eventNames.OnPurchaseCompleted:
        // Subscribe to the event.
        GameManager.instance.onPurchaseCompleted -= Dissolve;
        break;
      case eventNames.onIntroGrabCompleted:
        // Subscribe to the event.
        GameManager.instance.onIntroGrabCompleted -= Dissolve;
        break;
      default:
        GameManager.instance.onPurchaseCompleted -= Dissolve;
        break;
    }
  }

  private void Dissolve()
  {
    //Start the dissolve coroutine.
    if (reverseDirection) StartCoroutine(DissolveBackwards());
    else StartCoroutine(DissolveForward());
    // Using a termary only resulted in an error.
  }

  private IEnumerator DissolveForward()
  {
    float dissolveAmount = dissolveFrom;

    material.SetFloat("_CutOfHight", dissolveAmount);

    while (dissolveAmount > dissolveTo)
    {
      dissolveAmount -= dissolveSpeed;

      //Set the _CutOfHight amount to the dissolve amount.
      material.SetFloat("_CutOfHight", dissolveAmount);

      //Wait for 0.1 seconds.
      yield return new WaitForSeconds(Time.deltaTime);
    }
  }
  private IEnumerator DissolveBackwards()
  {
    float dissolveAmount = dissolveTo;

    material.SetFloat("_CutOfHight", dissolveAmount);

    while (dissolveAmount < dissolveFrom)
    {
      dissolveAmount += dissolveSpeed;

      //Set the _CutOfHight amount to the dissolve amount.
      material.SetFloat("_CutOfHight", dissolveAmount);

      //Wait for 0.1 seconds.
      yield return new WaitForSeconds(Time.deltaTime);
    }
  }

  public void OnReset()
  {
    //Reset the dissolve amount to the pre determained one.
    material.SetFloat("_CutOfHight", dissolveFrom);
  }
}
