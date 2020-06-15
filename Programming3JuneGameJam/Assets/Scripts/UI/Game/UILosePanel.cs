using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILosePanel : Singleton<UILosePanel>
{
    [SerializeField] private GameObject losePanel = null;
    [SerializeField] private GameObject losePanelBg = null;

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
