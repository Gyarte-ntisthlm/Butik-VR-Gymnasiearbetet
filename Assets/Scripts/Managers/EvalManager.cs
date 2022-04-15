using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class EvalManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private GameObject evalRate;
    [SerializeField] private List<GameObject> IntroInstruction = new List<GameObject>();
    [SerializeField] private List<GameObject> InteractiveInstruction = new List<GameObject>();
    [SerializeField] private List<GameObject> MixedInstruction = new List<GameObject>();
    [SerializeField] private List<GameObject> GUIInstruction = new List<GameObject>();

    private int currentInstruction = 0;

    GameObject eval_gui;
    void Start()
    {
        eval_gui = GameObject.Find("Anchour");

        switch (FindObjectOfType<WorldManager>().GetComponent<WorldManager>().EvalForScene)
        {
            case "Intro":
                StartCoroutine(Intro());
                break;
            case "Interactive":
                StartCoroutine(Interactive());
                break;
            case "Mixed":
                StartCoroutine(Mixed());
                break;
            case "GUI":
                StartCoroutine(GUI());
                break;
            default:
                StartCoroutine(Intro());
                break;
        }
    }

    // Intro functionality
    IEnumerator Intro()
    {
        
        yield return new WaitForSeconds(2);
    }

    // Interactive functionality
    IEnumerator Interactive()
    {
        yield return new WaitForSeconds(2);
    }

    // Mixed functionality
    IEnumerator Mixed()
    {
        yield return new WaitForSeconds(2);
    }
    // GUI functionality
    IEnumerator GUI()
    {
        yield return new WaitForSeconds(2);
    }

    private void ShowEvalMessage()
    {
        // Remove all the children of the eval_gui anchour object
        foreach (Transform child in eval_gui.transform)
        {
            Destroy(child.gameObject);
        }

        // Show eval message by setting the prefab as a child of the eval_gui anchour object
        GameObject evalMessage = Instantiate(evalRate, eval_gui.transform);
    }
}
