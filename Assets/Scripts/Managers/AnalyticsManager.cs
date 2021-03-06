using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Firestore;

public class AnalyticsManager : MonoBehaviour
{

    // Event stuff
    public static AnalyticsManager instance;
    public event Action onQuestionnaireLaunched;
    public void LaunchQuestionnaire() => onQuestionnaireLaunched?.Invoke();

    // Firebase stuff
    private FirebaseAuth auth;
    private FirebaseUser user;
    private FirebaseFirestore firestore;
    DependencyStatus dependencyStatus;

    private string id = "";
    private string secret = "";
    private string baseSecret = "";
    private string baseId = "";

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        // Start listening to the launch questionnaire event
        onQuestionnaireLaunched += questionnaireOrder;

       // Check that all of the necessary dependencies for Firebase are present on the system.
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            
            dependencyStatus = task.Result;
            
            if (dependencyStatus != DependencyStatus.Available)
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
                // Firebase Unity SDK is not safe to use here.
                return;
            }
            
            // Firebase is ready to use here.
            InitializeFirebase();
        });
    }

    private void questionnaireOrder(){
        Register();
        Application.OpenURL($"https:qna-butik-vr.web.app/{baseId}/{baseSecret}");
    }

    private void InitializeFirebase(){
        auth = FirebaseAuth.DefaultInstance;
    }

    private void Register()
    {
        // Randomize the user id and secret
        // id is used as the email in the sign up process, format: yMAsQAk1hhg@butik.vr
        
        // The id is the unix time stamp of the current tim in milliseconds, 
        // this effectively eliminates the risk of duplicates compared to the old system.
        id = $"{Base64UrlEncode(((DateTimeOffset)DateTime.Now.ToUniversalTime()).ToUnixTimeMilliseconds().ToString())}@butik.vr";
        
        // The secret is the base64 encoded timestamp
        secret = Base64UrlEncode(DateTime.Now.ToString());

        // Generate the url safe base64 hash from the combined string of id and secret, this is sent to the questionnaire and
        // is converted back into text that can be used to verify the user.
        baseId = Base64UrlEncode(id);
        baseSecret = Base64UrlEncode(secret);

        Debug.Log($"id: {id}, secret: {secret}, base64url: {baseId}/{baseSecret}");

        StartCoroutine("RegisterCoroutine");
    }

    IEnumerator RegisterCoroutine()
    {
        Debug.Log("Registering user");
        //Call the Firebase auth signin function passing the email and password
        var RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(id, secret);
        //Wait until the task completes
        yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

        // Check if the user was created successfully
        if (RegisterTask.IsCompleted)
        {
            user = RegisterTask.Result;
            Debug.Log("User created successfully");
            
            CreateDatabaseEntry();
        }
        else
        {
            Debug.LogError("User creation failed");
        }
    }

    private void CreateDatabaseEntry()
    {
        // Create a new entry in the database
        firestore = FirebaseFirestore.DefaultInstance;
        print("Assinging firestore");
        DocumentReference docRef = firestore.Collection("data").Document(user.UserId);
        print("Assinging docRef");

        print("Getting recorded data");
        Dictionary<string, object> userData = new Dictionary<string, object>
        {
            { "id", id },
            { "secret", secret },
            { "baseId", baseId },
            { "baseSecret", baseSecret },
            { "gameCompleted", DateTime.Now.ToUniversalTime().ToString() },
            { "order", GameManager.instance.order},
            { "build", Application.version },
            { "timeSinceStart", Time.realtimeSinceStartup },
            { "collectedData", GetAnalyticsData()}
        };

        print("Setting data");
        docRef.SetAsync(userData).ContinueWith(task => {
            if (task.IsCompleted)
            {
                Debug.Log("Document written successfully");

                // Save a copy of the data locally
                SaveData(userData);
            }
            else
            {
                Debug.Log("Document write failed");
            }
        });
    }

    private Dictionary<string, object> GetAnalyticsData()
    {
        print("Getting analytics data");
        string[] files = GetDataFiles();

        AnalyticsData[] args = new AnalyticsData[files.Length];

        Dictionary<string, object> data = new Dictionary<string, object>();

        for (int i = 0; i < files.Length; i++)
        {
            args[i] = Deserialize(File.ReadAllText(files[i]));

            Dictionary<string, object> temp = new Dictionary<string, object>();

            // Manually deserialize the data, 'cause yes.
            temp.Add("Prefix", args[i].prefix);
            temp.Add("PurchaseBegin", args[i].PurchaseBegin);
            temp.Add("PurchaseCompleted", args[i].PurchaseCompleted);
            temp.Add("SceneCompleted", args[i].SceneCompleted);
            temp.Add("Rating", args[i].Rating);

            data.Add(args[i].prefix, temp);

            print(temp.ToString());
        }


        return data;
    }

    void SaveData(Dictionary<string, object> data)
    {
        string path = Application.persistentDataPath + "/data.json";
        File.WriteAllText(path, JsonUtility.ToJson(data));
    }

    string[] GetDataFiles()
    {
        // Read all the .log files in the persistent data path
        return Directory.GetFiles(Application.persistentDataPath, "*.analytics");
    }

    AnalyticsData Deserialize(string data)
    {
        return JsonUtility.FromJson<AnalyticsData>(data);
    }

    // Creates a URL safe encoding
    private string Base64UrlEncode(string input) {
        var inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
        // Special "url-safe" base64 encode.
        return Convert.ToBase64String(inputBytes)
        .Replace('+', '-') // replace URL unsafe characters with safe ones
        .Replace('/', '_') // replace URL unsafe characters with safe ones
        .Replace("=", ""); // no padding
    }
}
