using UnityEngine;
using System.Collections.Generic;

public class ConsoleController : MonoBehaviour
{
    [Tooltip("The events will be triggered in the order they are in this list.")]
    public GameObject[] consoleCanvases;

    private List<GameObject> instantiatedList = new List<GameObject>();

    private int currentCanvasIndex = 0;

    private void Start()
    {   
        GameObject go = Instantiate(consoleCanvases[currentCanvasIndex], consoleCanvases[currentCanvasIndex].transform.position, consoleCanvases[currentCanvasIndex].transform.rotation);
        // Set the first canvas to be active
        instantiatedList.Add(go);
        instantiatedList[currentCanvasIndex].SetActive(true);
    }

    public void NextScreen()
    {
        // Remove the old canvas
        instantiatedList[currentCanvasIndex].SetActive(false);
        Debug.Log("Current canvas index: " + currentCanvasIndex);
        Debug.Log(consoleCanvases[currentCanvasIndex].name);

        currentCanvasIndex++;
        
        GameObject go = Instantiate(consoleCanvases[currentCanvasIndex], consoleCanvases[currentCanvasIndex].transform.position, consoleCanvases[currentCanvasIndex].transform.rotation);
        // Instantiate the next canvas
        instantiatedList.Add(go);
        instantiatedList[currentCanvasIndex].SetActive(true);

        Debug.Log("Current canvas index: " + currentCanvasIndex);
        Debug.Log(consoleCanvases[currentCanvasIndex].name);            
    }
}