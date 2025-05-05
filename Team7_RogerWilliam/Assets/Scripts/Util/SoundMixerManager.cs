using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixerInstance;
    private static AudioMixer audioMixer;

    public static float masterVol = 0.5f;

    void Awake() {
        audioMixer = audioMixerInstance;
        if (audioMixer == null) {
            Debug.LogError("SoundMixerManager: AudioMixer reference not set in inspector.");
        } else {
            SetMasterVolume(masterVol);
        }
    }

    public static void SetMasterVolume(float level) {
        masterVol = level;
        if (audioMixer != null) {
            audioMixer.SetFloat("masterVolume", Mathf.Log10(Mathf.Clamp(level, 0.0001f, 1f)) * 20f);
        }
    }

    public static void SetSoundFXVolume(float level) {
        if (audioMixer != null) {
            audioMixer.SetFloat("soundFXVolume", Mathf.Log10(Mathf.Clamp(level, 0.0001f, 1f)) * 20f);
        }
    }

    public static void SetMusicVolume(float level) {
        if (audioMixer != null) {
            audioMixer.SetFloat("musicVolume", Mathf.Log10(Mathf.Clamp(level, 0.0001f, 1f)) * 20f);
        }
    }
}
