using UnityEngine;

public class SequenceController : MonoBehaviour
{
    public SequenceEvent sequenceEvent;
    private void OnTriggerEnter(Collider other)
    {
      if (!other.CompareTag("Player")) return;
      SequenceManager.instance.OnEventTriggered(sequenceEvent);
    }
}

