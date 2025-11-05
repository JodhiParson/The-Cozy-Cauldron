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
    [Range(0f, 1f)] public float sfxVolume = 1f;

    [Header("Audio Text")]
    public TMP_Text musicText;
    public TMP_Text soundText;

    private void Start()
    {
        if (musicSource != null) musicSource.volume = musicVolume;
        if (sfxSource != null) sfxSource.volume = sfxVolume;

        UpdateUI();
    }

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
    public void SetMusicVol(float volume)
    {
        musicVolume = Mathf.Clamp01(volume);
        if (musicSource != null) musicSource.volume = musicVolume;
        UpdateUI();
    }
    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
        if (sfxSource != null) sfxSource.volume = sfxVolume;
        if (sfxSource1 != null) sfxSource1.volume = sfxVolume;
        UpdateUI();
    }
}
