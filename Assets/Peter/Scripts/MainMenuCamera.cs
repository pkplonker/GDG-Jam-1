using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCamera : MonoBehaviour
{
    [SerializeField] GameObject cam1;
    [SerializeField] GameObject cam2;

    [SerializeField] double swapTime;

    double timer = 0;

    bool swap = false;

    private void Start()
    {
        timer = Time.time;

        cam1.SetActive(true);
        cam1.SetActive(false);
    }

    void Update()
    {
        if (timer + swapTime < Time.time)
        {
            timer = Time.time;
    
            cam1.SetActive(swap);
            cam1.SetActive(!swap);

            swap = !swap;
        }
    }
}
