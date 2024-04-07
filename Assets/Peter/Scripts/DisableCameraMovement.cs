using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableCameraMovement : MonoBehaviour
{
    CinemachineFreeLook freeCamera;
    private PauseController pauseController;

    void Start()
    {
        freeCamera = gameObject.GetComponent<CinemachineFreeLook>();
        pauseController = FindObjectOfType<PauseController>();
        pauseController.PauseChanged += SwapCameraMove;

    }

    private void OnDisable()
    {
        pauseController.PauseChanged -= SwapCameraMove;
    }

    private void SwapCameraMove(bool paused)
    {
        if (paused)
        {
            freeCamera.m_YAxis.m_MaxSpeed = 0;
            freeCamera.m_XAxis.m_MaxSpeed = 0;
        }
        else
        {
            freeCamera.m_YAxis.m_MaxSpeed = 0.15f;
            freeCamera.m_XAxis.m_MaxSpeed = 4;
        }
    }
}
