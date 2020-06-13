using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Title("References")]
    [SerializeField] private Slider slider = null;
    [SerializeField] private Image fill = null;
    [SerializeField] private Image background = null;
    [SerializeField] private SliderColors[] sliderColors = null;

    [Space(5), Title("Debugging")]
    [SerializeField] private bool isDebugging = false;
    [SerializeField] private float debugCurrentHp = 10;
    [SerializeField] private float debugMaxHp = 20;

    private void Update()
    {
        if (isDebugging)
        {
            SetValue(debugCurrentHp, debugMaxHp);
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
        if(numberOfBars >= sliderColors.Length)
        {
            numberOfBars = sliderColors.Length - 1;
        }

        fill.color = sliderColors[numberOfBars].fillColor;
        background.color = sliderColors[numberOfBars].backgroundColor;
    }

    [System.Serializable]
    public struct SliderColors
    {
        public Color fillColor;
        public Color backgroundColor;
    }
}
