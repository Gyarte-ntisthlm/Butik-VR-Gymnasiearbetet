using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "Events/SequenceEvent")]
public class SequenceEvent : ScriptableObject
{
    public string subtitle;
    public float delayFromTrigger;
    public float subtitleDuration;
    public AudioSource audioSource;
    public AudioClip audioClip;
}