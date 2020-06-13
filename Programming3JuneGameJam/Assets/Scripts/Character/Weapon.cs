using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    #region Fields

    public Transform firingPoint;

    [SerializeField] private Character character;
    [SerializeField] private FiringMode firingMode;
    [SerializeField] private ProjectileType projectileType;

    private float lastShootTime = 0f;

    #endregion

    #region Methods

    public void Shoot()
    {
        firingMode.Shoot(firingPoint, projectileType, character.faction);
        lastShootTime = Time.time;
    }

    public bool CanShoot()
    {
        return lastShootTime + (1 / firingMode.bulletsPerSecond) < Time.time;
    }


    #endregion
}
