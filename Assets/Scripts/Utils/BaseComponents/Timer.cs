using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{

    [SerializeField]
    private float defaultStartTime;
    
    [SerializeField]
    private bool startImmediate;

    private float timeLeft = 0;

    public float DefaultStartTime { get => defaultStartTime; set => defaultStartTime = value; }
    public float StartTime { get => defaultStartTime; set => defaultStartTime = value; }
    public float TimeLeft => timeLeft;

    public bool IsRunning {get; private set;}

    public event Action OnTimerStarted;
    public event Action OnTimerChanged;
    public event Action OnTimerFinished;

    private void Start() {
        if(startImmediate){
            StartTimer();
        }
    }

    public void StartTimer(){
        StartTimer(defaultStartTime);
    }

    public void StartTimer(float time){
        
        if(time <= 0)
            return;
        
        StartTime = time;
        IsRunning = true;
        timeLeft = time;
        OnTimerStarted?.Invoke();
    }

    private void Update() {

        IsRunning = IsRunning && TimeLeft != 0; 

        if(!IsRunning)
            return;

        timeLeft = Mathf.Max(timeLeft - Time.deltaTime, 0);
                
        if(TimeLeft == 0){
            OnTimerFinished?.Invoke();
        }

        OnTimerChanged?.Invoke();
    }

    public void StopTimer(){
        IsRunning = false;
        timeLeft = 0;
    }



    

}
