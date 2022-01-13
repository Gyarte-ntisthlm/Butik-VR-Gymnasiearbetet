using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubtitleIntroManager : MonoBehaviour
{
    public static SubtitleIntroManager instance;

    void Awake()
    {
        instance = this;
    }


    public Subtitle[] subtitles;

    // Update is called once per frame
    void Update()
    {
        
    }

    public struct Subtitle
    {
        public string Text;
        public float Delay;
        public AudioSource AudioSource;
    }
}