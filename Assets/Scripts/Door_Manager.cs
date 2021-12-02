using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // For loading scenes

public class Door_Manager : MonoBehaviour
{
    [SerializeField]
    bool isOpen = false;

    GameObject door;
    GameObject doorVoid;

    string nextScene;
    static string previousScene;

    private void Start() {
        door = this.gameObject;
        doorVoid = door.transform.GetChild(0).gameObject;

        // Set the doors appearance.
        if(!isOpen) DeactivateDoor();

        // Define the logic for the path the player will take in between scenes and tests.
        // if the previous scene was intro, then the player will go to the next test, randomized between Store_TestA and Store_TestB.
        // Otherwise, the player will go to the ThankYouScene

        if(previousScene != "Intro") {
            switch (SceneManager.GetActiveScene().name) {
            case "Intro":
                // Randomize the next scene between Store_TestA and Store_TestB
                nextScene = Random.Range(0, 2) == 0 ? "Store_TestA" : "Store_TestB";
                break;
            case "Store_TestA":
                nextScene = "Store_TestB";
                break;
            case "Store_TestB":
                nextScene = "Store_TestA";
                break;
            default:
                nextScene = "ThankYou";
                break;
            }
        } else {
            nextScene = "ThankYou";
        }

        // Set the current scene as the previousScene.
        previousScene = SceneManager.GetActiveScene().name;
    }

    // If the door is entered call ChangeScene.
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            ChangeScene();
        }
    }
    

    // If called and "isOpen" is true, the player will change scene to "nextScene".
    public void ChangeScene()
    {
        if (isOpen) {
            SceneManager.LoadSceneAsync(nextScene);
        }
    }

    
    // == Look of the door ==

    // When this method is called the door will "activate" by changing doorVoids material color to white, 
    // un-muting the audiosource and change "isOpen" to true. 
    // and the player will be able to enter it and change scene.
    public void ActivateDoor()
    {
        doorVoid.GetComponent<Renderer>().material.color = Color.white;
        doorVoid.GetComponent<AudioSource>().mute = false;
        isOpen = true;
    }

    // Reverts the door back to its original state after being activated.
    public void DeactivateDoor()
    {
        doorVoid.GetComponent<Renderer>().material.color = Color.black;
        doorVoid.GetComponent<AudioSource>().mute = true;
        isOpen = false;
    }

}
