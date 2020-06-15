using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWinPanel : Singleton<UIWinPanel>
{
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject winPanelBg;

    private void Start()
    {
        winPanelBg.SetActive(false);
        winPanel.SetActive(false);
    }

    public void Win()
    {
        winPanelBg.SetActive(false);
        winPanel.SetActive(true);
    }
}
