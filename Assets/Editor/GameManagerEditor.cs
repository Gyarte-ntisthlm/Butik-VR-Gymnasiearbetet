using UnityEngine;
using UnityEditor;

public class GameManagerEditor : EditorWindow
{
  private int opb = 0;
  private int opc = 0;
  private int opa = 0;
  private int oec = 0;
  private int oim = 0;
  private int oitc = 0;
  private int oisc = 0;
  private int og = 0;
  private int oic = 0;
  private int or = 0;


  // Start listening on the events from the GameManager.
  private void OnEnable()
  {
    GameManager.instance.onPurchaseBegin += OnPurchaseBegin;
    GameManager.instance.onPurchaseCompleted += OnPurchaseCompleted;
    GameManager.instance.onPurchaseAborted += OnPurchaseAborted;

    GameManager.instance.onEvalCompleted += OnEvalCompleted;

    GameManager.instance.onIntroMovement += OnIntroMovement;
    GameManager.instance.onIntroTeleportCompleted += OnIntroTeleportCompleted;
    GameManager.instance.onIntroSnapCompleted += OnIntroSnapCompleted;
    GameManager.instance.onIntroGrabCompleted += OnIntroGrabCompleted;
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
    GUILayout.Label("Intro Events", EditorStyles.boldLabel);
    if (GUILayout.Button("OnIntroMovement"))
    {
      GameManager.instance.OnIntroMovement();
    }
    if (GUILayout.Button("OnIntroTeleportCompleted"))
    {
      GameManager.instance.OnIntroTeleportCompleted();
    }
    if (GUILayout.Button("OnIntroSnapCompleted"))
    {
      GameManager.instance.OnIntroSnapCompleted();
    }
    if (GUILayout.Button("OnIntroGrabCompleted"))
    {
      GameManager.instance.OnIntroGrabCompleted();
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
    GUILayout.Label("OnIntroMovement: " + oim);
    GUILayout.Label("OnIntroTeleportCompleted: " + oitc);
    GUILayout.Label("OnIntroSnapCompleted: " + oisc);
    GUILayout.Label("OnIntroGrabCompleted: " + og);
    GUILayout.Label("OnSceneCompleted: " + oic);
    GUILayout.Label("OnReset: " + or);
    GUILayout.EndVertical();
  }

  // When the GameManager has triggered an event return true.
  private void OnPurchaseBegin() => opb++;
  private void OnPurchaseCompleted() => opc++;
  private void OnPurchaseAborted() => opa++;
  private void OnEvalCompleted() => oec++;
  private void OnIntroMovement() => oim++;
  private void OnIntroTeleportCompleted() => oitc++;
  private void OnIntroSnapCompleted() => oisc++;
  private void OnIntroGrabCompleted() => og++;
  private void OnSceneCompleted() => oic++;
  private void OnReset() => or++;


  // Stop listening on the events from the GameManager.
  private void OnDisable()
  {
    GameManager.instance.onPurchaseBegin -= OnPurchaseBegin;
    GameManager.instance.onPurchaseCompleted -= OnPurchaseCompleted;
    GameManager.instance.onPurchaseAborted -= OnPurchaseAborted;

    GameManager.instance.onEvalCompleted -= OnEvalCompleted;

    GameManager.instance.onIntroMovement -= OnIntroMovement;
    GameManager.instance.onIntroTeleportCompleted -= OnIntroTeleportCompleted;
    GameManager.instance.onIntroSnapCompleted -= OnIntroSnapCompleted;
    GameManager.instance.onIntroGrabCompleted -= OnIntroGrabCompleted;
    GameManager.instance.onSceneCompleted -= OnSceneCompleted;

    GameManager.instance.onReset -= OnReset;

  }
}