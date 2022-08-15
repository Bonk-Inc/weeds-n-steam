using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawnWave : Wave
{
    
    [SerializeField]
    private float minSpawnTime = 1f, maxSpawnTime = 10f;

    [SerializeField]
    private float minDissapearTime = 4f, maxDissapearTime = 12f;

    [SerializeField]
    private float dangerDelay = 3f;

    private Coroutine dangerCor, safetyCor;

    public override void StartWave()
    {
        dangerCor = StartCoroutine(DangerSpawner());
        safetyCor = StartCoroutine(SafetySpawner());
    }

    public override void UpdateWave() { }

    private IEnumerator DangerSpawner() {
        while(true) {
            dangerCreator.SetDangerous(dangerDelay);

            var waitingTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(waitingTime);
        }
    }

    private IEnumerator SafetySpawner() {
        
        yield return new WaitForSeconds(GetRandomTime(minDissapearTime, maxDissapearTime));
        while(true) {            
            dangerCreator.SetSaferous();
            yield return new WaitForSeconds(GetRandomTime(minDissapearTime, maxDissapearTime));
        }
    }

    private float GetRandomTime(float minTime, float maxTime) {
        return Random.Range(minTime, maxTime);
    }

    public override void EndWave() {
        base.EndWave();
        if(dangerCor != null) StopCoroutine(dangerCor);
        if(safetyCor != null) StopCoroutine(safetyCor);
    }
}
