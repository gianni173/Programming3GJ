using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIRageGauge : MonoBehaviour
{
    [Title("References")]
    [SerializeField] private TextMeshProUGUI number = null;
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
            GameManager.OnMainPlayerSpawned += LinkRageBar;
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
        number.text = ((int)((currValue - .001f) / maxValue) + 1).ToString();
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

    private void LinkRageBar(Character mainPlayer)
    {
        mainPlayer.OnHealthChanged += SetValue;
        SetValue(mainPlayer.HP, mainPlayer.Stats.basicHP);
    }
}
