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
    
    private CameraInput input = null;

    private bool paused = false;

    private void Awake()
    {
        input = new CameraInput();
    }

    private void OnEnable()
    {
        input.Enable();
        input.cameraSwap.camera.performed += OnCameraInput;
        PauseController.Instance.PauseChanged += SwapPause;

        ((DeadState)PlayerStateMachine.Instance.DeadState).Death += OnDeath;
    }

    private void OnDisable()
    {
        input.Disable();
        input.cameraSwap.camera.performed -= OnCameraInput;
        PauseController.Instance.PauseChanged -= SwapPause;

        ((DeadState)PlayerStateMachine.Instance.DeadState).Death -= OnDeath;
    }

    private void SwapPause(bool _paused)
    {
        paused = _paused;
    }

    private void OnCameraInput(InputAction.CallbackContext context)
    {
        Debug.Log("press1");
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
