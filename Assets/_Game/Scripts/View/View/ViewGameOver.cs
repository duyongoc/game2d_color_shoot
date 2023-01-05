using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class ViewGameOver : View
{

    // inspector
    [SerializeField] private TMP_Text textHighScore;
    [SerializeField] private Transform highScorePanel;

    private bool _isDoneGameover;



    #region  UNITY
    // private void Start()
    // {
    // }

    // private void Update()
    // {
    // }
    #endregion



    #region STATE
    public override void StartState()
    {
        LoadView();
    }

    public override void UpdateState()
    {
    }

    public override void EndState()
    {
    }
    #endregion




    private void LoadView()
    {
        int score = ScoreManager.Instance.score;
        textHighScore.text = score.ToString();
        PlayfabController.Instance.CheckShowRecordScore(score);
    }


    public void OnClickButtonReplay()
    {
        GameManager.Instance.ReplayGame();
    }


}
