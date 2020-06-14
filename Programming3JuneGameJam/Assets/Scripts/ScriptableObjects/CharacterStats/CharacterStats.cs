using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "character_stats", menuName = "Character/Stats")]
public class CharacterStats : ScriptableObject
{
    public float basicHP;
    public float basicAtk;
    public float basicSpd;
}