using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(menuName = "Spawner/Wave", fileName = "new_wave")]
public class Wave : ScriptableObject
{
    public EntitiesSet[] entitiesSets;

    [System.Serializable]
    public struct EntitiesSet
    {
        public CharacterStats entityStats;
        public int amount;
    }
}
