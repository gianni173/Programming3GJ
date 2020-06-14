using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileType : ScriptableObject
{
    #region Fields

    public GameObject prefab;
    public float speed = 20f;
    public float baseDamage = 10f;
    public float lifeTime = 5f;

    #endregion

    #region Methods

    public abstract Projectile SpawnProjectile(Vector3 spawnPoint);

    public abstract void ProjectileBehaviour(Character characterHit, Projectile projectile);

    #endregion
}