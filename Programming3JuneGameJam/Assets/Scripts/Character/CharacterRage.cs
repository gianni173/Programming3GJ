using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRage : MonoBehaviour
{
    public RageStats stats;

    [NonSerialized] public Character character = null;
    [NonSerialized] public bool isRaging = false;

    private float currRageTime = 0f;

    private void Update()
    {
        if(isRaging && currRageTime > 0)
        {
            currRageTime -= Time.deltaTime;
            if(currRageTime <= 0)
            {
                currRageTime = 0;
                StopRage();
            }
        }
    }

    [Button("Start")]
    public void StartRage()
    {
        isRaging = true;
        currRageTime = stats.rageTime;
    }

    [Button("Stop")]
    private void StopRage()
    {
        isRaging = false;
        if(character.HP / character.Stats.basicHP < stats.normalizedThreshold)
        {
            character.Die();
        }
    }
}
