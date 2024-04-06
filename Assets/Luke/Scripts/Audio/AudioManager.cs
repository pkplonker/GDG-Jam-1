using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum VolumeType { GLOBAL, MUSIC, FX }

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    public GlobalSoundList globalSoundList;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource _soundFXSource;
    [SerializeField] private AudioSource _musicSource;

    private float _globalVolume;
    private float _musicVolume;
    private float _fxVolume;

    public float GlobalVolume { get { return _globalVolume; } }
    public float MusicVolume { get { return _musicVolume; } }
    public float FXVolume { get { return _fxVolume; } }


    private void Awake()
    {
        // Singleton Functionality
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            globalSoundList = GetComponent<GlobalSoundList>();

            if (globalSoundList == null) Debug.LogWarning("No Sound List Present on AudioManager, Please Create One");


        }

        DontDestroyOnLoad(gameObject);
    }

    //Gets called when this script is added to an object
    private void Reset()
    {

        GameObject obj;

        obj = new GameObject("SoundEffects_AudioSource1");
        obj.transform.parent = gameObject.transform;
        obj.AddComponent<AudioSource>();
        _soundFXSource = obj.GetComponent<AudioSource>();
        _soundFXSource.playOnAwake = false;

        obj = new GameObject("Music_AudioSource");
        obj.transform.parent = gameObject.transform;
        obj.AddComponent<AudioSource>();
        _musicSource = obj.GetComponent<AudioSource>();
        _musicSource.loop = true;

    }

    public void PlaySoundEffect(AudioClip clip)
    {
        if (clip == null)
        {
            Debug.Log("Sound Effect not present");
            return;
        }

        else 
        {
            PlaySoundEffect(clip);

        }

    }



    public void SetVolume(float value, VolumeType type)
    {
        switch (type)
        {
            case VolumeType.GLOBAL:
                _globalVolume = value;
                break;
            case VolumeType.FX:
                _fxVolume = value;
                break;
            case VolumeType.MUSIC:
                _musicVolume = value;
                break;
        }

        _soundFXSource.volume = _globalVolume * _fxVolume;
        _musicSource.volume = _globalVolume * _musicVolume;

    }

    public void SetMute(bool mute, VolumeType type)
    {
        switch (type)
        {
            case VolumeType.MUSIC:
                _musicSource.mute = mute;
                break;
            case VolumeType.FX:
                _soundFXSource.mute = mute;
                break;
        }
    }

}
