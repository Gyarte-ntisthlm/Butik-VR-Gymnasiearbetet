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

    [SerializeField] private GameObject DoorInstructions;

    [SerializeField] private GameObject SingleBell;
    [SerializeField] private GameObject RateBell;
    [SerializeField] private GameObject Door;

    private int currentInstruction = 0;
    private string evalForScene;

    GameObject eval_gui;
    void Start()
    {
        eval_gui = GameObject.Find("Anchour");
        evalForScene = FindObjectOfType<WorldManager>().GetComponent<WorldManager>().EvalForScene;

        // If the evalfornext does not contain anything, then we are in the intro scene
        // If it does, we first display the eval for scene, then we display the next scene
        // There are always 2 instructions, either intro -> room, or eval -> room.
        
        if (evalForScene == null) Intro();
        else ShowEvalMessage();        

        // When the currentInstruction is increased, we then display the next instruction
    }


    #region Show methods
    // Intro functionality
    private void Intro()
    {
        RateBell.SetActive(false);
        SingleBell.SetActive(true);
        Instantiate(IntroInstruction[0], eval_gui.transform);
    }

    // Interactive functionality
    private void Interactive()
    {
        Instantiate(InteractiveInstruction[0], eval_gui.transform);
    }

    // Mixed functionality
    private void Mixed()
    {
        Instantiate(MixedInstruction[0], eval_gui.transform);
    }
    // GUI functionality
    private void GUI()
    {
        Instantiate(GUIInstruction[0], eval_gui.transform);
    }

    private void ShowEvalMessage()
    {
        SingleBell.SetActive(false);
        RateBell.SetActive(true);
        // Show eval message by setting the prefab as a child of the eval_gui anchour object
        Instantiate(evalRate, eval_gui.transform);
    }

    #endregion

    private void ClearScreen()
    {
        // Remove all the children of the eval_gui anchour object
        foreach (Transform child in eval_gui.transform)
        {
            Destroy(child.gameObject);
        }
    }


    public void ShowNextInstruction()
    {
        // Show the next instruction
        ClearScreen();
        RateBell.SetActive(false);
        SingleBell.SetActive(true);

        if(currentInstruction > 0){
            Instantiate(DoorInstructions, eval_gui.transform);
            Door.SetActive(true);
            GameManager.instance.OnActivateDoor();
            return;
        }

        currentInstruction++;

        if (GameManager.instance.order == "int-gui" && evalForScene == null) 
        {
            Interactive();
            return;
        }
        if (GameManager.instance.order == "gui-int" && evalForScene == "Mixed")
        {
            Interactive();
            return;
        }

        if (GameManager.instance.order == "gui-int" && evalForScene == null) 
        {
            GUI();
            return;
        }
        if (GameManager.instance.order == "int-gui" && evalForScene == "Mixed") 
        {
            GUI();
            return;
        }

        Mixed();
    }
}
