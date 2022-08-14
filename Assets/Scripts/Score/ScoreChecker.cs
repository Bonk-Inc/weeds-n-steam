using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreChecker : MonoBehaviour
{
    [SerializeField]
    private Canvas canvas;

    [SerializeField]
    private ScoreDisplay[] scores;

    [SerializeField]
    private TMPro.TextMeshProUGUI scoreLabelDisplay;

    [SerializeField]
    private TMPro.TextMeshProUGUI scoreDisplay;

    [SerializeField]
    private float displayTime = 5;
    private IEnumerator Start()
    {
        int current = 0;
        while (true)
        {
            scoreLabelDisplay.text = scores[current].label;
            scoreDisplay.text = PlayerPrefs.GetInt(scores[current].playerpref, 0).ToString();
            yield return new WaitForSeconds(displayTime);
            current += 1;
            current %= scores.Length;
        }

    }

    [System.Serializable]
    private class ScoreDisplay
    {
        public string label;
        public string playerpref;
    }
}
