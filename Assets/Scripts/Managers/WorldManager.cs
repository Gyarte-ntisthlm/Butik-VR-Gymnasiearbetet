using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldManager : MonoBehaviour
{
    /// <summary>
    /// When switching between scenes, the current scene is set to this variable.
    /// This is so that the eval scene knows what information it should use.
    /// Since we don't destroy this on load, this is fine.
    /// </summary>
    public string EvalForScene { get; private set; }
    private void Awake() {
        DontDestroyOnLoad(this);

        // If there is already a world manager, destroy this one
        if (FindObjectsOfType<WorldManager>().Length > 1) {
            Destroy(gameObject);
        }
    }

    private void Start() {
        // Subscribe to the on eval complete event
        GameManager.instance.onEvalCompleted += OnEvalCompleted;
        GameManager.instance.onPurchaseCompleted += OnPurchaseCompleted;
    }

    private void OnEvalCompleted() {
        // When this method is called, determain what the next scene should be, and load it
        
        // By using the order, and EvalForScene, we can determine what the next scene should be
        if (GameManager.instance.order == "int-gui" && EvalForScene == "Intro") 
        {
            StartCoroutine(LoadTest("Interactive"));
            return;
        }
        if (GameManager.instance.order == "gui-int" && EvalForScene == "Intro") 
        {
            StartCoroutine(LoadTest("GUI"));
            return;
        }
        if (GameManager.instance.order == "int-gui" && EvalForScene == "Mixed") 
        {
            StartCoroutine(LoadTest("GUI"));
            return;
        }
        if (GameManager.instance.order == "gui-int" && EvalForScene == "Mixed")
        {
            StartCoroutine(LoadTest("Interactive"));
            return;
        }
        if ((GameManager.instance.order == "int-gui" && EvalForScene == "GUI") || (GameManager.instance.order == "int-gui" && EvalForScene == "Interactive"))
        {
            StartCoroutine(LoadTest("Thankyou"));
            return;
        }

        StartCoroutine(LoadTest("Mixed"));
        
        // I feel bad for writing this, but what is done is done, and ngl this was the most straight forward way to do it.
        // It do be treading close to YandareDev levels of if statements, but it works.
    }

    private void OnPurchaseCompleted() {
        StartCoroutine(LoadEval());
    }

    private IEnumerator LoadEval() {
        yield return new WaitForSeconds(10f); // Wait 10 seconds before loading the eval scene
        
        // When call, invoke the "magic" effect, blinding the player, then loading the eval scene

        // Invoke the "magic" effect
        FindObjectOfType<Blink>().Blind();

        yield return new WaitForSeconds(1f);

        // Load the eval scene
        EvalForScene = SceneManager.GetActiveScene().name;
        GameManager.instance.OnSceneCompleted();
        SceneManager.LoadSceneAsync("Eval");
    }

    private IEnumerator LoadTest(string scene) {
        // When call, invoke the "magic" effect, blinding the player, then loading the eval scene

        // Invoke the "magic" effect
        FindObjectOfType<Blink>().Blind();

        yield return new WaitForSeconds(1f);

        // Load the eval scene
        EvalForScene = SceneManager.GetActiveScene().name;

        SceneManager.LoadSceneAsync(scene);
    }
}
