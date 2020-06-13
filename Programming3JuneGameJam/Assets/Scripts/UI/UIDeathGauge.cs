using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIDeathGauge : MonoBehaviour
{
    [Title("References")]
    [SerializeField] private TextMeshProUGUI number = null;
    [SerializeField] private Image background = null;

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
        number.text = ((int)(normalizedValue / maxValue)).ToString();
        UpdateColor(currValue, maxValue);
    }

    private void UpdateColor(float currValue, float maxValue)
    {
        var numberOfBars = (int)((currValue - .001) / maxValue);
        if (numberOfBars >= GlobalSettings.Instance.healthColors.Length)
        {
            numberOfBars = GlobalSettings.Instance.healthColors.Length - 1;
        }

        number.color = GlobalSettings.Instance.healthColors[numberOfBars].fillColor;
        background.color = GlobalSettings.Instance.healthColors[numberOfBars].backgroundColor;
    }
}
