using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    [SerializeField]
    private Timer timer;

    [SerializeField]
    private Image fillBar;

    private void Awake() {
        timer.OnTimerChanged += () => {
            fillBar.fillAmount = 1 / timer.StartTime * timer.TimeLeft;
        };
    }
}
