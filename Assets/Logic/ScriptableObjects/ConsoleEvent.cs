using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
[CreateAssetMenu(menuName = "Events/ConsoleEvent", fileName = "CE_")]
public class ConsoleEvent : ScriptableObject
{
    // Tops screen text or image
    public string topText;
    public Image topImage;

    [Space]
    // Bottom screen text or image
    public string bottomText;
    public Image bottomImage;
    
    [Space]
    // Audio clip
    public AudioClip audioClip;

    [Space]
    // Buttons
    public Image[] extaButtonImages;
    public string[] extraButtonTexts;
    public string[] extraButtonEvents;
}


