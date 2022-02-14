using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using TMPro;

public class ConsoleController : MonoBehaviour
{
    [Tooltip("The events will be triggered in the order they are in this list.")]
    public ConsoleEvent[] consoleEvents;

    private int currentEvent = 0;

    // Console Screen refs
    private TextMeshProUGUI consoleUpperText;
    private TextMeshProUGUI consoleLowerText;

    private TextMeshProUGUI consoleUpperImage;
    private TextMeshProUGUI consoleLowerImage;


    public void Start()
    {
        // // Find the text mesh pro gui elements that are children of this object
        // consoleUpperText = transform.Find("ConsoleUpperText").GetComponent<TextMeshProUGUI>();
        // consoleLowerText = transform.Find("ConsoleLowerText").GetComponent<TextMeshProUGUI>();
        // consoleLowerImage = transform.Find("ConsoleLowerImage").GetComponent<TextMeshProUGUI>();
        // consoleUpperImage = transform.Find("ConsoleUpperImage").GetComponent<TextMeshProUGUI>();
    
        // // Set the text to empty
        // consoleUpperText.text = "";
        // consoleLowerText.text = "";
        
    }

    private void ClearScreen()
    {
        consoleUpperText.text = "";
        consoleLowerText.text = "";
        consoleUpperImage.text = "";
    }

    public void ExecuteNextEvent()
    {

    }
    
    public void ExecuteEvent()
    {

    }

}