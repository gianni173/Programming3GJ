using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : Singleton<SoundPlayer>
{
    [SerializeField] private AudioSource musicSource = null;
    [SerializeField] private AudioSource oneShotSource = null;

    public void Play(Sound sound)
    {
        var source = sound.isOneShot ? oneShotSource : musicSource;
        source.clip = sound.clip;
        source.volume = Random.Range(sound.minMaxVolume.x, sound.minMaxVolume.y);
        source.pitch = Random.Range(sound.minMaxPitch.x, sound.minMaxPitch.y);
        if (sound.isOneShot)
        {
            source.PlayOneShot(source.clip, source.volume);
        }
        else
        {
            source.Play();
        }
    }
}
