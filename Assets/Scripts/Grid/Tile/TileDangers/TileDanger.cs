using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TileDanger : MonoBehaviour
{
    [SerializeField]
    protected TileSafety safety;

    private void Awake() {
        if(safety != null) safety.OnSafetyChanged += ToggleDanger;
    }

    public abstract void ToggleDanger(bool safe);
}
