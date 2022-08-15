using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveManager : MonoBehaviour {

    [SerializeField]
    private Timer timer;
    [SerializeField]
    private int initialDifficultyUpperPercentage = 50, addPercentage = 20;

    [SerializeField]
    private List<Wave> waves;

    private int currentDifficulty = 0;
    private int maxDifficulty;
    private int lowestDifficulty;

    [SerializeField]
    private Wave currentWave;

    private int difficultyUpperPercentage = 50;

    private void Awake() {
        maxDifficulty = waves.Max(wave => wave.Difficulty);
        lowestDifficulty = waves.Min(wave => wave.Difficulty);
        currentDifficulty = lowestDifficulty;
        timer.OnTimerFinished += EndWave;
    }

    private void Update() {
        currentWave?.UpdateWave();
    }

    public void StartWave() {
        var lowWaves = waves.Where(wave => wave.Difficulty == lowestDifficulty).ToList();
        var wave = lowWaves[Random.Range(0, lowWaves.Count)];
        StartWave(wave);
    }

    public void StartWave(int difficulty) {
        var possibleWaves = waves.Where(wave => wave.Difficulty == difficulty).ToList();
        if(possibleWaves.Count == 0) return;
        var wave = possibleWaves[Random.Range(0, possibleWaves.Count)];
        StartWave(wave);
    }

    public void StartWave(Wave wave) {
        currentWave?.EndWave();
        wave.StartWave();
        currentWave = wave;

        timer.StartTimer(wave.Duration);
    }


    private void EndWave() {
        if (currentDifficulty < maxDifficulty) {
            AddDifficulty();
        }
        StartWave(currentDifficulty);
    }

    private void AddDifficulty() {
        var chance = Random.Range(0, 100);
        if (difficultyUpperPercentage >= chance) {
            currentDifficulty++;
            difficultyUpperPercentage = initialDifficultyUpperPercentage;
        } else difficultyUpperPercentage += addPercentage;
    }

}