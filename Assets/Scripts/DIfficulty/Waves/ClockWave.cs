using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockWave : Wave
{
    [SerializeField]
    private GridGenerator grid;

    [SerializeField]
    private bool clockwise, extraDifficulty;

    [SerializeField]
    private int middleSize = 2, spacing = 4;

    [SerializeField]
    private float startTime, minRotateTime, maxRotateTime, dangerDelay;

    private Coroutine clockCor;

    public override void StartWave() {
        clockCor = StartCoroutine(ClockMover()); 
        // grid.Rows
    }

    public override void UpdateWave() {}

    public override void EndWave() {
        base.EndWave();
        if(clockCor != null) StopCoroutine(clockCor);
    }

    private IEnumerator ClockMover() {
        yield return new WaitForSeconds(startTime);
        while(true) {

            var waitingTime = Random.Range(minRotateTime, maxRotateTime);
            yield return new WaitForSeconds(waitingTime);
        }
    }
}
