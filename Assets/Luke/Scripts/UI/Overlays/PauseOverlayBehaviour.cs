using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseOverlayBehaviour : MonoBehaviour
{
	public PauseMenuBehaviour pauseMenu;
	private PauseController pauseController;

	private void Awake()
	{
		pauseController = FindObjectOfType<PauseController>();
	}

	public void OnPause()
	{
		pauseController.IsPaused = true;
		AudioManager.Instance.PlaySoundEffect(AudioManager.Instance.globalSoundList.uiclick);
		pauseMenu.DisplayComponent(pauseMenu, true);
	}
}