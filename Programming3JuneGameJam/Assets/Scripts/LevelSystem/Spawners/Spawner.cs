using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    [Title("Activations")]
    [SerializeField] private bool startsActive = true;
    [SerializeField] private TriggerDetector[] activators = null;

    [Space(5), Title("Spawner Behaviour")]
    [SerializeField] private float spawnerPulse = 1f;
    [SerializeField] private Wave[] waves = null;
    [SerializeField] private BoxCollider2D spawnArea = null;
    [SerializeField] private Transform entitiesSpawnedContainer = null;
    [SerializeField] private List<Character> entitiesSpawned = new List<Character>();

    private bool isActive = false;
    private bool IsActive
    {
        get
        {
            return isActive;
        }
        set
        {
            if(isActive != value)
            {
                isActive = value;
                if(isActive)
                {
                    SpawnWave();
                }
            }
        }
    }

    private int currWave = 0;
    private float lastSpawnTime = 0;

    private void OnDrawGizmos()
    {
        if (spawnArea)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(spawnArea.bounds.center, spawnArea.bounds.size);
        }
    }

    private void Start()
    {
        IsActive = startsActive;
        foreach(TriggerDetector detector in activators)
        {
            detector.OnTriggered += Activate;
        }
    }

    private void Update()
    {
        if(IsActive)
        {
            if(Time.time > lastSpawnTime + spawnerPulse)
            {
                SpawnWave();
            }
        }
    }

    private void Activate()
    {
        if(!IsActive)
        {
            IsActive = true;
        }
    }

    private void SpawnWave()
    {
        lastSpawnTime = Time.time;
        if (waves.Length > currWave)
        {
            foreach (Wave.EntitiesSet set in waves[currWave].entitiesSets)
            {
                for (int i = 0; i < set.amount; i++)
                {
                    if (set.prefab)
                    {
                        SpawnEntity(set.prefab, GetRandomPosInSpawnArea());
                    }
                }
            }

            currWave++;
            if(currWave <= waves.Length)
            {
                isActive = false;
            }
        }
    }

    private void SpawnEntity(GameObject prefab, Vector3 position)
    {
        var newEntity = Instantiate(prefab, position, Quaternion.identity, entitiesSpawnedContainer);
        newEntity.transform.Rotate(Vector3.up, Random.Range(0f, 360f));
        var characterPart = newEntity.GetComponent<Character>();
        if(characterPart)
        {
            //TODO: add death event
            entitiesSpawned.Add(characterPart);
        }
    }

    private Vector3 GetRandomPosInSpawnArea()
    {
        var randomPos = new Vector3(Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x), transform.position.y,
                                    Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z));

        return randomPos;
    }
}
