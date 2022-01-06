using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveController : MonoBehaviour
{
  //Get the shared material.
  private Material material;

  [Range(0.01f, 1f)]
  public float dissolveSpeed = 0.05f;

  public float dissolveFrom = 100f;

  public float dissolveTo = -5f;

  private void Start()
  {
    GameManager.instance.onPurchaseCompleted += OnPurchaseCompleted;
    GameManager.instance.onReset += OnReset;

    //Get the shared material.
    material = GetComponent<Renderer>().material;
  }

  private void OnPurchaseCompleted()
  {
    //Start the dissolve coroutine.
    StartCoroutine(Dissolve());
  }

  private IEnumerator Dissolve()
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

  public void OnReset()
  {
    //Reset the dissolve amount to the pre determained one.
    material.SetFloat("_CutOfHight", dissolveFrom);
  }

  private void OnDestroy()
  {
    GameManager.instance.onPurchaseCompleted -= OnPurchaseCompleted;
  }
}
