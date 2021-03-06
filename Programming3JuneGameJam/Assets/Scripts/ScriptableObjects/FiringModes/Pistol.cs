using UnityEngine;

[CreateAssetMenu(fileName = "pistol", menuName = "FiringMode/Pistol", order = 0)]
public class Pistol : FiringMode
{
    #region Methods

    public override void Shoot(Transform firingPoint, ProjectileType type, Character owner)
    {
        var projectile = type.SpawnProjectile(firingPoint.position);
        projectile.damage = type.baseDamage * (owner.GetAttackMultiplier() + 1);
        projectile.type = type;
        projectile.owner = owner;
        projectile.SetDirection(firingPoint.forward);
    }

    #endregion
}