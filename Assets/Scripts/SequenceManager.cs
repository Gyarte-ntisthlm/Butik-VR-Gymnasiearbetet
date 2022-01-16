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
    private List <SequenceController> sequenceControllers;

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
        // Get all sequence controllers.
        // This is used if any of the objects should be activated or deactivated.
        // Only does this once it is called and not twice.
        foreach (SequenceController sc in FindObjectsOfType<SequenceController>())
        {
            if (sequenceControllers == null) sequenceControllers = new List<SequenceController>();
            sequenceControllers.Add(sc);
        }

        // Wait for delay.
        yield return new WaitForSeconds(sequenceEvent.delayFromTrigger);

        // Activate light.
        StartCoroutine(ActivateLight(sequenceEvent));

        // Activate objects.
        StartCoroutine(ActivateGameObjects(sequenceEvent));

        // Deactivate objects.
        StartCoroutine(DeactivateGameObjects(sequenceEvent));

        // Play audio.
        StartCoroutine(PlayAudio(sequenceEvent));

        // Show the subtitles.
        StartCoroutine(Subtitles(sequenceEvent));


        // If there are any game manager events to trigger, trigger them.
        foreach (string eventToTrigger in sequenceEvent.eventsToTrigger)
        {
            if (!string.IsNullOrEmpty(eventToTrigger))
            {
                Invoke($"GameManger.instance.{sequenceEvent.eventsToTrigger}()", 0);
            }
        }
    }

    IEnumerator PlayAudio(SequenceEvent sequenceEvent)
    {
        // Play the audio clip.
        if (sequenceEvent.audioSource == null && sequenceEvent.audioClip == null) 
            yield return new WaitForSeconds(0);
        
        // Move the source above the trigger area.
        sequenceEvent.audioSource.transform.position = transform.position + Vector3.up * 10;

        // Play the audio clip.
        sequenceEvent.audioSource.PlayOneShot(sequenceEvent.audioClip);
    }

    IEnumerator Subtitles(SequenceEvent sequenceEvent)
    {
        for (int i = 0; i < sequenceEvent.subtitles.Length; i++)
        {
            SubtitleManager.instance.ShowSubtitle(sequenceEvent.subtitles[i]);
            yield return new WaitForSeconds(sequenceEvent.subtitleDuration[i]);
        }

        // Hide the subtitle.
        SubtitleManager.instance.HideSubtitle();
    }

    IEnumerator ActivateLight(SequenceEvent sequenceEvent)
    {
        // Wait for delay.
        yield return new WaitForSeconds(sequenceEvent.delayLightActivation);

        // Get the light.
        Light light = GameObject.Find(sequenceEvent.name).GetComponent<Light>();

        // Activate the light.
        light.enabled = true;
    }

    IEnumerator ActivateGameObjects(SequenceEvent sequenceEvent)
    {
        // Wait for delay.
        yield return new WaitForSeconds(sequenceEvent.delayObjectActivation);

        foreach (SequenceController sc in sequenceControllers)
        {
            if (sc.id == sequenceEvent.objectsToActivateID)
            {
                sc.gameObject.SetActive(true);
            }
        }
    }

    IEnumerator DeactivateGameObjects(SequenceEvent sequenceEvent)
    {
        // Wait for delay.
        yield return new WaitForSeconds(sequenceEvent.delayObjectActivation + sequenceEvent.delayObjectDeactivation);

        // Either use an int for the id, or use tags.
        // If we use ints, then we have to structure the trigger/controller to be the parent object for the entire sequence.
        // If we use tags, then the structure does not really matter that much.

        foreach (SequenceController sc in sequenceControllers)
        {
            if (sc.id == sequenceEvent.objectsToDeactivateID)
            {
                sc.gameObject.SetActive(false);
            }
        }
    }

    IEnumerator IsPlayingTimer()
    {
        yield return new WaitForSeconds(3);
        isPlaying = false;
    }

    #if UNITY_EDITOR
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
        }
    }
    #endif
}

