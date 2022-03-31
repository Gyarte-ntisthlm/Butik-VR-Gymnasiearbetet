using UnityEngine;
using UnityEngine.EditorWindow;
using UnityEditor;


public class BlinkEditor : EditorWindow
{
    [MenuItem("Window/Blink")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(BlinkEditor));
    }

    private void OnGUI()
    {
        GUILayout.Label("Blink", EditorStyles.boldLabel);
        if (GUILayout.Button("Blink"))
        {
            // Call the blink method on the Blink script on the camera object
            //Camera.main.GetComponent<Blink>().Blink_();
            GameObject.Find("Camera").GetComponent<Blink>().Blink_();
        }
    }
}