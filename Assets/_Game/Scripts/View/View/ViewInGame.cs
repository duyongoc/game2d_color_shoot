using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class ViewInGame : View
{

    [Space]
    [SerializeField] private TMP_Text textScore;



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


    public void Reset()
    {
        textScore.text = "00";
    }


    public void OnClickButtonMenu()
    {
        Reset();
        GameScene.Instance.Reset();
        GameManager.Instance.SetState(GameState.Menu);
        SoundManager.PlayMusic(SoundManager.MUSIC_BACKGROUND);
    }



}
