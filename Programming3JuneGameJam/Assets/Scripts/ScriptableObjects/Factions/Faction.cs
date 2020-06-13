using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Faction", fileName = "new_faction")]
public class Faction : ScriptableObject
{
    [SerializeField] private List<Faction> allyFactions = new List<Faction>();
    [SerializeField] private List<Faction> enemyFactions = new List<Faction>();

    public bool IsAlly(Faction requestingFaction)
    {
        return allyFactions.Contains(requestingFaction);
    }

    public bool IsEnemy(Faction requestingFaction)
    {
        return enemyFactions.Contains(requestingFaction);
    }
}
