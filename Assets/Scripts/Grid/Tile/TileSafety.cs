using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TileSafety : MonoBehaviour
{
    private bool safe = true;

    [SerializeField]
    private SpriteRenderer alert;

    public bool Safe => safe;

    [SerializeField]
    private float maxFlickerTime = 0.2f, minFlickerTime = 0.01f;

    public event Action<bool> OnSafetyChanged;

    private void Awake()
    {
        SetSafety(safe);
    }

    public void SetTileSafety(bool safe = true, float delay = 0)
    {
        if (delay <= 0) SetSafety(safe);
        else StartCoroutine(StartSafetyToggle(safe, delay));
    }
    private void SetSafety(bool safe)
    {
        this.safe = safe;
        OnSafetyChanged?.Invoke(safe);
    }

    private IEnumerator StartSafetyToggle(bool safe, float delay)
    {
        if (!safe)
            yield return StartCoroutine(Blinkies(delay));

        SetSafety(safe);
    }

    private IEnumerator Blinkies(float delay)
    {
        float timeElapsed = 0;
        while (timeElapsed < delay)
        {
            var t = Mathf.InverseLerp(0, delay, timeElapsed);
            var flickerTime = Mathf.Lerp(maxFlickerTime, minFlickerTime, t);
            var timeLeft = delay - timeElapsed;
            var finalFlickerTime = Math.Min(flickerTime, timeLeft);
            alert.enabled = !alert.enabled;
            yield return new WaitForSeconds(finalFlickerTime);
            timeElapsed += flickerTime;
        }
        alert.enabled = false;
    }
}
