using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Wave: MonoBehaviour
{
    [SerializeField]
    protected GridDangerCreator dangerCreator;

    [SerializeField]
    private int difficulty;

    [SerializeField]
    protected float duration = 30;

    public int Difficulty => difficulty;
    public float Duration => duration;

    public abstract void StartWave();
    public abstract void UpdateWave();
    public virtual void EndWave() {
        dangerCreator.SetAllSaferous();
    }

}
