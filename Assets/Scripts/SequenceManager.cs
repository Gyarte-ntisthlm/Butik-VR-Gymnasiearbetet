using System.Collections;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteAlways]
public class SequenceManager : MonoBehaviour
{
    public static SequenceManager instance;

    private void Awake() {
        instance = this;
    }

    public void OnEventTriggered(SequenceEvent sequenceEvent)
    {
        Debug.Log($"OnEventTriggered: {sequenceEvent.name}");

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

        // Show the subtitle.
        if (!string.IsNullOrEmpty(sequenceEvent.subtitle))
        {
            SubtitleManager.instance.ShowSubtitle(sequenceEvent.subtitle);
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

        // Turn on the objects.
        if (sequenceEvent.objectsToActivate != null)
        {
            foreach (GameObject go in sequenceEvent.objectsToActivate)
            {
                go.SetActive(true);
            }
        }
    }

    IEnumerator DeactivateGameObjects(SequenceEvent sequenceEvent)
    {
        // Wait for delay.
        yield return new WaitForSeconds(sequenceEvent.delayObjectActivation + sequenceEvent.delayObjectDeactivation);

        // Turn on the objects.
        if (sequenceEvent.objectsToDeactivate != null)
        {
            foreach (GameObject go in sequenceEvent.objectsToDeactivate)
            {
                go.SetActive(false);
            }
        }
    }

    #if UNITY_EDITOR

    // Draw a line from the manager to the SequenceControllers
    private void OnDrawGizmosSelected()
    {
        print("OnDrawGizmosSelected");
        if (instance == null) return;

        foreach (SequenceController sequenceController in FindObjectsOfType<SequenceController>())
        {
            print("OnDrawGizmosSelected: " + sequenceController.name);
            Handles.color = Color.yellow;
            Handles.DrawAAPolyLine(2, new Vector3[] { transform.position, sequenceController.transform.position });

            // Draw a cube at the position of the SequenceController.
            Handles.DrawWireCube(sequenceController.transform.position, Vector3.one);
        }
    }
    #endif
}

