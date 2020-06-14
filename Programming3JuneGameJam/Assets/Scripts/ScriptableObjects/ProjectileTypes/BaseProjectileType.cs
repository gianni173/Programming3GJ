using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "base_projectile_type", menuName = "Shooting/ProjectileType/Basic", order = 0)]
public class BaseProjectileType : ProjectileType
{
    public override Projectile SpawnProjectile(Vector3 spawnPoint)
    {
        var instance = Instantiate(prefab, spawnPoint, Quaternion.identity);
        return instance.GetComponent<Projectile>();
    }

    public override void ProjectileBehaviour(Character characterHit, Projectile projectile)
    {
        characterHit.ReceiveDamage(projectile.damage);
    }
}
