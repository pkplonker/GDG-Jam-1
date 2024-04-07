using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PlayerOverlay : MonoBehaviour
{
    public Slider battery;
    public Image chargingImage;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI rubbishCounter;

    public event Action FinishedGame;

    public TimeController timeController;

    private float prevVal;

    private void Awake()
    {
        prevVal = 1;
        chargingImage.gameObject.SetActive(false);
    }

    private void Start()
    {
        timeController = ScoreManager.Instance.GetComponent<TimeController>();
    }

    public void OnFinishLevel()
    {
        Debug.Log("Finishing Level");
        FinishedGame.Invoke();

    }
    private void OnEnable()
    {
        PlayerStateMachine.Instance.GetComponent<Battery>().BatteryLevelChanged += OnBatteryChanged;
        RubbishCollectionController.OnScoreChanged += ScoreChanged;
    }

    private void Update()
    {
        var currTime = timeController.elapsedSeconds;

        int minutes = TimeSpan.FromSeconds(currTime).Minutes;
        int seconds = TimeSpan.FromSeconds(currTime).Seconds;

        timerText.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    private void ScoreChanged(int score)
    {
        rubbishCounter.text = score.ToString();
    }

    private void OnBatteryChanged(float _, float percent)
    {
        var mainPercent = percent / 100;
        if (mainPercent > 1) mainPercent = 1;
        else if (mainPercent < 0) mainPercent = 0;
        
        chargingImage.gameObject.SetActive(mainPercent > prevVal && mainPercent!=1);

        battery.SetValueWithoutNotify(mainPercent);
        prevVal = mainPercent;
    }
}
