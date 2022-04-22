using UnityEngine;
using UnityEditor;

public class SequenceAreaRenamerEditor : EditorWindow
{

    [MenuItem("Gymnasiearbetet/Sequence Area Renamer")]
    private static void ShowWindow()
    {
        var window = GetWindow<SequenceAreaRenamerEditor>();
        window.titleContent = new GUIContent("Sequence Area Renamer");
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Space(10);
        GUILayout.Label("Sequence Area Renamer", EditorStyles.boldLabel);
        // Description
        GUILayout.Label("This script will rename all SequenceArea objects in the scene to their name SequenceEvent name.");
        GUILayout.Space(10);

        if (GUILayout.Button("Rename Sequence Areas"))
        {
            RenameSequenceAreas();
        }
    }

    private void RenameSequenceAreas()
    {
        // Loop through all the objects in the scene with the component SequenceController.
        // For each object, get the SequenceController component and rename the object to the name of the SequenceEvent.
        SequenceController[] sequenceControllers = FindObjectsOfType<SequenceController>();

        foreach (SequenceController sequenceController in sequenceControllers)
        {
            Debug.Log($"Renaming {sequenceController.name} to {sequenceController.sequenceEvent.name}");
            sequenceController.gameObject.name = sequenceController.sequenceEvent.name;
        }
    }
}
