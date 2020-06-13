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
    [SerializeField] private SharedFloatValue debugCurrentHp = null;
    [SerializeField] private SharedFloatValue debugMaxHp = null;

    private void Update()
    {
        if (isDebugging)
        {
            SetValue(debugCurrentHp.value, debugMaxHp.value);
        }
    }

    public void SetValue(float currValue, float maxValue)
    {
        number.text = ((int)(currValue / maxValue) + 1).ToString();
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
