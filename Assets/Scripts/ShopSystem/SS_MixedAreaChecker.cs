using UnityEngine;

public class SS_MixedAreaChecker : MonoBehaviour
{
    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Player") {
            GameManager.instance.OnPurchaseAborted();
        }
    }
}
