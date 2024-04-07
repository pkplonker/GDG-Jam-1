using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableCameraMovement : MonoBehaviour
{
    CinemachineFreeLook freeCamera;

    void Start()
    {
        freeCamera = gameObject.GetComponent<CinemachineFreeLook>();
    }

    private void OnEnable()
    {
        PauseController.Instance.PauseChanged += SwapCameraMove;
    }

    private void OnDisable()
    {
        PauseController.Instance.PauseChanged -= SwapCameraMove;
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
