using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

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
    public CharacterTargetDetectors CharacterTargetDetectors;

    [SerializeField] private CapsuleCollider hitBox;

    // stats
    private float hp = 0f;
    public float HP 
    { 
        get => hp;
        set
        {
            if (hp != value)
            {
                if(value <= 0 && Rage.isRaging)
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
        return Rage.isRaging ? Stats.attackMultiplier * GetStatsMultiplier() : Stats.attackMultiplier;
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
        return Rage.isRaging ? Stats.speedMultiplier * GetStatsMultiplier() : Stats.speedMultiplier;
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
        }

        if(startingFactionOverride)
        {
            Faction = startingFactionOverride;
        }
    }

    public void ReceiveDamage(float damage)
    {
        HP -= Rage.isRaging ? (damage / 2) : damage;
    }

    public void Die()
    {
        OnDeath?.Invoke(this);
    }

    private float GetStatsMultiplier()
    {
        if (Rage.isRaging)
        {
            var currHpMultipler = (hp / Stats.basicHP) + 1;
            return currHpMultipler > 3 ? currHpMultipler : 3;
        }
        else
        {
            return (hp / Stats.basicHP) + 1;
        }
    }
  
    private void ChangedFaction()
    {
        gameObject.tag = Faction.tag;
    }

    #endregion
}