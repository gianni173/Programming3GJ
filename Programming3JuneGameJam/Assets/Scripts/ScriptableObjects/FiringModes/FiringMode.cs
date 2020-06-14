using System;
using UnityEngine;

public class FiringMode : ScriptableObject
{
    #region Fields

    public float bulletsPerSecond = 1f;
    public float enragedBulletsPerSecond = 2f;

    #endregion

    #region Methods

    public virtual void Shoot(Transform firingPoint, ProjectileType projectile, Character owner) { }

    #endregion
}