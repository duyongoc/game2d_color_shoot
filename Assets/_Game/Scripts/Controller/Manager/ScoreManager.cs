using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{


    // [public]
    public int score;
    public int highscore;



    #region UNITY
    private void OnEnable()
    {
        GameManager.EVENT_RESET_INGAME += ResetData;
        this.RegisterListener(EventID.OnEvent_UpdateScore, UpdateScore);
    }

    private void OnDisable()
    {
        GameManager.EVENT_RESET_INGAME -= ResetData;
        this.RemoveListener(EventID.OnEvent_UpdateScore, UpdateScore);
    }

    // private void Update()
    // {
    // }
    #endregion



    public void UpdateScore(object param)
    {
        score += (int)param;
        highscore = PlayerPrefs.GetInt("high_score");

        if (score > highscore)
            highscore = (int)score;

        PlayerPrefs.SetInt("high_score", highscore);
    }


    public void ResetData()
    {
        score = 0;
    }


}
