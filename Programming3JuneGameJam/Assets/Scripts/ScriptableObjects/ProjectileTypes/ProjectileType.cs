using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "projectile_type", menuName = "Shooting/ProjectileType", order = 0)]
public class ProjectileType : ScriptableObject
{
    #region Fields

    public GameObject prefab;
    public float damage = 10f;
    public float speed = 20f;
    public float lifeTime = 5f;

    #endregion

    #region Methods
    
    public virtual ProjectileControl SpawnProjectile(Vector3 spawnPoint)
    {
        var instance = Instantiate(prefab, spawnPoint, Quaternion.identity);
        return instance.GetComponent<ProjectileControl>();
    }

    public virtual void ProjectileBehaviour()
    {}

    #endregion
}