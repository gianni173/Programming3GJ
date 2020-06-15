using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessModifies : MonoBehaviour
{
    [SerializeField] private PostProcessProfile PostProcessProfile = null;
    [SerializeField] private Vector2 minMaxVignetteIntensity = new Vector2(.24f, .33f);
    [SerializeField] private float maxVignetteOffset = .5f;
    [SerializeField] private Color normalBloomColor = Color.white;
    [SerializeField] private Color rageBloomColor = Color.white;
    [SerializeField] private float normalGrainValue = 0f;
    [SerializeField] private float rageGrainValue = .24f;
    [SerializeField] private float maxAberrationIntensity = .54f;
    [SerializeField] private float maxAberrationOffset = .5f;

    private float bounceValue = 0f;
    private float minBounceValue = -.3f;
    private float maxBounceValue = .3f;

    private Vignette vignetteModule = null;
    private float desiredVignetteValue = 0f;

    private Bloom bloomModule = null;
    private Color desiredBloomColor = Color.white;

    private Grain grainModule = null;
    private float desiredGrainValue = 0f;

    private ChromaticAberration aberrationModule = null;
    private float desiredAberrationValue = 0f;


    private void Start()
    {
        var gameManager = GameManager.Instance;
        if(gameManager)
        {
            gameManager.OnMainPlayerSpawned += LinkMainPlayer;
        }

        StartCoroutine(BounceValue());

        vignetteModule = PostProcessProfile.GetSetting<Vignette>();
        bloomModule = PostProcessProfile.GetSetting<Bloom>();
        grainModule = PostProcessProfile.GetSetting<Grain>();
        aberrationModule = PostProcessProfile.GetSetting<ChromaticAberration>();
    }

    private void Update()
    {
        vignetteModule.intensity.value = Mathf.Lerp(vignetteModule.intensity.value, 
                                                    desiredVignetteValue + (desiredVignetteValue * maxVignetteOffset * bounceValue),
                                                    .7f);
        bloomModule.color.value = Color.Lerp(bloomModule.color.value,
                                             desiredBloomColor, .7f);
        grainModule.intensity.value = Mathf.Lerp(grainModule.intensity.value,
                                                 desiredGrainValue, .7f);
        aberrationModule.intensity.value = Mathf.Lerp(aberrationModule.intensity.value,
                                                      desiredAberrationValue + (desiredAberrationValue * maxAberrationOffset * bounceValue),
                                                      .7f);
    }

    private void LinkMainPlayer(Character mainPlayer)
    {
        mainPlayer.OnRageChanged += LinkedPlayerRageChanged;
        mainPlayer.OnHealthChanged += LinkedPlayerHealthChanged;
    }

    private void LinkedPlayerHealthChanged(float curr, float max)
    {
        desiredVignetteValue = 0f;
        if(curr / max < .15f)
        {
            var normalizedCurrHpValue = 1 - ((curr / max) / .15f);
            desiredVignetteValue = Mathf.Lerp(minMaxVignetteIntensity.x, minMaxVignetteIntensity.y, normalizedCurrHpValue);
        }
    }

    private void LinkedPlayerRageChanged(Character character, float normalizedRageTime)
    {
        if(normalizedRageTime > 0)
        {
            desiredBloomColor = rageBloomColor;
            desiredGrainValue = rageGrainValue;
            desiredAberrationValue = maxAberrationIntensity;
        }
        else
        {
            desiredBloomColor = normalBloomColor;
            desiredGrainValue = normalGrainValue;
            desiredAberrationValue = 0;
        }
    }

    private IEnumerator BounceValue()
    {
        var mod = 1f;
        while(true)
        {
            if (mod > 0 && bounceValue < maxBounceValue)
            {
                bounceValue += Time.deltaTime;
                if(bounceValue >= maxBounceValue)
                {
                    mod = -mod;
                }
            }
            if (mod < 0 && bounceValue > minBounceValue)
            {
                bounceValue -= Time.deltaTime;
                if (bounceValue <= minBounceValue)
                {
                    mod = -mod;
                }
            }
            yield return null;
        }
    }
}
