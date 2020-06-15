using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWinPanel : Singleton<UIWinPanel>
{
    [SerializeField] private GameObject winPanel = null;
    [SerializeField] private GameObject winPanelBg = null;

    private void Start()
    {
        winPanelBg.SetActive(false);
        winPanel.SetActive(false);
    }

    public void Win()
    {
        winPanelBg.SetActive(true);
        winPanel.SetActive(true);
    }
}
