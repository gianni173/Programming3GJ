using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public event Action<Character> OnMainPlayerSpawned;

    [SerializeField] private CharacterStats playerStats = default;
    [SerializeField] private string menuScene = "MainMenu";

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
    }

    public void Win()
    {
        var winPanel = UIWinPanel.Instance;
        if(winPanel)
        {
            winPanel.Win();
        }
        StartCoroutine(GoToMenuDelayed(5f));
    }

    private IEnumerator GoToMenuDelayed(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadSceneAsync(menuScene);
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
