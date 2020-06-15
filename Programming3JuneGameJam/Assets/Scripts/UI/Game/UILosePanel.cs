using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILosePanel : Singleton<UILosePanel>
{
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject losePanelBg;

    private void Start()
    {
        losePanelBg.SetActive(false);
        losePanel.SetActive(false);
    }

    public void Lose()
    {
        losePanelBg.SetActive(true);
        losePanel.SetActive(true);
    }
}
