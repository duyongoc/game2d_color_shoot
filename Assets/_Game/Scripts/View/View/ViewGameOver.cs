using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ViewGameOver : View
{

    [Space]
    [SerializeField] private Text txtScore;
    [SerializeField] private Text txtHighScore;
    [SerializeField] private bool _isDoneGameover;



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
        Load();
    }

    public override void UpdateState()
    {
    }

    public override void EndState()
    {
    }
    #endregion



    private void Load()
    {
        var score = ScoreManager.Instance.score;
        var playfab = PlayfabController.Instance;

        txtScore.text = $"{score.ToString()}";
        txtHighScore.text = $"Best: {playfab.HighScore}"; 
        playfab.CheckShowRecordScore(score);
    }


    public void OnClickButtonReplay()
    {
        GameManager.Instance.ReplayGame();
    }


    public void OnClickButtonMenu()
    {
        GameScene.Instance.Reset();
        GameManager.Instance.SetState(GameState.Menu);
        SoundManager.PlayMusic(SoundManager.MUSIC_BACKGROUND);
    }


    public void OnClickButtonLeaderBoard()
    {
        PlayfabController.Instance.ShowLeaderBoard();
    }


}
