using System;
using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public class PlayfabController : Singleton<PlayfabController>
{

    [Header("[Setting]")]
    public int currentScore;
    public string leaderboard;
    public string playFabId;

    [Space]
    [SerializeField] private UILeaderboard uiLeaderboard;


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

        RequestLogin();
        uiLeaderboard.Init();
    }



    private void RequestLogin()
    {
        var request = new LoginWithCustomIDRequest { CustomId = SystemInfo.deviceUniqueIdentifier, CreateAccount = true };
        PlayFabClientAPI.LoginWithCustomID(request,
        (result) =>
        {
            GetAccountInfo();
        },
        (error) =>
        {
            Debug.Log(error.GenerateErrorReport());
        });

        // GetLeaderboard();
        // SendLeaderboard(50, "test_1");
    }


    private void GetAccountInfo()
    {
        GetAccountInfoRequest request = new GetAccountInfoRequest();
        PlayFabClientAPI.GetAccountInfo(request,
        (result) =>
        {
            playFabId = result.AccountInfo.PlayFabId;
            // uiLeaderboard.ShowLeaderBoard();
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
