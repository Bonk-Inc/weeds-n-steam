using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScoreFader : MonoBehaviour
{
    private static bool isFirst = false;

    [SerializeField]
    private float normalDelay, firstDelay;

    [SerializeField]
    private float fadeTime = 1;

    [SerializeField]
    private CanvasGroup title, score;

    private IEnumerator Start()
    {
        var delay = isFirst ? firstDelay : normalDelay;
        isFirst = false;
        yield return new WaitForSeconds(delay);
        yield return StartCoroutine(Fade(1, 0, title));
        yield return StartCoroutine(Fade(0, 1, score));
    }

    private IEnumerator Fade(float from, float to, CanvasGroup item)
    {
        var t = 0f;
        while (t < 1)
        {
            item.alpha = Mathf.Lerp(from, to, t);
            t += Time.deltaTime / fadeTime;
            yield return null;
        }
    }
}
