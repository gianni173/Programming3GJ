using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Damage/Type", fileName = "new_damage_type")]
public class DamageType : ScriptableObject
{
    public float multiplierAgainstNotEnraged = 1f;
    public float multiplierAgainstEnraged = 1f;
}

public struct HitInfos
{
    public float damage;
    public DamageType damageType;
    public bool isEnragedDamage;
}
