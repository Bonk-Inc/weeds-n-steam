using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDifficulty : MonoBehaviour
{
    /// TODO Make this into a wave system? 
    /// Each wave has a set difficulty and a duration. Every wave can be different by spawning enemies, changing up the danger tiles, etc
    /// At the end of each wave, the grid and enemies should be cleared and set to save again. 
    ///  
    /// In another class called WaveManager, keep track of the current difficulty, when to up that difficulty and decide which wave to start depending on the current difficulty.
    
    [SerializeField]
    private GridDangerCreator dangerCreator;

    [SerializeField]
    private GridGenerator grid;

    [SerializeField]
    private float minSpawnTime = 1f, maxSpawnTime = 10f;

    [SerializeField]
    private int dangerChance = 70;

    [SerializeField]
    private float dangerDelay = 3f;

    private Coroutine manager;

    public void StartRound()
    {
        manager = StartCoroutine(DifficultyManager());
    }

    private IEnumerator DifficultyManager() {
        while(true) {
            
            var chance = Random.Range(0, 100);
            if(chance <= dangerChance) dangerCreator.SetDangerous(dangerDelay);
            else dangerCreator.SetSaferous();

            var waitingTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(waitingTime);
        }
    }
}
