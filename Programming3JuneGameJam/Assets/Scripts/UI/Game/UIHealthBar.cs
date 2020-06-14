﻿using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    [Title("References")]
    [SerializeField] private Slider slider = null;
    [SerializeField] private Image fill = null;
    [SerializeField] private Image background = null;

    [Space(5), Title("Debugging")]
    [SerializeField] private bool isDebugging = false;
    [SerializeField] private SharedFloatValue debugCurrentHp = null;
    [SerializeField] private SharedFloatValue debugMaxHp = null;
    
    private GameManager GameManager;

    private void Start()
    {
        GameManager = GameManager.Instance;
        if (GameManager)
        {
            GameManager.OnMainPlayerSpawned += LinkHealthBar;
        }
    }

    private void Update()
    {
        if (isDebugging)
        {
            SetValue(debugCurrentHp.value, debugMaxHp.value);
        }
    }

    public void SetValue(float currValue, float maxValue)
    {
        float normalizedValue = (float)(currValue - .001) % maxValue;
        slider.value = normalizedValue / maxValue;
        UpdateColor(currValue, maxValue);
    }

    private void UpdateColor(float currValue, float maxValue)
    {
        var numberOfBars = (int)((currValue - .001) / maxValue);
        if(numberOfBars >= GlobalSettings.Instance.healthColors.Length)
        {
            numberOfBars = GlobalSettings.Instance.healthColors.Length - 1;
        }

        fill.color = GlobalSettings.Instance.healthColors[numberOfBars].fillColor;
        background.color = GlobalSettings.Instance.healthColors[numberOfBars].backgroundColor;
    }

    private void LinkHealthBar(Character mainPlayer)
    {
        mainPlayer.OnHealthChanged += SetValue;
        SetValue(mainPlayer.HP, mainPlayer.Stats.basicHP);
    }
}