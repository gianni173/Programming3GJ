using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    #region Fields

    public Transform firingPoint;
    public FiringMode firingMode;
    public ProjectileType projectileType;
    public ProjectileType enragedProjectileType;

    [SerializeField] private Character character = default;

    private float lastShootTime = 0f;

    #endregion

    #region Methods

    public void Shoot()
    {
        if (character.Rage && character.Rage.isRaging)
        {
            if (firingMode && enragedProjectileType)
            {
                firingMode.Shoot(firingPoint, enragedProjectileType, character);
            }
        }
        else
        {
            if (firingMode && projectileType)
            {
                firingMode.Shoot(firingPoint, projectileType, character);
            }
        }
        if(character.Stats.playsShootSound && character.Stats.shootSound)
        {
            SoundPlayer.Instance?.Play(character.Stats.shootSound);
        }
        lastShootTime = Time.time;
    }

    public bool CanShoot()
    {
        if (character && firingMode)
        {
            var bps = firingMode.bulletsPerSecond;
            if(character.Rage && character.Rage.isRaging)
            {
                 bps = firingMode.enragedBulletsPerSecond;
            }
            return lastShootTime + (1 / bps) < Time.time;
        }
        return false;
    }


    #endregion
}
