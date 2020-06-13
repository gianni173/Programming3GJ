using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDetector : MonoBehaviour
{
    public event Action OnTriggered;

    [SerializeField] private List<string> detectedTags = new List<string>();
    [SerializeField] private BoxCollider detectionArea = null;
    [SerializeField] private bool isActive = true;
    [SerializeField] private bool deactivatesWhenDetect = true;

    private void OnDrawGizmos()
    {
        if (detectionArea)
        {
            var prevGizmosColor = Gizmos.color;

            var thisGizmoColor = Color.blue;
            thisGizmoColor.a = isActive ? .4f : .2f;
            Gizmos.color = thisGizmoColor;
            Gizmos.DrawCube(detectionArea.bounds.center, detectionArea.bounds.size);

            Gizmos.color = prevGizmosColor;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(detectedTags.Contains(collision.tag))
        {
            Tigger();
        }
    }

    [Button("Manual Trigger")]
    private void Tigger()
    {
        OnTriggered?.Invoke();
        if (deactivatesWhenDetect)
        {
            isActive = false;
        }
    }
}
