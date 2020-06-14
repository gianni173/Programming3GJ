using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [Title("Activations")]
    [SerializeField] private bool startsActive = true;
    [SerializeField] private TriggerDetector[] activators = null;

    [Space(5), Title("Spawner Behaviour")]
    [SerializeField, Tooltip("Spawn after SpawnerPulse time or after all enemies have been killed?")] private bool pulseMode = true;
    [SerializeField, ShowIf("pulseMode")] private float spawnerPulse = 1f;
    [SerializeField] private Wave[] waves = null;
    [SerializeField] private BoxCollider spawnArea = null;
    [SerializeField] private Transform entitiesSpawnedContainer = null;
    [SerializeField] private List<Character> entitiesSpawned = new List<Character>();

    private bool isActive = false;
    private bool IsActive
    {
        get => isActive;
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

    private int aliveEntities = 0;
    private int AliveEntities
    {
        get => aliveEntities;
        set
        {
            if (aliveEntities != value)
            {
                aliveEntities = value;
                if (aliveEntities <= 0)
                {
                    aliveEntities = 0;
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
            var prevGizmosColor = Gizmos.color;

            var thisGizmoColor = Color.red;
            thisGizmoColor.a = isActive ? .4f : .2f;
            Gizmos.color = thisGizmoColor;
            Gizmos.DrawCube(spawnArea.bounds.center, spawnArea.bounds.size);

            Gizmos.color = prevGizmosColor;
        }
        if (activators != null)
        {
            var prevGizmosColor = Gizmos.color;

            var thisGizmoColor = Color.red;
            Gizmos.color = thisGizmoColor;

            foreach (TriggerDetector activator in activators)
            {
                Gizmos.DrawLine(spawnArea.bounds.center, activator.transform.position + (Vector3.up * 2f));
            }

            Gizmos.color = prevGizmosColor;
        }
    }

    private void Start()
    {
        IsActive = startsActive;
        foreach(TriggerDetector detector in activators)
        {
            detector.OnTriggerEntered += Activate;
        }
    }

    private void Update()
    {
        if(IsActive)
        {
            if (pulseMode)
            {
                if (Time.time > lastSpawnTime + spawnerPulse)
                {
                    SpawnWave();
                }
            }
        }
    }

    private void Activate(Collider collidedObject)
    {
        if(!IsActive)
        {
            IsActive = true;
        }
    }

    private void SpawnWave()
    {
        if (waves.Length > currWave)
        {
            lastSpawnTime = Time.time;

            foreach (Wave.EntitiesSet set in waves[currWave].entitiesSets)
            {
                for (int i = 0; i < set.amount; i++)
                {
                    if (set.entityStats)
                    {
                        SpawnEntity(set.entityStats, GetRandomPosInSpawnArea());
                    }
                }
            }

            currWave++;
        }
        if (currWave >= waves.Length)
        {
            IsActive = false;
        }
    }

    private void SpawnEntity(CharacterStats stats, Vector3 position)
    {
        var newEntity = Instantiate(GlobalSettings.Instance.baseCharacterPrefab, position, Quaternion.identity, entitiesSpawnedContainer);
        newEntity.transform.Rotate(Vector3.up, Random.Range(0f, 360f));
        var characterPart = newEntity.GetComponent<Character>();
        if(characterPart)
        {
            characterPart.InitCharacter(stats, GlobalSettings.Instance.enemyFaction);
            characterPart.OnDeath += EntityDied;
            entitiesSpawned.Add(characterPart);
            AliveEntities = entitiesSpawned.Count;
        }
    }

    private Vector3 GetRandomPosInSpawnArea()
    {
        var randomPos = new Vector3(Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x), transform.position.y,
                                    Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z));

        return randomPos;
    }

    private void EntityDied(Character character)
    {
        if (entitiesSpawned.Contains(character))
        {
            entitiesSpawned.Remove(character);
            AliveEntities = entitiesSpawned.Count;
        }
    }
}
