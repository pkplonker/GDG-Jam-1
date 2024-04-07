using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuBehaviour : UIComponent
{
    public SettingsMenuBehaviour settingsMenu;


    public void OnSettings()
    {
        DisplayComponent(settingsMenu,true);
    }

    public void OnReturnToMenu()
    {
        Debug.Log("Returning to Main");
        SceneManager.LoadScene("MainMenu");

    }

    public void OnUnPause()
    {
        PauseController.Instance.IsPaused = false;
        DisplayComponent(this, false);

    }

}
