using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Global/Settings", fileName = "new_global_settings")]
public class GlobalSettings : SingletonScriptable<GlobalSettings>
{
    [System.Serializable]
    public struct SliderColors
    {
        public Color fillColor;
        public Color backgroundColor;
    }

    public SliderColors[] healthColors = null;
}
