using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TileSafety : MonoBehaviour
{
    private bool safe = true;

    public bool Safe => safe;

    public event Action<bool> OnSafetyChanged;

    public void SetTileSafety(bool safe = true, float delay = 0) {
     if(delay <= 0 ) SetSafety(safe);
     else StartCoroutine(StartSafetyToggle(safe, delay));
    }

    private void Awake() {
        SetSafety(safe);
    }

    private void SetSafety(bool safe) {
        this.safe = safe;
        OnSafetyChanged?.Invoke(safe);
    }

    private IEnumerator StartSafetyToggle(bool safe, float delay) {
        yield return new WaitForSeconds(delay); // Change to activate a blinking system.
        SetSafety(safe);
    }

    [ContextMenu("Cap")]
    private void SetDanger(){
        SetSafety(false);
    }
}
