using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class testSound : MonoBehaviour
{
    public AudioSource mBackgroundSource;
    public AudioMixer backgroundAudioMixer;
    public AudioMixer gameAudioMixer;

    public Slider backgroundSoundBar;

    private void Awake()
    {
        backgroundSoundBar.minValue = 0;
        backgroundSoundBar.maxValue = 100;
        backgroundSoundBar.value = 0;
        UpdateMixerVolume(backgroundSoundBar.value, backgroundAudioMixer);

        backgroundSoundBar.onValueChanged.AddListener(OnVolumeChanged);
        
        SoundManager.PlaySound(mBackgroundSource, true, true);
    }

    private void OnVolumeChanged(float value)
    {
        float volumeInDB = Mathf.Lerp(-80, 2, value);
        UpdateMixerVolume(volumeInDB, backgroundAudioMixer);
    }

    public void UpdateMixerVolume(float value, AudioMixer audioMixer)
    {
        backgroundAudioMixer.SetFloat("mVolume", value);
    }

    void Start()
    {
        SoundManager.PlaySound(mBackgroundSource, true, true);
    }
}
