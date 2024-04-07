using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Windows;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private GameObject thirdPersonCamera;
    [SerializeField] private GameObject firstPersonCamera;
    [SerializeField] private GameObject topDownCamera;
    [SerializeField] private GameObject deathCamera;
    
    private PeterTestControls input;

    private bool paused = false;

    private void Start()
    {
        input = new PeterTestControls();
    }

    private void OnEnable()
    {
        input.PlayerTest.Cameras.performed += OnCameraInput;
        PauseController.Instance.PauseChanged += SwapPause;
    }

    private void OnDisable()
    {
        input.PlayerTest.Cameras.performed += OnCameraInput;
        PauseController.Instance.PauseChanged -= SwapPause;
    }

    private void SwapPause(bool _paused)
    {
        paused = _paused;
    }

    private void OnCameraInput(InputAction.CallbackContext context)
    {
        if (!paused)
        {
            switch (context.control.name)
            {
                case "1":
                    firstPersonCamera.SetActive(true);
                    topDownCamera.SetActive(false);
                    thirdPersonCamera.SetActive(false);
                    deathCamera.SetActive(false);
                    break;
                case "2":
                    firstPersonCamera.SetActive(false);
                    topDownCamera.SetActive(true);
                    thirdPersonCamera.SetActive(false);
                    deathCamera.SetActive(false);
                    break;
                case "3":
                    firstPersonCamera.SetActive(false);
                    topDownCamera.SetActive(false);
                    thirdPersonCamera.SetActive(true);
                    deathCamera.SetActive(false);
                    break;
            }
        }
    }

    private void OnDeath()
    {
        firstPersonCamera.SetActive(false);
        topDownCamera.SetActive(false);
        thirdPersonCamera.SetActive(false);
        deathCamera.SetActive(true);
    }
}
