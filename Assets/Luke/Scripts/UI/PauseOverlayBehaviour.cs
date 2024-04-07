using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseOverlayBehaviour : MonoBehaviour
{

    public PauseMenuBehaviour pauseMenu;

    public void OnPause()
    {
        PauseController.Instance.IsPaused = true;
        AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.globalSoundList.uiclick);
        pauseMenu.DisplayComponent(pauseMenu, true);
    }



}
