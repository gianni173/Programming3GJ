using System;
using UnityEngine;

public class FiringMode : ScriptableObject
{
    #region Fields

    public float bulletsPerSecond = 1f;

    #endregion

    #region Methods

    public virtual void Shoot(Transform firingPoint, ProjectileType projectile, Faction ownerFaction) { }

    #endregion
}