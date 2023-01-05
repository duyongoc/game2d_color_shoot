using System;
using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public class PlayfabController : Singleton<PlayfabController>
{

    [Header("[Setting]")]
    [SerializeField] private int currentScore;
    [SerializeField] private string leaderboard;
    [SerializeField] private string playFabId;

    [Space]
    [SerializeField] private UILeaderboard uiLeaderboard;


    // [properties]
    public int CurrentScore { get => currentScore; set => currentScore = value; }



    #region UNITY
    private void Start()
    {
        Init();
    }

    // private void Update()
    // {
    // }
    #endregion




    private void Init()
    {
        if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId))
        {
            // Please change the titleId below to your own titleId from PlayFab Game Manager.
            // If you have already set the value in the Editor Extensions, this can be skipped.
            PlayFabSettings.staticSettings.TitleId = "2E41C";
        }

        RequestLogin(() => { GetAccountInfo(); });
        uiLeaderboard.Init();

        // GetLeaderboard();
    }


    public void RecordScore()
    {
        
    }


    public void ShowLeaderBoard()
    {
        uiLeaderboard.ShowObjLoading(true);
        RequestLogin(() =>
        {
            uiLeaderboard.RefeshLeaderBoard();
            RequestTopPlayers();
        });
    }


    private void RequestLogin(Action cbSuccess = null, Action cbFail = null)
    {
        // request for login
        var request = new LoginWithCustomIDRequest { CustomId = SystemInfo.deviceUniqueIdentifier, CreateAccount = true };
        PlayFabClientAPI.LoginWithCustomID(request,
        (result) =>
        {
            cbSuccess?.Invoke();
        },
        (error) =>
        {
            Debug.Log(error.GenerateErrorReport());
            cbFail?.Invoke();
        });
    }


    public void RequestTopPlayers()
    {
        // create request and get 10 top players
        var request = new GetLeaderboardRequest
        {
            StatisticName = PlayfabController.Instance.leaderboard,
            StartPosition = 0,
            MaxResultsCount = 10
        };

        // get leader board and show it
        PlayFabClientAPI.GetLeaderboard(request,
        (result) =>
        {
            ShowTopPlayers(result.Leaderboard);
            uiLeaderboard.ShowObjLoading(false);
        },
        (error) =>
        {
            Debug.Log(error.GenerateErrorReport());
        });
    }


    private void ShowTopPlayers(List<PlayerLeaderboardEntry> board)
    {
        for (int index = 0; index <= board.Count - 1; index++)
        {
            uiLeaderboard.CreateItemPlayer(board[index], index);
        }

        // check if user on the leader board
        GetAccountInfo((x) => { uiLeaderboard.CheckMeOnLeaderBoard(x); });
    }


    private void GetAccountInfo(Action<string> cbSuccess = null)
    {
        PlayFabClientAPI.GetAccountInfo(new GetAccountInfoRequest(),
        (result) =>
        {
            print("GetAccountInfo");
            // SendLeaderboard(50, "test_4");
            
            playFabId = result.AccountInfo.PlayFabId;
            cbSuccess?.Invoke(playFabId);
        },
        (error) =>
        {
            Debug.Log(error.GenerateErrorReport());
        });
    }


    public void GetPlayerProfile()
    {
        PlayFabClientAPI.GetPlayerProfile(new GetPlayerProfileRequest()
        {
            PlayFabId = playFabId,
            ProfileConstraints = new PlayerProfileViewConstraints() { ShowDisplayName = true }
        },
        (result) =>
        {
            Debug.Log("The player's DisplayName profile data is: " + result.PlayerProfile);
        },
        (error) =>
        {
            Debug.Log(error.GenerateErrorReport());
        });
    }


    public void GetDisplayName(Action<string> cb_success = null)
    {
        PlayFabClientAPI.GetPlayerProfile(new GetPlayerProfileRequest()
        {
            PlayFabId = playFabId,
            ProfileConstraints = new PlayerProfileViewConstraints() { ShowDisplayName = true }
        },
        (result) =>
        {
            cb_success?.Invoke(result.PlayerProfile.DisplayName);
        },
        (error) => { Debug.LogError(error.GenerateErrorReport()); });
    }


    public void SetDisplayName(string name)
    {
        var requestName = new UpdateUserTitleDisplayNameRequest { DisplayName = name, };
        PlayFabClientAPI.UpdateUserTitleDisplayName(requestName,
        (result) => { },
        (error) => { Debug.LogError(error.GenerateErrorReport()); });
    }


    // public void SendLeaderboard(int score, Action cb_success = null)
    // {
    //     var request = new UpdatePlayerStatisticsRequest
    //     {
    //         Statistics = new List<StatisticUpdate> { new StatisticUpdate { StatisticName = leaderboard, Value = score } }
    //     };

    //     // send request for update score
    //     PlayFabClientAPI.UpdatePlayerStatistics(request,
    //     (result) => { cb_success?.Invoke(); },
    //     (error) => Debug.LogError(error.GenerateErrorReport()));
    // }


    public void SendLeaderboard(int score, string name, Action cb_success = null)
    {
        // set name for display
        SetDisplayName(name);

        // create update request
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate> { new StatisticUpdate { StatisticName = leaderboard, Value = score } }
        };

        // send request for update score
        PlayFabClientAPI.UpdatePlayerStatistics(request,
        (result) => { cb_success?.Invoke(); },
        (error) => Debug.LogError(error.GenerateErrorReport()));
    }


    public void GetLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = leaderboard,
            StartPosition = 0,
            MaxResultsCount = 20
        };

        PlayFabClientAPI.GetLeaderboard(request,
        result =>
        {
            result.Leaderboard.ForEach(x =>
            {
                Debug.Log($"x position{x.Position} {x.DisplayName} {x.StatValue}");
            });
        },
        error => Debug.LogError(error.GenerateErrorReport()));
    }




}
