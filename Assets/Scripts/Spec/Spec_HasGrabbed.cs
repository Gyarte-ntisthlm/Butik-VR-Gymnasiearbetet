using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spec_HasGrabbed : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.tag != "Player") return;

        // This is just for testing, look for if the player has grabbed an object.
        GameManager.instance.OnActivateDoor();
    }
}
