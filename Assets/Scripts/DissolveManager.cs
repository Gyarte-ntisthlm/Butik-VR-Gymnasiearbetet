using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveManager : MonoBehaviour
{
  public List<Material> materialsForward;
  public List<Material> materialsBackward;

  [Range(0.01f, 1f)]
  public float dissolveSpeed = 0.05f;

  public float dissolveFrom = 100f;

  public float dissolveTo = -5f;

  private void Start()
  {
    GameManager.instance.onPurchaseCompleted += Dissolve;
    GameManager.instance.onIntroGrabCompleted += Dissolve;

    GameManager.instance.onReset += OnReset;
  }
  private void OnDestroy()
  {
    GameManager.instance.onPurchaseCompleted -= Dissolve;
    GameManager.instance.onIntroGrabCompleted -= Dissolve;
  }

  private void Dissolve()
  {
    foreach (Material mat in materialsForward)
    {
      StartCoroutine(DissolveSmoothly(mat));
    }
    
    foreach (Material mat in materialsBackward)
    {
      StartCoroutine(DissolveSmoothlyBackwards(mat));
    }

  }

  private IEnumerator DissolveSmoothly(Material mat)
  {

    float dissolveAmount = dissolveFrom;

    mat.SetFloat("_CutOfHight", dissolveAmount);

    while (dissolveAmount > dissolveTo)
    {
      dissolveAmount -= dissolveSpeed;

      //Set the _CutOfHight amount to the dissolve amount.
      mat.SetFloat("_CutOfHight", dissolveAmount);

      //Wait for 0.1 seconds.
      yield return new WaitForSeconds(Time.deltaTime);
    }
  }

    private IEnumerator DissolveSmoothlyBackwards(Material mat)
  {

    float dissolveAmount = dissolveTo;

    mat.SetFloat("_CutOfHight", dissolveAmount);

    while (dissolveAmount < dissolveFrom)
    {
      dissolveAmount += dissolveSpeed;

      //Set the _CutOfHight amount to the dissolve amount.
      mat.SetFloat("_CutOfHight", dissolveAmount);

      //Wait for 0.1 seconds.
      yield return new WaitForSeconds(Time.deltaTime);
    }
  }

  public void OnReset()
  {
    foreach (Material mat in materialsForward)
    {
      mat.SetFloat("_CutOfHight", dissolveFrom);
    }

    foreach (Material mat in materialsBackward)
    {
      mat.SetFloat("_CutOfHight", dissolveFrom);
    }
  }
}
