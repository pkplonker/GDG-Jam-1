using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuBehaviour : UIComponent
{

    public Slider volumeSFX;
    public Slider volumeMusic;
    public Slider volumeAll;

    protected override void SetVals()
    {
        //Debug.Log("Setting Vals");

        volumeAll.SetValueWithoutNotify(AudioManager.Instance.GlobalVolume);
        volumeMusic.SetValueWithoutNotify(AudioManager.Instance.MusicVolume);
        volumeSFX.SetValueWithoutNotify(AudioManager.Instance.FXVolume);
    }


    public void OnSetMasterVol()
    {
        AudioManager.Instance.SetVolume(volumeAll.value,VolumeType.GLOBAL);
    }

    public void OnSetSFXVol()
    {
        AudioManager.Instance.SetVolume(volumeSFX.value, VolumeType.FX);
    }

    public void OnSetMusicVol()
    {
        AudioManager.Instance.SetVolume(volumeMusic.value, VolumeType.MUSIC);
    }


}
