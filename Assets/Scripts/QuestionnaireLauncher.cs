using System;
using System.IO;
using UnityEngine;

public class QuestionnaireLauncher : MonoBehaviour
{
  public static QuestionnaireLauncher instance;
  public event Action onQuestionnaireLaunched;
  public void LaunchQuestionnaire() => onQuestionnaireLaunched?.Invoke();

  private string id = "123";
  private string secret = "123";

  AnalyticsData[] args;

  // Start is called before the first frame update
  void Start()
  {
    instance = this;

    // Start listening to the launch questionnaire event
    onQuestionnaireLaunched += OnQuestionnaireLaunched;
  }

  private void OnQuestionnaireLaunched()
  {
    string[] files = GetDataFiles();
    args = new AnalyticsData[files.Length];

    for (int i = 0; i < files.Length; i++)
    {
      args[i] = Deserialize(File.ReadAllText(files[i]));
      Debug.Log(args[i].PurchaseBegin);
    }

    string queryString = GenerateQueryString();

    // Launch the questionnaire
    Debug.Log("QuestionnaireLauncher.OnQuestionnaireLaunched");
    Application.OpenURL($"https://nti-gyarte.web.app/questionnaire?id={id}&{queryString}&secret={secret}");
  }

  string[] GetDataFiles()
  {
    // Read all the .log files in the persistent data path
    return Directory.GetFiles(Application.persistentDataPath, "*.log");
  }

  AnalyticsData Deserialize(string data)
  {
    return JsonUtility.FromJson<AnalyticsData>(data);
  }

  string GenerateQueryString()
  {
    // Generate the query string by iterating through the analytics data
    // and appending the key value pairs to the query string
    // use the prefix of the file name as the key
    // that is: "intro_movement" becomes "dev_intro_movement=1"

    string queryString = "";

    for (int i = 0; i < args.Length; i++)
    {
      queryString += $"{args[i].prefix}_PurchaseBegin={args[i].PurchaseBegin}&";
      queryString += $"{args[i].prefix}_PurchaseCompleted={args[i].PurchaseBegin}&";
      queryString += $"{args[i].prefix}_SceneCompleted={args[i].PurchaseBegin}&";
    }

    return queryString;
  }

  // TODO(Zedzee): Get firebase and create the doc on the server.
  // TODO(Zedzee): The docs id is also stored in the "id" var.
  // TODO: Generate a unique "secret" for each user using base64.

}
