using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameManager : Singleton<GameManager>
{

    // [public]
    public static Action EVENT_RESET_INGAME;
    public GameState gameState = GameState.None;

    // [DI]
    [Inject] private ViewManager _viewMgr;


    // [properties]
    public bool IsInGameState => gameState == GameState.InGame;



    #region UNITY
    private void Start()
    {
        InitGame();
    }

    // private void Update()
    // {
    // }
    #endregion



    private void InitGame()
    {
        SetState(gameState);
        SoundManager.PlayMusic(SoundManager.MUSIC_MENU);
    }


    public void PlayGame()
    {
        GameScene.Instance.Reset();
        GameScene.Instance.Init();
        SetState(GameState.InGame);
        SoundManager.PlayMusic(SoundManager.MUSIC_BACKGROUND);
    }


    public void ReplayGame()
    {
        GameScene.Instance.Reset();
        GameScene.Instance.Init();
        SetState(GameState.InGame);
        SoundManager.PlayMusic(SoundManager.MUSIC_BACKGROUND);
    }


    public void GameOver()
    {
        SetState(GameState.GameOver);
        SoundManager.Instance.PlaySFX(SoundManager.SFX_GAMEOVER);
    }


    public void SetState(GameState newState)
    {
        gameState = newState;
        switch (gameState)
        {
            case GameState.Menu:
                _viewMgr.SetStateView("Menu"); break;
            case GameState.InGame:
                _viewMgr.SetStateView("InGame"); break;
            case GameState.GameOver:
                _viewMgr.SetStateView("GameOver"); break;
        }
    }


}



// God bless my code to be bug free 
//
//                       _oo0oo_
//                      o8888888o
//                      88" . "88
//                      (| -_- |)
//                      0\  =  /0
//                    ___/`---'\___
//                  .' \\|     |// '.
//                 / \\|||  :  |||// \
//                / _||||| -:- |||||- \
//               |   | \\\  -  /// |   |
//               | \_|  ''\---/''  |_/ |
//               \  .-\__  '-'  ___/-. /
//             ___'. .'  /--.--\  `. .'___
//          ."" '<  `.___\_<|>_/___.' >' "".
//         | | :  `- \`.;`\ _ /`;.`/ - ` : | |
//         \  \ `_.   \_ __\ /__ _/   .-` /  /
//     =====`-.____`.___ \_____/___.-`___.-'=====
//                       `=---='
//
//
//     ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
//
//               佛祖保佑         永无BUG
//