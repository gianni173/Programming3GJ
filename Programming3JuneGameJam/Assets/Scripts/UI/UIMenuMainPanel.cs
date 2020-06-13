using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenuMainPanel : MonoBehaviour
{
    [Title("Play")]
    [SerializeField] private UIMenuButton playButton;
    [SerializeField] private string playScene;
    [Space(5), Title("Quit")]
    [SerializeField] private UIMenuButton quitButton;

    private void Start()
    {
        playButton.onClick.AddListener(Play);
        quitButton.onClick.AddListener(Quit);
    }

    private void Play()
    {
        SceneManager.LoadSceneAsync(playScene);
    }

    private void Quit()
    {
        Application.Quit();
    }
}
