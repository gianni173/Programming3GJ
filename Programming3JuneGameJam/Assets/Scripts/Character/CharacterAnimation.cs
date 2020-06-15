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

    //layers
    private const string BaseLayer = "BaseLayer";
    private const string UpperBodyLayer = "UpperBody";
    private const string LowerBodyLayer = "LowerBody";
    
    //params
    private const string PosXParam = "PosX";
    private const string PosZParam = "PosY";
    private const string IsAimingParam = "IsAiming";
    private const string LightWeaponParam = "LightWeapon";
    private const string NormalWeaponParam = "NormalWeapon";
    private const string HeavyWeaponParam = "HeavyWeapon";
    private const string RageParam = "Rage";
    private const string DeathParam = "Death";

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

    public void Rage()
    {
        animator.SetTrigger(RageParam);
    }

    public void Death()
    {
        animator.SetTrigger(DeathParam);
    }

    public void SetWeightSecondaryLayers(float weight)
    {
        int layerIndex = animator.GetLayerIndex(UpperBodyLayer);
        animator.SetLayerWeight(layerIndex, weight);
        layerIndex = animator.GetLayerIndex(LowerBodyLayer);
        animator.SetLayerWeight(layerIndex, weight);
    }

    #endregion
}
