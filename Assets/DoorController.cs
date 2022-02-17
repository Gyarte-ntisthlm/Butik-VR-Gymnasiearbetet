using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorController : XRGrabInteractable
{
    public UnityEvent OnDoorOpen;

    // This is called when the player grabs the doorknob.
    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        base.OnSelectEntering(args);
        
        OnDoorOpen.Invoke();
    }
}
