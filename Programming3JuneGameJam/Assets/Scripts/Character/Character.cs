﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEditor.SceneManagement;

public class Character : MonoBehaviour
{
    #region Fields

    public event Action<Character> OnDeath;
    public event Action<float, float> OnHealthChanged;

    public Faction faction;
    public Faction Faction
    {
        get => faction;
        set
        {
            if(faction != value)
            {
                faction = value;
                ChangedFaction();
            }
        }
    }
    public CharacterStats Stats;
    public CharacterInput Input;
    public CharacterMovement Movement;
    public CharacterRage Rage;
    public Weapon Weapon;
    public CharacterTargetDetectors CharacterTargetDetectors;

    [NonSerialized] public bool isDead = false;

    [SerializeField] private CapsuleCollider hitBox = null;

    // stats
    private float hp = 0f;
    public float HP 
    { 
        get => hp;
        set
        {
            if (hp != value)
            {
                if(Rage && value <= 0 && Rage.isRaging)
                {
                    value = 1;
                }
                hp = value;
                OnHealthChanged?.Invoke(hp, Stats.basicHP);
                if(hp <= 0)
                {
                    if (!Rage)
                    {
                        Die();
                    }
                    else
                    {
                        Rage.StartRage();
                    }
                }
            }
        }
    }

    public float GetAttackMultiplier()
    {
        return Stats.attackMultiplier * GetStatsMultiplier();
    }


    private float spd = 0f;
    public float Spd
    {
        get => spd * GetSpeedMultiplier();
        set
        {
            if (spd != value)
            {
                spd = value;
            }
        }
    }

    public float GetSpeedMultiplier()
    {
        return Stats.speedMultiplier * GetStatsMultiplier();
    }

    #endregion
    
    #region Methods

    public void InitCharacter(CharacterStats startingStatsOverride = null, Faction startingFactionOverride = null)
    {
        if (startingStatsOverride)
        {
            Stats = startingStatsOverride;
        }
        if (Stats)
        {
            gameObject.name = Stats.hierarchyName + " Character";
            if(Stats.basicFaction)
            {
                Faction = Stats.basicFaction;
            }
            HP = Stats.basicHP;
            Spd = Stats.basicSpd;
            hitBox.height = Stats.hitBoxHeight;
            hitBox.center = new Vector3(0, hitBox.height / 2, 0);
            hitBox.radius = Stats.hitBoxRadius;
            if (Stats.basicInputType)
            {
                Input.inputType = Stats.basicInputType;
                var aiInputType = Stats.basicInputType as AIInput;
                CharacterTargetDetectors.SetDetectorsActive(aiInputType != null);
                if (aiInputType != null)
                {
                    CharacterTargetDetectors.SetDetectorsRanges(aiInputType.visionRange, aiInputType.shootingRange);
                }
            }
            if (Stats.rageStats)
            {
                var rageComponent = gameObject.AddComponent<CharacterRage>();
                rageComponent.character = this;
                rageComponent.stats = Stats.rageStats;
                Rage = rageComponent;
            }
            Weapon.firingMode = Stats.basicFiringMode ? 
                Stats.basicFiringMode : Weapon.firingMode;
            Weapon.projectileType = Stats.basicProjectileType ? 
                Stats.basicProjectileType : Weapon.projectileType;
            Weapon.enragedProjectileType = Stats.basicEnragedProjectileType ? 
                Stats.basicEnragedProjectileType : Weapon.enragedProjectileType;
        }

        if(startingFactionOverride)
        {
            Faction = startingFactionOverride;
        }
    }

    public void DamageInflicted(float damageInflicted)
    {
        if(Rage && Rage.isRaging)
        {
            HP += damageInflicted * Rage.stats.rageDrainMultiplier;    
        }
    }

    public float ReceiveDamage(HitInfos hitInfos)
    {
        //Enraged Reduction
        var damageReceived = hitInfos.damage;
        if (Rage && Rage.isRaging)
        {
            damageReceived *= Rage.stats.damageReceivedModifier;
            damageReceived *= hitInfos.damageType.multiplierAgainstEnraged;
        }
        else
        {
            damageReceived *= hitInfos.damageType.multiplierAgainstNotEnraged;
        }
        
        HP -= damageReceived;
        return damageReceived;
    }

    public void Die()
    {
        OnDeath?.Invoke(this);
        isDead = true;

        Input.enabled = false;
        Movement.enabled = false;
        CharacterTargetDetectors.SetDetectorsActive(false);
        CharacterTargetDetectors.enabled = false;
        if (Rage)
        {
            Rage.enabled = false;
        };
    }

    private float GetStatsMultiplier()
    {
        if (Rage && Rage.isRaging)
        {
            var currHpMultipler = (int)((hp - .001f) / Stats.basicHP) + 1;
            return currHpMultipler > 3 ? currHpMultipler : 3;
        }
        else
        {
            return (int)((hp - .001f) / Stats.basicHP) + 1;
        }
    }
  
    private void ChangedFaction()
    {
        gameObject.tag = Faction.tag;
    }

    #endregion
}