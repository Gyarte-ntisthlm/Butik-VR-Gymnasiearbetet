using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;

public class BuildVersionProcessor : IPreprocessBuildWithReport
{
    public int callbackOrder => 0;

    private const string primaryVersion = "0.0";

    public void OnPreprocessBuild(BuildReport report)
    {
        string currentVersion = FindCurrentVersion();
        UpdateVersion(currentVersion);
    }

    private string FindCurrentVersion()
    {
        // Find the current version in the build settings
        string[] currentVersion = PlayerSettings.bundleVersion.Split('[',']');

        // If not the proper format, start with the initial version
        return currentVersion.Length == 1 ? primaryVersion : currentVersion[1];
    }

    private void UpdateVersion(string version)
    {
        if(float.TryParse(version, out float versionNumber))
        {
            versionNumber += 0.01f;
            string date = System.DateTime.Now.ToString("yyyyMMdd-HH.mm.ff");

            PlayerSettings.bundleVersion = string.Format("Build [{0}] {1}", versionNumber, date);
            Debug.Log("Updated version to " + PlayerSettings.bundleVersion);
        }

    }
}
