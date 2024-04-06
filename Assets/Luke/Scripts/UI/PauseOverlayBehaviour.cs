using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseOverlayBehaviour : MonoBehaviour
{

    public PauseMenuBehaviour pauseMenu;

    public void OnPause()
    {
        PauseController.Instance.IsPaused = true;
        pauseMenu.DisplayComponent(pauseMenu, true);
    }



}
