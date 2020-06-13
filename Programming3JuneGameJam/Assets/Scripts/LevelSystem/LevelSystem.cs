using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : Singleton<LevelSystem>
{
    public event Action<Map> OnMapLoaded;

    [SerializeField] private GameObject mapPrefab = null;
    [SerializeField] private Transform mapContainer = null;

    private void Start()
    {
        OnMapLoaded?.Invoke(LoadMap());
    }

    private Map LoadMap()
    {
        var newMap = Instantiate(mapPrefab, mapContainer);
        var map = newMap.GetComponent<Map>(); 
        return map;
    }
}
