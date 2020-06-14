using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Rage/Stats", fileName = "new_rage_stats")]
public class RageStats : ScriptableObject
{
    public float damageReceivedModifier = .5f;
    public float normalizedThreshold = .5f;
    public float rageDrainMultiplier = .3f;
    public float rageTime = 5f; 
}
