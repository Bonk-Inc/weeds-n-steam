using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField]
    private PlayerHealth health;

    [SerializeField]
    private string highscorePref = "highscore", scorePref = "score";

    [SerializeField]
    private Score score;

    private void Awake()
    {
        health.OnDeath += Die;
    }

    private void Die()
    {
        var currentScore = score.CurrentScore;
        var highscore = Mathf.Max(currentScore, PlayerPrefs.GetInt(highscorePref, 0));

        PlayerPrefs.SetInt(scorePref, currentScore);
        PlayerPrefs.SetInt(highscorePref, highscore);

        SceneLoader.Instance.LoadScene("Menu");

    }

}
