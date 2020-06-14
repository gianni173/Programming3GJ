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
    [NonSerialized] public GameObject rageCircle = null;
    [NonSerialized] public Animator rageCircleAnim = null;

    private float currRageTime = 0f;

    private void Update()
    {
        if(isRaging && currRageTime > 0)
        {
            currRageTime -= Time.deltaTime;
            if (currRageTime <= 3)
            {
                rageCircleAnim.SetBool("IsEnding", true);
                if (currRageTime <= 0)
                {
                    currRageTime = 0;
                    StopRage();
                }
            }
        }
    }

    [Button("Start")]
    public void StartRage()
    {
        isRaging = true;
        rageCircle.SetActive(isRaging);
        rageCircleAnim.SetBool("IsRaging", isRaging);
        character.OnRageChanged?.Invoke(character, currRageTime / stats.rageTime);
        currRageTime = stats.rageTime;
    }

    [Button("Stop")]
    private void StopRage()
    {
        isRaging = false;
        rageCircleAnim.SetBool("IsRaging", isRaging);
        rageCircle.SetActive(isRaging);
        character.OnRageChanged?.Invoke(character, currRageTime / stats.rageTime);
        if(character.HP / character.Stats.basicHP < stats.normalizedThreshold)
        {
            character.Die();
        }
    }
}
