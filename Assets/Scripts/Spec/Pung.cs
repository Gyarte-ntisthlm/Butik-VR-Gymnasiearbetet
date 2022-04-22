using UnityEngine;

public class Pung : MonoBehaviour
{
    // Get the camera component transform
    private Transform cameraTransform;

    private void Awake() {
        cameraTransform = Camera.main.transform;
    }

    private void FixedUpdate() {
        // Move the transform to the camera position with a vertical offset of -1
        transform.position = cameraTransform.position + Vector3.up * -0.6f;
    }
}
