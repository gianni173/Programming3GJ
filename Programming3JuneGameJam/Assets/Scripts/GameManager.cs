using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public event Action<Character> OnMainPlayerSpawned;

    [SerializeField] private CharacterStats playerStats = default;
    [SerializeField] private string menuScene = "MainMenu";
    [SerializeField] private Sound winSound = null;
    [SerializeField] private string loseScene = "MainScene";
    [SerializeField] private Sound loseSound = null;

    private LevelSystem LevelSystem = null;
    private CameraManager CameraManager = null;

    private void Start()
    {
        Cursor.visible = false;

        LevelSystem = LevelSystem.Instance;
        if(LevelSystem)
        {
            LevelSystem.OnMapLoaded += SpawnMainPlayer;
        }

        CameraManager = CameraManager.Instance;
        Cursor.visible = false;
    }

    public void Win()
    {
        var winPanel = UIWinPanel.Instance;
        if(winPanel)
        {
            winPanel.Win();
        }
        if (winSound)
        {
            SoundPlayer.Instance?.Play(winSound);
        }
        StartCoroutine(LoadSceneDelayed(5f, menuScene));
    }

    public void Lose()
    {
        var losePanel = UILosePanel.Instance;
        if (losePanel)
        {
            losePanel.Lose();
        }
        if (loseSound)
        {
            SoundPlayer.Instance?.Play(loseSound);
        }
        StartCoroutine(LoadSceneDelayed(5f, loseScene));
    }

    private IEnumerator LoadSceneDelayed(float delay, string sceneName)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadSceneAsync(sceneName);
    }

    private void SpawnMainPlayer()
    {
        var playerSpawned = Instantiate(GlobalSettings.Instance.baseCharacterPrefab, LevelSystem.loadedMap.spawnPoint.position,
                                        LevelSystem.loadedMap.spawnPoint.rotation, LevelSystem.loadedMap.charactersContainer);
        var characterComponent = playerSpawned.GetComponent<Character>();
        characterComponent.InitCharacter(playerStats, GlobalSettings.Instance.playerFaction);
        characterComponent.OnDeath += MainPlayerDead;

        OnMainPlayerSpawned?.Invoke(characterComponent);

        if (CameraManager)
        {
            CameraManager.followSystem.target = playerSpawned.transform;
            CameraManager.followSystem.Snap();
        }
    }

    private void MainPlayerDead(Character mainPlayer)
    {
        Lose();
    }
}
