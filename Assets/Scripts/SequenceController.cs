using UnityEngine;

public class SequenceController : MonoBehaviour
{
    public SequenceEvent sequenceEvent;
    public int id;

    // Apparently this doesn't work on the anchor point...
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") return;
        SequenceManager.instance.OnEventTriggered(sequenceEvent);
    }
}

