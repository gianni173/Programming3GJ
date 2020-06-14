using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : Singleton<LevelSystem>
{
    public event Action OnMapLoaded;

    [NonSerialized] public Map loadedMap = null;

    [SerializeField] private GameObject mapPrefab = null;
    [SerializeField] private Transform mapContainer = null;
    
    private void Start()
    {
        StartCoroutine(DelayedMapLoad());
    }

    private IEnumerator DelayedMapLoad()
    {
        yield return new WaitForSeconds(.5f);
        loadedMap = LoadMap();
        OnMapLoaded?.Invoke();
    }

    private Map LoadMap()
    {
        var newMap = Instantiate(mapPrefab, mapContainer);
        var map = newMap.GetComponent<Map>();
        return map;
    }
}
