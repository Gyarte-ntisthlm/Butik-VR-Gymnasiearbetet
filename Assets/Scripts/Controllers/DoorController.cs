using UnityEngine;
using UnityEngine.InputSystem;

public class DoorController : MonoBehaviour
{

    [SerializeField] private InputActionReference LeftHandGrip = null;
    [SerializeField] private InputActionReference RightHandGrip = null;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && (RightHandGrip.action.IsPressed() || LeftHandGrip.action.IsPressed()))
        {
            GameManager.instance.OnEvalCompleted();
            print("Eval completed");
        }
    }
}
