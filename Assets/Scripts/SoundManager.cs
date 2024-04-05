using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Tilemaps;

public static class SoundManager 
{
    public static void PlaySound(AudioSource source, bool playOnAwake, bool loop)
    {
        source.playOnAwake = playOnAwake;
        source.loop = loop;
    }
    public static void StopSound(AudioSource source)
    {
        source.Stop();
    }
    public static void PauseSound(AudioSource source)
    {
        source.Pause();
    }
    public static void UnPauseSound(AudioSource source)
    {
        source.UnPause();
    }


    /// <summary>
    /// To Cross fade between two audios
    /// </summary>
    /// <param name="audio1">The Audio that you want to fade out</param>
    /// <param name="audio2">The Audio that you want to fade in</param>
    public static void SwapAudio(AudioSource audio1, AudioSource audio2)
    {
        audio1.Stop();
        audio2.Play();
    }

}
