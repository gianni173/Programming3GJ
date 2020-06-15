using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource oneShotSource;

    public void PlayOneShot(AudioClip clip, Vector2 minMaxVolume, Vector2 minMaxPitch)
    {
        oneShotSource.clip = clip;
        oneShotSource.volume = Random.Range(minMaxVolume.x, minMaxVolume.y);
        oneShotSource.pitch = Random.Range(minMaxPitch.x, minMaxPitch.y);
        oneShotSource.Play();
    }

    public void PlayMusic(AudioClip clip, float volume)
    {
        musicSource.clip = clip;
        musicSource.volume = volume;
        musicSource.Play();
    }
}
