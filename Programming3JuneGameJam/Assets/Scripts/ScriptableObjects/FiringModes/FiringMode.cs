using System;
using UnityEngine;

public abstract class FiringMode : ScriptableObject
{
    #region Fields

    public float bulletsPerSecond = 1f;
    public float enragedBulletsPerSecond = 2f;

    #endregion

    #region Methods

    public abstract void Shoot(Transform firingPoint, ProjectileType type, Character owner);

    #endregion
}