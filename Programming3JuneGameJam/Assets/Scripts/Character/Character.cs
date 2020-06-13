using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Character : MonoBehaviour
{
    #region Fields

    public CharacterStats stats;
    [HideInInspector]
    public CharacterInput input;
    [HideInInspector]
    public CharacterMovement movement;
    
    // stats
    public float HP { get => hp; set => hp = value; }
    public int Atk { get => atk; set => atk = value; }
    public int Spd { get => spd; set => spd = value; }
    
    private float hp;
    private int atk;
    private int spd;
    
    #endregion
    
    #region UnityCallbacks

    private void Awake()
    {
        input = GetComponent<CharacterInput>();
        movement = GetComponent<CharacterMovement>();
    }

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