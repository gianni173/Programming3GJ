using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDetector : MonoBehaviour
{
    public event Action OnTriggered;

    [SerializeField] private List<string> detectedTags = new List<string>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(detectedTags.Contains(collision.tag))
        {
            OnTriggered?.Invoke();
        }
    }
}
