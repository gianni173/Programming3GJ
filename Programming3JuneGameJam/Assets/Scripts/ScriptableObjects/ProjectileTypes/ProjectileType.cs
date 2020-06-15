using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileType : ScriptableObject
{
    #region Fields

    public GameObject prefab;
    public GameObject hitParticlePrefab;
    public float speed = 20f;
    public float baseDamage = 10f;
    public DamageType damageType = null;
    public float lifeTime = 5f;
    public bool isEnraged = false;

    #endregion

    #region Methods

    public abstract Projectile SpawnProjectile(Vector3 spawnPoint);

    public abstract float ProjectileBehaviour(Character characterHit, Projectile projectile);

    #endregion
}