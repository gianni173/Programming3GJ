using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "character_stats", menuName = "Character/Stats")]
public class CharacterStats : ScriptableObject
{
    #region Fields
    
    public float basicHP;
    public int basicAtk;
    public int basicSpd;
    
    #endregion
}