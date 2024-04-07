using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuBehaviour : UIComponent
{
    public SettingsMenuBehaviour settingsMenu;
    private PauseController pauseController;

    protected override void Awake()
    {
        base.Awake();
        pauseController = FindObjectOfType<PauseController>();
    }

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
        pauseController.IsPaused = false;
        DisplayComponent(this, false);

    }

}
