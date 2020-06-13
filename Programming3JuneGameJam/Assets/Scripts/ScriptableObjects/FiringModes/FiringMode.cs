using System;
using UnityEngine;

public class FiringMode : ScriptableObject
{
    #region Fields
    
    
    
    #endregion

    #region Methods

    public virtual void Shoot(Transform firingPoint, ProjectileType projectile, bool isPlayer)
    {}

    #endregion
}