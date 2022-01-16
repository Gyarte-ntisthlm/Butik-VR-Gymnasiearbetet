using UnityEngine;
using UnityEditor;

public class GameManagerEditor : EditorWindow
{
    private int opb = 0;
    private int opc = 0;
    private int opa = 0;
    private int oec = 0;
    private int osc = 0;
    private int or = 0;


    // Start listening on the events from the GameManager.
    private void OnEnable()
    {
        GameManager.instance.onPurchaseBegin += OnPurchaseBegin;
        GameManager.instance.onPurchaseCompleted += OnPurchaseCompleted;
        GameManager.instance.onPurchaseAborted += OnPurchaseAborted;

        GameManager.instance.onEvalCompleted += OnEvalCompleted;

        GameManager.instance.onSceneCompleted += OnSceneCompleted;

        GameManager.instance.onReset += OnReset;
    }

    [MenuItem("Gymnasiearbetet/Game Manager Debugger")]
    private static void ShowWindow()
    {
        var window = GetWindow<GameManagerEditor>();
        window.titleContent = new GUIContent("Game Manager Debugger");
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("Game Manager Debugger", EditorStyles.boldLabel);

        // Description
        GUILayout.Label("This is a tool to debug the GameManager. It will show you all the events that the GameManager will trigger.", EditorStyles.wordWrappedLabel);

        GUILayout.BeginVertical("Box");
        GUILayout.Label("General Events", EditorStyles.boldLabel);
        if (GUILayout.Button("OnSceneCompleted"))
        {
            GameManager.instance.OnSceneCompleted();
        }
        if (GUILayout.Button("OnEvalCompleted"))
        {
            GameManager.instance.OnEvalCompleted();
        }
        GUILayout.EndVertical();
        // Button section with spaceing on top and bottom.
        GUILayout.BeginVertical("Box");
        GUILayout.Label("Purchase Events", EditorStyles.boldLabel);
        // All the events of the GameManager.
        if (GUILayout.Button("OnPurchaseBegin"))
        {
            GameManager.instance.OnPurchaseBegin();
        }
        if (GUILayout.Button("OnPurchaseCompleted"))
        {
            GameManager.instance.OnPurchaseCompleted();
        }
        if (GUILayout.Button("OnPurchaseAborted"))
        {
            GameManager.instance.OnPurchaseAborted();
        }
        GUILayout.EndVertical();

        GUILayout.BeginVertical("Box");
        GUILayout.Label("Reset Events", EditorStyles.boldLabel);
        if (GUILayout.Button("OnReset"))
        {
            GameManager.instance.OnReset();
        }
        GUILayout.EndVertical();


        GUILayout.BeginVertical("Box");
        GUILayout.Label("Events Triggered", EditorStyles.boldLabel);
        // All the events that has been triggered.
        GUILayout.Label("OnPurchaseBegin: " + opb);
        GUILayout.Label("OnPurchaseCompleted: " + opc);
        GUILayout.Label("OnPurchaseAborted: " + opa);
        GUILayout.Label("OnEvalCompleted: " + oec);
        GUILayout.Label("OnSceneCompleted: " + osc);
        GUILayout.Label("OnReset: " + or);
        GUILayout.EndVertical();

        GUILayout.BeginVertical("Box");
        GUILayout.Label("Launch Questionnaire", EditorStyles.boldLabel);
        if (GUILayout.Button("Launch Questionnaire"))
        {
            QuestionnaireLauncher.instance.LaunchQuestionnaire();
        }
        GUILayout.EndVertical();
    }

    // When the GameManager has triggered an event return true.
    private void OnPurchaseBegin() => opb++;
    private void OnPurchaseCompleted() => opc++;
    private void OnPurchaseAborted() => opa++;
    private void OnEvalCompleted() => oec++;
    private void OnSceneCompleted() => osc++;
    private void OnReset() => or++;


    // Stop listening on the events from the GameManager.
    private void OnDisable()
    {
        GameManager.instance.onPurchaseBegin -= OnPurchaseBegin;
        GameManager.instance.onPurchaseCompleted -= OnPurchaseCompleted;
        GameManager.instance.onPurchaseAborted -= OnPurchaseAborted;

        GameManager.instance.onEvalCompleted -= OnEvalCompleted;

        GameManager.instance.onSceneCompleted -= OnSceneCompleted;

        GameManager.instance.onReset -= OnReset;

    }
}