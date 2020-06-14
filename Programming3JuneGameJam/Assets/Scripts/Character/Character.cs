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

    [Required] public Faction faction;
    public CharacterStats stats;
    public CharacterInput input;
    public CharacterMovement movement;

    // stats
    private float hp = 0f;
    public float HP 
    { 
        get => hp;
        set
        {
            if (hp != value)
            {
                hp = value;
                OnHealthChanged?.Invoke(hp, stats.basicHP);
                if(hp < 0)
                {
                    OnDeath?.Invoke(this);
                }
            }
        }
    }

    private float atk = 0f;
    public float Atk 
    {
        get => atk;
        set 
        {
            if(atk != value)
            {
                atk = value;
            }
        }
    }

    private float spd = 0f;
    public float Spd
    {
        get => spd;
        set
        {
            if (spd != value)
            {
                spd = value;
            }
        }
    }

    #endregion

    #region UnityCallbacks

    private void Start()
    {
        InitCharacter();
    }

    #endregion
    
    #region Methods

    public void InitCharacter()
    {
        // set stats
        HP = stats.basicHP;
        Atk = stats.basicAtk;
        Spd = stats.basicSpd;
    }

    #endregion
}