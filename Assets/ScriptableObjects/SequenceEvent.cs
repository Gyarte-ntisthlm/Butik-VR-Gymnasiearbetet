using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
[CreateAssetMenu(menuName = "Events/SequenceEvent")]
public class SequenceEvent : ScriptableObject
{ 
    [Tooltip("Usefull if you want to let the player to \"take in\" their surroundings before playing the audio etc.")]
    [Range(0, 30)]
    public float delayFromTrigger;
    
    [Tooltip("If the light should be activated at a special point in the sequence. For instance on a specific sentence in the audio clip.")]
    [Range(0, 30)]
    public float delayLightActivation;
    
    [Tooltip("If the objects should be activated at a special point in the sequence. For instance on a specific sentence in the audio clip.")]
    [Range(0, 30)]
    public float delayObjectActivation;

    [Tooltip("This value is added ontopp of the Delay Object Activation value.")]
    [Range(0, 15)]
    public float delayObjectDeactivation;

    [Tooltip("This is for how long each subtitle should be displayed.")]
    [Range(1, 30)]
    public float[] subtitleDuration;

    [Space]

    public AudioClip audioClip;
    public string[] subtitles;

    [Space]

    public AudioSource audioSource;
    public int objectsToActivateID;
    public int objectsToDeactivateID;

    [Space]
    [Tooltip("GameManager events to trigger. If left empty will not trigger any events.")]
    //public List<string> eventsToTrigger;
    public string[] eventsToTrigger;
}