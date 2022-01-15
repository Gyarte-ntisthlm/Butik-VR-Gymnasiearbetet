using System;
using System.Collections.Generic;
using UnityEngine;

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

    [Tooltip("This should be a bit longer than the audio clip duration.")]
    [Range(0, 30)]
    public float subtitleDuration;

    [Space]

    public AudioClip audioClip;
    public string subtitle;

    [Space]

    public AudioSource audioSource;
    public Light light;
    public List<GameObject> objectsToActivate;

    [Space]
    [Tooltip("GameManager events to trigger. If left empty will not trigger any events.")]
    public List<string> eventsToTrigger;
}