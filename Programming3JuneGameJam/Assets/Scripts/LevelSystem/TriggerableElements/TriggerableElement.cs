using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerableElement : MonoBehaviour
{
    [Title("Activations")]
    [SerializeField] private bool startsActive = true;
    [SerializeField] private TriggerDetector[] activators = null;

    [Space(5), Title("Element Behaviour")]
    [SerializeField] private Animator anim = null;

    private bool isActive = false;
    private bool IsActive
    {
        get => isActive;
        set
        {
            if (isActive != value)
            {
                isActive = value;
                anim.SetBool("IsActive", isActive);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (activators != null)
        {
            var prevGizmosColor = Gizmos.color;

            var thisGizmoColor = Color.red;
            Gizmos.color = thisGizmoColor;

            foreach (TriggerDetector activator in activators)
            {
                Gizmos.DrawLine(transform.position + (Vector3.up * 2f), activator.transform.position + (Vector3.up * 2f));
            }

            Gizmos.color = prevGizmosColor;
        }
    }

    private void Start()
    {
        IsActive = startsActive;
        foreach (TriggerDetector detector in activators)
        {
            detector.OnTriggerEntered += Activate;
        }
    }

    private void Activate(Collider collidedObject)
    {
        IsActive = !IsActive;
    }
}
