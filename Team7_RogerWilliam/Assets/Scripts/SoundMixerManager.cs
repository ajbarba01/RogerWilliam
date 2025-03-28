using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixerInstance;
    private static AudioMixer audioMixer;
    public static float masterVol = 1f;

    void Start() {
        audioMixer = audioMixerInstance;
        audioMixerInstance = null;
        
        SetMasterVolume(masterVol);
    }
    
    public static void SetMasterVolume (float level) 
    {
        masterVol = level;
        audioMixer.SetFloat("masterVolume", Mathf.Log10(level) * 20f);
    }

    public static void SetSoundFXVolume (float level) 
    {
        audioMixer.SetFloat("soundFXVolume", Mathf.Log10(level) * 20f);
    }

    public static void SetMusicVolume (float level) 
    {
        audioMixer.SetFloat("musicVolume", Mathf.Log10(level) * 20f);
    }
}
