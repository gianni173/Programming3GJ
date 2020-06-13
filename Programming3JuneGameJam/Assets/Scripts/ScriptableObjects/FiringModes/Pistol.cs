using UnityEngine;

[CreateAssetMenu(fileName = "pistol", menuName = "FiringMode/Pistol", order = 0)]
public class Pistol : FiringMode
{
    #region Methods

    public override void Shoot(Transform firingPoint, ProjectileType type, Faction ownerFaction)
    {
        var projectile = type.SpawnProjectile(firingPoint.position);
        projectile.type = type;
        projectile.ownerFaction = ownerFaction;
        projectile.Push(firingPoint.forward);
    }

    #endregion
}