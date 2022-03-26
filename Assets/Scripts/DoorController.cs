using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;

public class DoorController : XRGrabInteractable
{

    // This is called when the player grabs the doorknob.
    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        base.OnSelectEntering(args);
        
        GameManager.instance.OnEvalCompleted();
    }
}
