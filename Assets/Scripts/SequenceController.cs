using UnityEngine;

public class SequenceController : MonoBehaviour
{
    public SequenceEvent sequenceEvent;

    // Apparently this doesn't work on the anchor point...
    private void OnTriggerEnter(Collider other)
    {
      Debug.Log($"Other1: {other.gameObject.name}");
      if (other.tag != "Player") return;
      Debug.Log($"Other: {other.gameObject.name}");
      SequenceManager.instance.OnEventTriggered(sequenceEvent);
    }

    // Workaround for the anchor point.
    public void PlayerEntered()
    {
      SequenceManager.instance.OnEventTriggered(sequenceEvent);
    }
}

