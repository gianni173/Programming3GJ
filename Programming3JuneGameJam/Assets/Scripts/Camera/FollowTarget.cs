﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class FollowTarget : MonoBehaviour
{
    #region Fields

    [SerializeField] private Transform target;
    [SerializeField] private float smoothSpeed = 0.25f;
    [SerializeField] private Vector3 offset;

    #endregion

    #region UnityCallbacks

    private void LateUpdate()
    {
        Vector3 newPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, newPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }

    #endregion

    #region Methods



    #endregion
}