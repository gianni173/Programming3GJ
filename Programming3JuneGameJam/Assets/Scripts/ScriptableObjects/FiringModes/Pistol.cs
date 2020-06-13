using UnityEngine;

[CreateAssetMenu(fileName = "pistol", menuName = "FiringMode/Pistol", order = 0)]
public class Pistol : FiringMode
{
    #region Methods

    public override void Shoot(Transform firingPoint, ProjectileType type)
    {
        var projectile = type.SpawnProjectile(firingPoint.position);
        projectile.type = type;
        projectile.Push(firingPoint.forward);
    }

    #endregion
}