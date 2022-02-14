using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleButtonController : MonoBehaviour
{
    private ConsoleController consoleController;

    private void Start()
    {
        consoleController = FindObjectOfType<ConsoleController>();
    }

    public void OnTriggerEnter(Collider other)
    {
        // If the player is hovering above the "next" button
        // then toggle the hover effect of the button
        if (other.gameObject.tag != "Player") return;


    }

    public void OnTriggerExit(Collider other)
    {
        consoleController.ExecuteNextEvent();
    }
}
