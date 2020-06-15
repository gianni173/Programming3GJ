using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameTriggerable : MonoBehaviour
{
    [Title("Activations")]
    [SerializeField] private TriggerDetector[] triggerActivators = null;
    [SerializeField] private Spawner[] spawnerActivators = null;

    private bool hasBeenTriggered = false;

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
        foreach (TriggerDetector detector in triggerActivators)
        {
            detector.OnTriggerEntered += Trigger;
        }
        foreach (Spawner spawner in spawnerActivators)
        {
            spawner.OnWavesCompleted += SpawnerCompleted;
        }
    }

    [Button("Manual Trigger")]
    private void Trigger(Collider collidedObject)
    {
        if (!hasBeenTriggered)
        {
            GameManager.Instance.Win();
            hasBeenTriggered = true;
        }
    }

    private void SpawnerCompleted()
    {
        foreach (Spawner spawner in spawnerActivators)
        {
            if (spawner.isActive)
            {
                return;
            }
        }
        Trigger(null);
    }
}
