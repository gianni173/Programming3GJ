using UnityEngine;

[CreateAssetMenu(fileName = "spread_mode", menuName = "FiringMode/Spread", order = 0)]
public class SpreadMode : FiringMode
{
    public int bulletsPerSpread = 3;
    public float angle = 10f;
    
    public override void Shoot(Transform firingPoint, ProjectileType type, Character owner)
    {
        for (int i = 0; i < bulletsPerSpread; i++)
        {
            var rot = Quaternion.AngleAxis(angle * i - angle * ((bulletsPerSpread - 1) / 2f), Vector3.up);
            var projectile = type.SpawnProjectile(firingPoint.position);
            projectile.damage = type.baseDamage * owner.GetAttackMultiplier();
            projectile.type = type;
            projectile.owner = owner;
            projectile.SetDirection(rot * firingPoint.forward);
        }
    }
}