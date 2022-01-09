// NOTE(Zedzee):
// In this script, it will listen on all the events from the game manager
// and set the current timestamp since the game started when the event is triggered.
// The saved timestamp is stored in a dictionary, and the dictionary is saved in a file (analytics.log) once the "sceneCompleted" event is called.
// The file is loaded and parsed in the last scene, this is then sent along with the web request when the questionnaire is opened.


using System;
using System.IO;
using UnityEngine;

public class Analytics : MonoBehaviour
{
  public string prefix = "";

  string path;

  //private Dictionary<string, float> analyticsDictionary = new Dictionary<string, float>();
  private AnalyticsData analyticsData = new AnalyticsData();

  // Start is called before the first frame update
  void Start()
  {
    path = $"{Application.persistentDataPath}/{prefix}_analytics.log";

    // Start listening on all GameManager events

    // General events
    GameManager.instance.onSceneCompleted += OnSceneCompleted;
    GameManager.instance.onReset += OnReset;
    GameManager.instance.onEvalCompleted += OnEvalCompleted;

    // Intro events
    GameManager.instance.onIntroMovement += OnIntroMovement;
    GameManager.instance.onIntroTeleportCompleted += OnIntroTeleportCompleted;
    GameManager.instance.onIntroSnapCompleted += OnIntroSnapCompleted;
    GameManager.instance.onIntroGrabCompleted += OnIntroGrabCompleted;

    // Purchase events
    GameManager.instance.onPurchaseBegin += OnPurchaseBegin;
    GameManager.instance.onPurchaseCompleted += OnPurchaseCompleted;
  }

  private void OnPurchaseCompleted()
  {
    // Save the timestamp
    analyticsData.PurchaseCompleted = Time.timeSinceLevelLoad;
  }

  private void OnPurchaseBegin()
  {
    // Save the timestamp since level loaded
    analyticsData.PurchaseBegin = Time.timeSinceLevelLoad;
  }

  private void OnIntroGrabCompleted()
  {
  }

  private void OnIntroSnapCompleted()
  {
  }

  private void OnIntroTeleportCompleted()
  {
  }

  private void OnIntroMovement()
  {
  }

  private void OnEvalCompleted()
  {
  }

  private void OnReset()
  {
  }

  private void OnSceneCompleted()
  {
    // Save the timestamp
    analyticsData.SceneCompleted = Time.timeSinceLevelLoad;
    analyticsData.prefix = prefix;

    Save(analyticsData);
  }

  private void Save(AnalyticsData jsonObject)
  {
    // Save the analytics dictionary to a file
    string json = JsonUtility.ToJson(jsonObject);
    Debug.Log(json);
    Debug.Log($"Saving analytics to {path}");

    File.WriteAllText(path, json);
    Debug.Log("Analytics saved to file");
  }
}

[Serializable]
public class AnalyticsData
{
  // Each event in GameManager is a key, and the time since the game started is the value.
  // in the format: { "event_name": time_since_game_started }

  public string prefix = "";
  public float PurchaseBegin;
  public float PurchaseCompleted;
  public float SceneCompleted;

}
