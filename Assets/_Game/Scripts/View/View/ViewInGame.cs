using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class ViewInGame : View
{


    // inspector
    [SerializeField] private TMP_Text textScore;



    #region  UNITY
    private void Start()
    {
    }

    // private void Update()
    // {
    // }
    #endregion



    #region STATE
    public override void StartState()
    {
    }

    public override void UpdateState()
    {
    }

    public override void EndState()
    {
    }
    #endregion





    public void UpdateScore(int score)
    {
        PlayScoreAnimation();
        textScore.text = score.ToString();
    }


    private void PlayScoreAnimation()
    {
        textScore.transform.DOScale(Vector3.one * .45f, 1).SetEase(Ease.InOutQuad)
            .OnComplete(() =>
            {
                textScore.transform.localScale = Vector3.one * .35f;
            });
    }


    public void OnClickedLeaderBoard()
    {
        PlayfabController.Instance.ShowLeaderBoard();
    }


    public void Reset()
    {
        textScore.text = "00";
    }
    

}
