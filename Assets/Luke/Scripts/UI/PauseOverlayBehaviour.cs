using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseOverlayBehaviour : MonoBehaviour
{

    public PauseMenuBehaviour pauseMenu;

    public void OnPause()
    {
        pauseMenu.DisplayComponent(pauseMenu, true);
    }



}
