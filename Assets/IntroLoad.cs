using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroLoad : MonoBehaviour
{
    private void Awake() {
        // Load the eval scene
        SceneManager.LoadScene("Eval");
    }
}
