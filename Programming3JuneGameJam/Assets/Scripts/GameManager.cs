using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public event Action<Character> OnMainPlayerSpawned;

    [SerializeField] private CharacterStats playerStats = default;

    private LevelSystem LevelSystem = null;
    private CameraManager CameraManager = null;

    private void Start()
    {
        LevelSystem = LevelSystem.Instance;
        if(LevelSystem)
        {
            LevelSystem.OnMapLoaded += SpawnMainPlayer;
        }

        CameraManager = CameraManager.Instance;
        Cursor.visible = false;
    }

    private void SpawnMainPlayer()
    {
        var playerSpawned = Instantiate(GlobalSettings.Instance.baseCharacterPrefab, LevelSystem.loadedMap.spawnPoint.position,
                                        LevelSystem.loadedMap.spawnPoint.rotation, LevelSystem.loadedMap.charactersContainer);
        var characterComponent = playerSpawned.GetComponent<Character>();
        characterComponent.InitCharacter(playerStats, GlobalSettings.Instance.playerFaction);

        OnMainPlayerSpawned?.Invoke(characterComponent);

        if (CameraManager)
        {
            CameraManager.followSystem.target = playerSpawned.transform;
            CameraManager.followSystem.Snap();
        }
    }
}
