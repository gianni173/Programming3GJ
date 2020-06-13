using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDetector : MonoBehaviour
{
    public event Action<Collider> OnTriggerEntered;
    public event Action<Collider> OnTriggerExited;

    public enum ColliderShape
    {
        Box,
        Sphere,
    }

    [SerializeField] private List<string> detectedTags = new List<string>();
    [SerializeField] private Collider detectionArea = null;
    [SerializeField] private bool showsGizmos = true;
    [SerializeField, ShowIf("showsGizmos")] private Color gizmoColor = new Color(1, 1, 1, 1);
    [SerializeField, ShowIf("showsGizmos")] private ColliderShape detectionAreaShape = ColliderShape.Box;
    [SerializeField] private bool isActive = true;
    [SerializeField] private bool deactivatesWhenDetect = true;

    private void OnDrawGizmos()
    {
        if (showsGizmos)
        {
            if (detectionArea)
            {
                var prevGizmosColor = Gizmos.color;

                var thisGizmoColor = gizmoColor;
                thisGizmoColor.a = isActive ? gizmoColor.a : gizmoColor.a / 2;
                Gizmos.color = thisGizmoColor;
                switch(detectionAreaShape)
                {
                    case ColliderShape.Box:
                        Gizmos.DrawCube(detectionArea.bounds.center, detectionArea.bounds.size);
                        break;
                    case ColliderShape.Sphere:
                        var sphereArea = detectionArea as SphereCollider;
                        if (sphereArea)
                        {
                            Gizmos.DrawSphere(detectionArea.bounds.center, sphereArea.radius);
                        }
                        break;
                }

                Gizmos.color = prevGizmosColor;
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(detectedTags.Contains(collision.tag))
        {
            TiggerEnter(collision);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (detectedTags.Contains(collision.tag))
        {
            TiggerExit(collision);
        }
    }

    [Button("Manual Trigger Enter")]
    private void TiggerEnter(Collider collision)
    {
        OnTriggerEntered?.Invoke(collision);
        if (deactivatesWhenDetect)
        {
            isActive = false;
        }
    }

    private void TiggerExit(Collider collision)
    {
        OnTriggerExited?.Invoke(collision);
    }
}
