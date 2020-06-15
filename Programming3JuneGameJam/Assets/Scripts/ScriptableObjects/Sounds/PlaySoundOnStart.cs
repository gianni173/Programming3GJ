using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnStart : MonoBehaviour
{
    [SerializeField] private Sound soundToPlay;
    
    private void Start()
    {
        if (soundToPlay)
        {
            SoundPlayer.Instance?.Play(soundToPlay);
        }
    }
}