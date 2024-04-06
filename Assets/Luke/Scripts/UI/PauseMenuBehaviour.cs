using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    public void OnUnPause()
    {
        DisplayComponent(this, false);

    }

}
