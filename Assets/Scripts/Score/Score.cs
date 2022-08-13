using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{

    [SerializeField]
    private TMPro.TextMeshProUGUI scoreDisplay;

    private float score;

    public int CurrentScore => Mathf.FloorToInt(score);

    private void Update()
    {
        score += Time.deltaTime;
        scoreDisplay.SetText(CurrentScore.ToString());
    }

}
