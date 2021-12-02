using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro_Manager : MonoBehaviour
{
    [SerializeField]
    GameObject[] interactableAreas;
    private Dictionary<GameObject, GameObject>[] interactableAreaDictionaries;

    [SerializeField]
    GameObject door;

    private void Start() {
        door = GameObject.Find("Door");

        interactableAreaDictionaries = new Dictionary<GameObject, GameObject>[interactableAreas.Length];

        for (int i = 0; i < interactableAreaDictionaries.Length; i++)
        {
            interactableAreaDictionaries[i] = interactableAreas[i].GetComponent<SHOP_ItemDetection>().items;
        }
    }

    private void FixedUpdate() {
        if(interactableAreaDictionaries.Length > 0 && interactableAreaDictionaries[0].Count > 0 || interactableAreaDictionaries[1].Count > 0)
        {
            door.GetComponent<Door_Manager>().ActivateDoor();
        } else {
            door.GetComponent<Door_Manager>().DeactivateDoor();
        }
    }
}
