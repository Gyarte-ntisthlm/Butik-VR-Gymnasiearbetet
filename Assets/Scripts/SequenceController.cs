using UnityEngine;
using System.Collections;

public class SequenceController : MonoBehaviour
{
    public int id;

    [Space]

    public SequenceEvent sequenceEvent;

    [Space]

    public bool hasMany = false;
    [Tooltip("This is the duration of the sequence + any delay between the events.")]
    public float[] delayInbetween;
    public SequenceEvent[] sequenceEvents;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;

        if (hasMany)
        {
            StartCoroutine(ExecuteSequenceEvents());
            return;
        }

        SequenceManager.instance.OnEventTriggered(sequenceEvent);
    }

    IEnumerator ExecuteSequenceEvents()
    {
        for (int i = 0; i < sequenceEvents.Length; i++)
        {
            SequenceManager.instance.OnEventTriggered(sequenceEvents[i]);
            
            yield return new WaitForSeconds(delayInbetween[i]);
        }
    }
}

