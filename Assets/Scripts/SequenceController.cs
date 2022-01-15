using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SequenceController : MonoBehaviour
{
    public SequenceEvent sequenceEvent;
    private void OnTriggerEnter(Collider other)
    {
      SequenceManager.instance.OnEventTriggered(sequenceEvent);
    }
}

