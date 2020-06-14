using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class FollowTarget : MonoBehaviour
{
    #region Fields

    public Transform target = null;
    [SerializeField] private float smoothSpeed = 0.25f;
    [SerializeField] private Vector3 offset = Vector3.zero;

    #endregion

    #region UnityCallbacks

    private void FixedUpdate()
    {
        if (target)
        {
            Vector3 newPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, newPosition, smoothSpeed * Time.fixedDeltaTime);
            transform.position = smoothedPosition;
        }
    }

    #endregion

    #region Methods

    public void Snap()
    {
        transform.position = target.position + offset;
    }

    #endregion
}
