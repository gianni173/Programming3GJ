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
    public bool isPlayer;
    
    // stats
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
    public int Atk { get => atk; set => atk = value; }
    public int Spd { get => spd; set => spd = value; }

    private float hp;
    private int atk;
    private int spd;
    
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