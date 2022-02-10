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
        consoleUpperImage = GameObject.Find("ConsoleUpperImage").GetComponent<TextMeshProUGUI>();
        consoleUpperText = GameObject.Find("ConsoleUpperText").GetComponent<TextMeshProUGUI>();

        consoleLowerImage = GameObject.Find("ConsoleLowerImage").GetComponent<TextMeshProUGUI>();
        consoleLowerText = GameObject.Find("ConsoleLowerText").GetComponent<TextMeshProUGUI>();
    }


    public void ExecuteNextEvent()
    {

    }
    
    public void ExecuteEvent()
    {

    }

}