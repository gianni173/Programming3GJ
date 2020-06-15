using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Sound", fileName = "new_sound")]
public class Sound : ScriptableObject
{
    public bool isOneShot = false;
    public AudioClip clip = null;
    public Vector2 minMaxVolume = new Vector2(1f, 1f);
    public Vector2 minMaxPitch = new Vector2(1f, 1f);
}
