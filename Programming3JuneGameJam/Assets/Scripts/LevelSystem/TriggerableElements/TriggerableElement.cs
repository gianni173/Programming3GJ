using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerableElement : MonoBehaviour
{
    [Title("Activations")]
    [SerializeField] private bool startsActive = true;
    [SerializeField] private TriggerDetector[] triggerActivators = null;
    [SerializeField] private Spawner[] spawnerActivators = null;

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
        if (spawnerActivators != null)
        {
            var prevGizmosColor = Gizmos.color;

            var thisGizmoColor = Color.red;
            Gizmos.color = thisGizmoColor;

            foreach (TriggerDetector activator in triggerActivators)
            {
                Gizmos.DrawLine(transform.position + (Vector3.up * 2f), activator.transform.position + (Vector3.up * 2f));
            }
            foreach (Spawner activator in spawnerActivators)
            {
                Gizmos.DrawLine(transform.position + (Vector3.up * 2f), activator.transform.position + (Vector3.up * 2f));
            }

            Gizmos.color = prevGizmosColor;
        }
    }

    private void Start()
    {
        IsActive = startsActive;
        foreach (TriggerDetector detector in triggerActivators)
        {
            detector.OnTriggerEntered += Activate;
        }
        foreach (Spawner spawner in spawnerActivators)
        {
            spawner.OnWavesCompleted += SpawnerCompleted;
        }
    }

    private void Activate(Collider collidedObject)
    {
        IsActive = !IsActive;
    }

    private void SpawnerCompleted()
    {
        foreach (Spawner spawner in spawnerActivators)
        {
            if(spawner.isActive)
            {
                return;
            }
        }
        IsActive = !IsActive;
    }
}
