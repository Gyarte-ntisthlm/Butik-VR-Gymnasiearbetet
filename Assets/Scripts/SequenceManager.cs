using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteAlways]
public class SequenceManager : MonoBehaviour
{
    public static SequenceManager instance;
    private List<GameObject> instantiatedObjects;

    // To prevent the sequence from being triggered multiple times.
    private bool isPlaying = false;

    private void Awake() {
        instance = this;
    }

    public void OnEventTriggered(SequenceEvent sequenceEvent)
    {
        Debug.Log($"OnEventTriggered: {sequenceEvent.name}");

        if (isPlaying) return;
        isPlaying = true;
        StartCoroutine(IsPlayingTimer());
        
        // Wait for the delay.
        StartCoroutine(ExecuteSequenceEvent(sequenceEvent));
    }

    IEnumerator ExecuteSequenceEvent(SequenceEvent sequenceEvent)
    {
        // Wait for delay.
        yield return new WaitForSeconds(sequenceEvent.delayFromTrigger);

        // Activate light.
        StartCoroutine(ActivateLight(sequenceEvent));

        // Activate objects.
        StartCoroutine(ActivateGameObjects(sequenceEvent));

        // Deactivate objects.
        StartCoroutine(DeactivateGameObjects(sequenceEvent));

        // Play the audio clip.
        if (sequenceEvent.audioSource != null && sequenceEvent.audioClip != null)
        {
            sequenceEvent.audioSource.PlayOneShot(sequenceEvent.audioClip);
        }

        // Show the subtitles.
        foreach (string subtitle in sequenceEvent.subtitles)
        {
            Debug.Log(subtitle);
            yield return new WaitForSeconds(sequenceEvent.subtitleDuration);
            SubtitleManager.instance.ShowSubtitle(subtitle);
        }

        // Wait for the subtitle to be hidden.
        yield return new WaitForSeconds(sequenceEvent.subtitleDuration);

        // Hide the subtitle.
        SubtitleManager.instance.HideSubtitle();


        // If there are any game manager events to trigger, trigger them.
        foreach (string eventToTrigger in sequenceEvent.eventsToTrigger)
        {
            if (!string.IsNullOrEmpty(eventToTrigger))
            {
                Invoke($"GameManger.instance.{sequenceEvent.eventsToTrigger}()", 0);
            }
        }
    }

    IEnumerator ActivateLight(SequenceEvent sequenceEvent)
    {
        // Wait for delay.
        yield return new WaitForSeconds(sequenceEvent.delayLightActivation);

        // Turn on the lights.
        if (sequenceEvent.light != null)
        {
            sequenceEvent.light.enabled = true;
        }
    }

    IEnumerator ActivateGameObjects(SequenceEvent sequenceEvent)
    {

        // Wait for delay.
        yield return new WaitForSeconds(sequenceEvent.delayObjectActivation);


        // This will not work due to how scriptable objects work.
        // We can only instantiate the objects that are specified in the SO
        // and not the ones that are in the scene.
        // This applies to the light objects to activate and deactivate.

        // // Turn on the objects.
        // if (sequenceEvent.objectsToActivate != null)
        // {
        //     foreach (GameObject go in sequenceEvent.objectsToActivate)
        //     {
        //         go.SetActive(true);
        //     }
        // }

        // Instantiate the objects.
        if (sequenceEvent.objectsToActivate != null)
        {
            foreach (GameObject go in sequenceEvent.objectsToActivate)
            {
                GameObject goInstance = Instantiate(go);
                goInstance.SetActive(true);
            }
        }

        // This do require us to set up the game objects in the scene with the correct position first.
        // Then either removing them from the scene or disabling them. Another alternative is creating a different scene for building and gameplay.
        // In the build scene, create the objects and set them up (overide the transforms for the prefab).
        // Then in the gameplay scene, simply just set up the managers etc.

        // Skiten man gör när allting går åt skogen... Får fan en huvudvärk.
    }

    IEnumerator DeactivateGameObjects(SequenceEvent sequenceEvent)
    {
        // Wait for delay.
        yield return new WaitForSeconds(sequenceEvent.delayObjectActivation + sequenceEvent.delayObjectDeactivation);

        // Turn off the objects.
        if (sequenceEvent.objectsToDeactivate != null)
        {
            foreach (GameObject go in sequenceEvent.objectsToDeactivate)
            {
                go.SetActive(false);
            }
        }
    }

    IEnumerator IsPlayingTimer()
    {
        yield return new WaitForSeconds(3);
        isPlaying = false;
    }

    #if UNITY_EDITOR

    // Draw a line from the manager to the SequenceControllers
    private void OnDrawGizmosSelected()
    {
        if (instance == null) return;

        foreach (SequenceController sequenceController in FindObjectsOfType<SequenceController>())
        {
            Handles.color = Color.yellow;
            Handles.DrawAAPolyLine(2, new Vector3[] { transform.position, sequenceController.transform.position });

            Vector3 labelPosition = sequenceController.transform.position;
            Vector3 labelScale = sequenceController.transform.lossyScale;
            Vector3 labelSize = sequenceController.GetComponent<BoxCollider>().size;

            // Draw a cube at the position of the SequenceController that is the size of its box collider.
            Handles.DrawWireCube(
                new Vector3 (labelPosition.x, labelPosition.y + 1, labelPosition.z ), 
                new Vector3 (labelSize.x/5, labelSize.y, labelSize.z/5)
            );

            Handles.Label(
                new Vector3(labelPosition.x, labelPosition.y + 10, labelPosition.z), 
                sequenceController.sequenceEvent.name
            );

            // // NOTE: Github copilot suggested this and it works sooo.

            // // Draw a line from the SequenceController to all the gameobjects that are to be activated.
            // for (int i = 0; i < sequenceController.sequenceEvent.objectsToActivate.Count; i++)
            // {
            //     GameObject go = sequenceController.sequenceEvent.objectsToActivate[i];
            //     Handles.color = Color.green;
            //     Handles.DrawAAPolyLine(2, new Vector3[] { sequenceController.transform.position, go.transform.position });
            // }

            // // Draw a line from the SequenceController to all the gameobjects that are to be deactivated.
            // foreach (GameObject go in sequenceController.sequenceEvent.objectsToDeactivate)
            // {
            //     Handles.color = Color.red;
            //     Handles.DrawAAPolyLine(2, new Vector3[] { sequenceController.transform.position, go.transform.position });
            //     Gizmos.DrawWireSphere(go.transform.position, 1);
            // }

            // // Draw a line from the SequenceController to the light that is to be activated.
            // if (sequenceController.sequenceEvent.light != null)
            // {
            //     Handles.color = Color.blue;
            //     Handles.DrawAAPolyLine(2, new Vector3[] { sequenceController.transform.position, sequenceController.sequenceEvent.light.transform.position });
            //     Gizmos.DrawWireSphere(sequenceController.sequenceEvent.light.transform.position, 1);
            // }

            // // Draw a line from the SequenceController to the audio source that is to be played.
            // if (sequenceController.sequenceEvent.audioSource != null && sequenceController.sequenceEvent.audioClip != null)
            // {
            //     Handles.color = Color.cyan;
            //     Handles.DrawAAPolyLine(2, new Vector3[] { sequenceController.transform.position, sequenceController.sequenceEvent.audioSource.transform.position });
            //     Gizmos.DrawWireSphere(sequenceController.sequenceEvent.audioSource.transform.position, 1);
            // }
        }
    }
    #endif
}

