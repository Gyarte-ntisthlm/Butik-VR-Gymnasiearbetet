using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

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


    }

    
}

