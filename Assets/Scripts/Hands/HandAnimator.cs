using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandAnimator : MonoBehaviour
{
    [SerializeField] private InputActionReference gripAction;
    [SerializeField] private InputActionReference pointAction;
    [SerializeField] private InputActionReference indexAction;
    
    public float speed = 0.05f;

    public Animator animator = null;

    private readonly List<Finger> gripfingers = new List<Finger>()
    {
        new Finger(FingerType.Middle),
        new Finger(FingerType.Ring),
        new Finger(FingerType.Pinky)
    };

    private readonly List<Finger> pointFingers = new List<Finger>()
    {
        new Finger(FingerType.Thumb)
    };

    private readonly List<Finger> indexFingers = new List<Finger>()
    {
        new Finger(FingerType.Index)
    };

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Store input
        CheckGrip();
        CheckPointer();
        CheckIndex();

        // Smooth input values
        SmoothFinger(gripfingers);
        SmoothFinger(pointFingers);
        SmoothFinger(indexFingers);

        // Apply smoothed values
        AnimateFinger(gripfingers);
        AnimateFinger(pointFingers);
        AnimateFinger(indexFingers);
    }

    private void CheckGrip()
    {
        SetFingerTargets(gripfingers, gripAction.action.ReadValue<float>());
    }

    private void CheckPointer()
    {
        SetFingerTargets(pointFingers, pointAction.action.ReadValue<float>());
    }

    private void CheckIndex()
    {
        SetFingerTargets(indexFingers, indexAction.action.ReadValue<float>());
    }

    private void SetFingerTargets(List<Finger> fingers, float value)
    {
        foreach (Finger finger in fingers)
            finger.target = value;
    }

    private void SmoothFinger(List<Finger> fingers)
    {
        foreach (Finger finger in fingers)
        {
            float time = Time.unscaledDeltaTime * speed;
            finger.current = Mathf.MoveTowards(finger.current, finger.target, time);
        }
    }

    private void AnimateFinger(List<Finger> fingers)
    {
        foreach (Finger finger in fingers)
        {
            AnimateFinger(finger.type.ToString(), finger.current);
        }
    }

    private void AnimateFinger(string finger, float blend)
    {
        animator.SetFloat(finger, blend);
    }
}