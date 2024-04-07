using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerOverlay : MonoBehaviour
{
    public Slider battery;
    public Image chargingImage;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI rubbishCounter;

    private float prevVal;

    private void Awake()
    {
        prevVal = 1;
        chargingImage.gameObject.SetActive(false);
    }

    public void OnFinishLevel()
    {
        Debug.Log("Finishing Level");
    }
    private void OnEnable()
    {
        PlayerStateMachine.Instance.GetComponent<Battery>().BatteryLevelChanged += OnBatteryChanged;
        RubbishCollectionController.OnScoreChanged += ScoreChanged;
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
