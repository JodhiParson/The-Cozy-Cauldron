using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Audio_Play : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource musicSource;  // Background music
    public AudioSource sfxSource;    // Sound effects CHOMP
    public AudioSource sfxSource1;    // Sound effects CLICK
    public Slider musicSlider;
    public Slider sfxSlider;

    [Header("Volume Settings")]
    [Range(0f, 1f)] public float musicVolume = 1f;
    [Range(0f, .5f)] public float sfxVolume = .5f;

    [Header("Audio Text")]
    public TMP_Text musicText;
    public TMP_Text soundText;

    private void Start()
    {
        // Initialize AudioSources
        if (musicSource != null) musicSource.volume = musicVolume;
        if (sfxSource != null) sfxSource.volume = sfxVolume;

        UpdateUI();
    }

    // Update the on-screen volume text
    private void UpdateUI()
    {
        if (musicText != null)
            musicText.text = $"{Mathf.RoundToInt(musicVolume * 10f)}";

        if (soundText != null)
            soundText.text = $"{Mathf.RoundToInt(sfxVolume * 10f)}";
    }

    private void Update()
    {
        musicSlider.value = musicVolume;
        sfxSlider.value = sfxVolume;
    }

    // Play sound effect
    public void PlaySound()
    {
        if (sfxSource != null)
            sfxSource.Play();
    }
    public void PlaySound1()
    {
        if (sfxSource1 != null)
            sfxSource1.Play();
    }
    // 🎚️ Decrease Music Volume
    public void DecreaseMusicVolume(float amount = 0.1f)
    {
        musicVolume = Mathf.Clamp01(musicVolume - amount);
        if (musicSource != null) musicSource.volume = musicVolume;
        Debug.Log($"Music volume decreased: {musicVolume}");
        UpdateUI();
    }

    // 🎚️ Increase Music Volume
    public void IncreaseMusicVolume(float amount = 0.1f)
    {
        musicVolume = Mathf.Clamp01(musicVolume + amount);
        if (musicSource != null) musicSource.volume = musicVolume;
        Debug.Log($"Music volume increased: {musicVolume}");
        UpdateUI();
    }

    // 🔉 Decrease SFX Volume
    public void DecreaseSFXVolume(float amount = 0.05f)
    {
        sfxVolume = Mathf.Clamp01(sfxVolume - amount);
        if (sfxSource != null) sfxSource.volume = sfxVolume;
        if (sfxSource1 != null) sfxSource1.volume = sfxVolume;
        
        // Debug.Log($"SFX volume decreased: {sfxVolume}");
        UpdateUI();
    }

    // 🔉 Increase SFX Volume
    public void IncreaseSFXVolume(float amount = 0.05f)
    {
        sfxVolume = Mathf.Clamp01(sfxVolume + amount);
        if (sfxSource != null) sfxSource.volume = sfxVolume;
        if (sfxSource1 != null) sfxSource1.volume = sfxVolume;

        // Debug.Log($"SFX volume increased: {sfxVolume}");
        UpdateUI();
    }
}
