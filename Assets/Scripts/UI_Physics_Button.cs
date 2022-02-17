using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UI_Physics_Button : MonoBehaviour
{
    public UnityEvent onPressed, onReleased;

    [SerializeField] private float threshold = 0.1f;
    [SerializeField] private float deadZone = 0.03f;

    private bool isPressed = false; // State management.
    private Vector3 startPos;
    private ConfigurableJoint joint; // The joint, will get the linear limit from that.

    private void Start()
    {
        startPos = transform.localPosition;
        joint = GetComponent<ConfigurableJoint>();
    }

    private void Update()
    {
        if (!isPressed && GetValue() + threshold >= 1)
            Pressed();
        else if (isPressed && GetValue() - threshold <= 0)
            Released();
    }

    private float GetValue()
    {

        // Normalizes the value between -1 and 1.
        float value = Vector3.Distance(startPos, transform.localPosition) / joint.linearLimit.limit;

        if (Math.Abs(value) < deadZone)
            return 0;

        return Mathf.Clamp(value, -1, 1);
    }

    private void Pressed()
    {
        isPressed = true;
        onPressed.Invoke();
        Debug.Log("Pressed");
    }

    private void Released()
    {
        isPressed = false;
        onReleased.Invoke();
        Debug.Log("Released");
    }

}
