using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    #region Fields

    [SerializeField] private Character character = null;
    [Required, SerializeField] private Animator animator = null;

    private const string PosXParam = "PosX";
    private const string PosZParam = "PosY";
    private const string IsAimingParam = "IsAiming";
    private const string LightWeaponParam = "LightWeapon";
    private const string NormalWeaponParam = "NormalWeapon";
    private const string HeavyWeaponParam = "HeavyWeapon";

    #endregion

    #region Methods

    public void SetMovementDirection(Vector3 direction)
    {
        var normalizedDirection = direction.normalized;

        var rightMovement = Vector3.Dot(normalizedDirection, character.transform.right);
        var forwardMovement = Vector3.Dot(normalizedDirection, character.transform.forward);

        var relativeDirection = new Vector3(rightMovement, 0, forwardMovement).normalized;

        animator.SetFloat(PosXParam, relativeDirection.x);
        animator.SetFloat(PosZParam, relativeDirection.z);
    }

    public void IsAiming(bool isAiming)
    {
        animator.SetBool(IsAimingParam, isAiming);
    }

    public void SetWeaponType(CharacterStats.WeaponType weaponType)
    {
        switch (weaponType)
        {
            case CharacterStats.WeaponType.Light:
                animator.SetBool(LightWeaponParam, true);
                break;
            
            case CharacterStats.WeaponType.Normal:
                animator.SetBool(NormalWeaponParam, true);
                break;
            
            case CharacterStats.WeaponType.Heavy:
                animator.SetBool(HeavyWeaponParam, true);
                break;
        }
    }

    #endregion
}
