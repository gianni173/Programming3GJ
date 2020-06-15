using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEditor.SceneManagement;
using UnityEditorInternal;

public class Character : MonoBehaviour
{
    #region Fields

    public event Action<Character> OnDeath;
    public event Action<float, float> OnHealthChanged;
    public Action<Character, float> OnRageChanged;

    [NonSerialized] public Faction faction;
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
    [NonSerialized] public CharacterRage Rage;
    public CharacterAnimation Animation;
    public CharacterGraphic Graphic;
    public Weapon Weapon;
    public CharacterTargetDetectors CharacterTargetDetectors;

    private bool isDead = false;
    public bool IsDead
    {
        get => isDead;
        set
        {
            if (isDead != value)
            {
                isDead = value;
                Input.enabled = !isDead;
                Movement.enabled = !isDead;
                CharacterTargetDetectors.SetDetectorsActive(!isDead);
                CharacterTargetDetectors.enabled = !isDead;
                hitBox.enabled = !isDead;
                if (Rage)
                {
                    Rage.enabled = !isDead;
                };
                Graphic.SetActive(!isDead);
            }
        }
    }

    [SerializeField] private CapsuleCollider hitBox = null;
    [SerializeField] private GameObject rageCircle = null;

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
        return Stats.attackMultiplier * (GetStatsMultiplier() - 1);
    }


    private float spd = 0f;
    public float Spd
    {
        get => spd + (spd * GetSpeedMultiplier());
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
        return Stats.speedMultiplier * (GetStatsMultiplier() - 1);
    }

    #endregion

    #region Unity Callbacks

    private void Update()
    {
        if (Rage && !Rage.isRaging && HP > Stats.basicHP)
        {
            HP -= Stats.basicHP / 25 * Time.deltaTime;
            if (HP < Stats.basicHP)
            {
                HP = Stats.basicHP;
            }
        }
    }

    #endregion

    #region Methods

    public void InitCharacter(CharacterStats startingStatsOverride = null, Faction startingFactionOverride = null)
    {
        //Stats override
        if (startingStatsOverride)
        {
            Stats = startingStatsOverride;
        }

        if (Stats)
        {
            //Name
            gameObject.name = Stats.hierarchyName + " Character";
            //Faction
            if(Stats.basicFaction)
            {
                Faction = Stats.basicFaction;
            }
            //Basic stats
            HP = Stats.basicHP;
            Spd = Stats.basicSpd;
            //Hitbox size
            hitBox.radius = Stats.hitBoxRadius * Stats.sizeMultiplier;
            hitBox.height = Stats.hitBoxHeight * Stats.sizeMultiplier;
            hitBox.center = new Vector3(0, hitBox.height / 2, 0);
            //Input Type
            if (Stats.basicInputType)
            {
                Input.inputType = Stats.basicInputType;
                //Target detectors
                var aiInputType = Stats.basicInputType as AIInput;
                CharacterTargetDetectors.SetDetectorsActive(aiInputType != null);
                if (aiInputType != null)
                {
                    CharacterTargetDetectors.SetDetectorsRanges(aiInputType.visionRange, aiInputType.shootingRange);
                }
            }
            //Rage
            if (Stats.rageStats)
            {
                var rageComponent = gameObject.AddComponent<CharacterRage>();
                rageComponent.character = this;
                rageComponent.rageCircle = rageCircle;
                rageComponent.rageCircleAnim = rageCircle.GetComponent<Animator>();
                rageComponent.stats = Stats.rageStats;
                Rage = rageComponent;
            }
            //Model graphics
            if (Graphic)
            {
                Graphic.SetUpGraphic(Stats.modelKey);
                Graphic.gameObject.transform.localScale = Vector3.one * 2 * Stats.sizeMultiplier;
                Weapon.gameObject.transform.localScale = Vector3.one * Stats.sizeMultiplier;
                //Weapon graphics
                if (Stats.weaponPrefab)
                {
                    Graphic.SetWeapon(this);
                }
            }
            //Weapon firing system
            Weapon.firingMode = Stats.basicFiringMode;
            Weapon.projectileType = Stats.basicProjectileType;
            Weapon.enragedProjectileType = Stats.basicEnragedProjectileType;
        }

        //Faction override
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

        damageReceived *= hitInfos.isEnragedDamage ? 1 - Stats.defenceAgainstEnraged : 1 - Stats.defenceAgainstNotEnraged;

        HP -= damageReceived;
        return damageReceived;
    }

    public void Die()
    {
        OnDeath?.Invoke(this);
        IsDead = true;
    }

    private float GetStatsMultiplier()
    {
        if (Rage && Rage.isRaging)
        {
            var currHpMultipler = (int)((hp - .001f) / Stats.basicHP) + 1;
            return currHpMultipler > Rage.stats.baseRageMultiplier ? currHpMultipler : Rage.stats.baseRageMultiplier;
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
