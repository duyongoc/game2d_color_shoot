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
        StartView();
    }

    public override void UpdateState()
    {
        UpdateView();
    }

    public override void EndState()
    {
        EndView();
    }
    #endregion


    private void StartView()
    {
    }

    private void UpdateView()
    {
    }

    private void EndView()
    {
    }




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


    public void ResetData()
    {
        textScore.text = "00";
    }


}
