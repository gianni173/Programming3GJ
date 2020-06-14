﻿using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "character_stats", menuName = "Character/Stats")]
public class CharacterStats : ScriptableObject
{
    [Title("Character Name")]
    public string hierarchyName = "Base";
    [Space(5), Title("Combat Stats")]
    public float basicHP;
    public float attackMultiplier = 0f;
    public float basicSpd;
    public float speedMultiplier = 0f;
    public float hitBoxHeight = 2f;
    public float hitBoxRadius = .75f;
    [Space(5), Title("Input Stats")]
    public InputType basicInputType;
    [Space(5), Title("Faction Stats")]
    public Faction basicFaction;
}