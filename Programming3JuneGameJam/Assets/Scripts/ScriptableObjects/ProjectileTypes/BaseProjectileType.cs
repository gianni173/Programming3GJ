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

    public override float ProjectileBehaviour(Character characterHit, Projectile projectile)
    {
        if (hitParticlePrefab)
        {
            Instantiate(hitParticlePrefab, projectile.transform.position, Quaternion.identity);
        }
        var actualDamageInflicted = characterHit.ReceiveDamage(
                                                    new HitInfos
                                                    {
                                                        damage = projectile.damage,
                                                        damageType = projectile.type.damageType,
                                                        isEnragedDamage = projectile.type.isEnraged,
                                                    });
        return actualDamageInflicted;
    }
}
