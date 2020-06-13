using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "Spawner/Wave", menuName = "new_wave")]
public class Wave : ScriptableObject
{
    public EntitiesSet[] entitiesSets;

    public struct EntitiesSet
    {
        public GameObject prefab;
        public int amount;
    }
}
