using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenuMainPanel : MonoBehaviour
{
    [Title("Play")]
    [SerializeField] private UIMenuButton playButton = default;
    [SerializeField] private string playScene = default;
    [Space(5), Title("Quit")]
    [SerializeField] private UIMenuButton quitButton = default;
    [Space(5), Title("Generic")]
    [SerializeField] private Sound clickSound = null;

    private void Start()
    {
        playButton.onClick.AddListener(Play);
        quitButton.onClick.AddListener(Quit);
        Cursor.visible = true;
    }

    private void Play()
    {
        SoundPlayer.Instance?.Play(clickSound);
        SceneManager.LoadSceneAsync(playScene);
    }

    private void Quit()
    {
        SoundPlayer.Instance?.Play(clickSound);
        Application.Quit();
    }
}
