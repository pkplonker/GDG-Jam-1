using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum VolumeType { GLOBAL, MUSIC, FX }

public class AudioManager : GenericUnitySingleton<AudioManager>
{
    public GlobalSoundList globalSoundList;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource _soundFXSource;
    [SerializeField] private AudioSource _musicSource;

    [SerializeField] private float _globalVolume;
    [SerializeField] private float _musicVolume;
    [SerializeField] private float _fxVolume;

    public float GlobalVolume { get { return _globalVolume; } }
    public float MusicVolume { get { return _musicVolume; } }
    public float FXVolume { get { return _fxVolume; } }


    protected override void Awake()
    {
        globalSoundList = GetComponent<GlobalSoundList>();   
        if (globalSoundList == null) Debug.LogWarning("No Sound List Present on AudioManager, Please Create One");

        _globalVolume = 1;
        _musicVolume = 1;
        _fxVolume = 1;
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
            _soundFXSource.PlayOneShot(clip);
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
