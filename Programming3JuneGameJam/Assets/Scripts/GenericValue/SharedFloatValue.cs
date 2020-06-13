using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Shared Value/Float", fileName = "new_float_value")]
public class SharedFloatValue : ScriptableObject
{
    public float value = 0;
}
