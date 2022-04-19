using UnityEngine;
using UnityEngine.InputSystem;

public class DoorController : MonoBehaviour
{
    private bool isGriped = false;
    [SerializeField] private InputActionReference LeftHandGrip = null;
    [SerializeField] private InputActionReference RightHandGrip = null;

    private void OnTriggerStay(Collider other)
    {   
        if (isGriped) return;
        
        if (other.gameObject.tag == "Player" && (RightHandGrip.action.IsPressed() || LeftHandGrip.action.IsPressed()))
        {
            isGriped = true;
            GameManager.instance.OnEvalCompleted();
            print("Eval completed");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        isGriped = false;
    }
}
