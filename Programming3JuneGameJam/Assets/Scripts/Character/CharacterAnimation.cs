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
    private const string PosYParam = "PosY";
    private const string IsAimingParam = "IsAiming";
    private const string LightWeaponParam = "LightWeapon";
    private const string NormalWeaponParam = "NormalWeapon";
    private const string HeavyWeaponParam = "HeavyWeapon";

    #endregion

    #region Methods

    public void SetMovementDirection(Vector3 direction)
    {
        Vector3 forward = character.transform.forward;
        Vector2 normalizedDirection = new Vector3(direction.x * forward.x, direction.z * forward.z);
        Debug.Log(normalizedDirection);
        float posX = Math.Abs(normalizedDirection.x) > 0.05f ? Mathf.Sign(normalizedDirection.x) : 0f;
        float posY = Math.Abs(normalizedDirection.y) > 0.05f ? Mathf.Sign(normalizedDirection.y) : 0f;
        animator.SetFloat(PosXParam, posX);
        animator.SetFloat(PosYParam, posY);
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
