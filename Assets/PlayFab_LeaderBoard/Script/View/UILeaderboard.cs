using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UILeaderboard : MonoBehaviour
{

    [Header("Leader Board")]
    [SerializeField] private GameObject objLeaderBoard;
    [SerializeField] private GameObject objUpdateInfo;
    [SerializeField] private GameObject objLoading;

    [Header("Leader Board")]
    [SerializeField] private Transform contentLeaderBoard;
    [SerializeField] private UILeaderboardItem itemPrefab;

    [Header("Pop up input name")]
    [SerializeField] private GameObject uiPopup;
    [SerializeField] private TMP_InputField inputName;
    [SerializeField] private TMP_Text txtYourScore;
    [SerializeField] private TMP_Text txtInfomation;


    #region UNITY
    private void Start()
    {
        // Init();
    }

    // private void Update()
    // {
    // }
    #endregion


    public void Init()
    {
        ShowObjLeaderBoard(false);
        ShowObjUpdateInfo(false);
        ShowObjLoading(false);
    }


    private void ShowYourScore()
    {
        PlayfabController.Instance.GetDisplayName((name) =>
        {
            print("boom " + name);
            txtYourScore.text = $"Your name - {name}: {PlayfabController.Instance.currentScore}";
        });
    }


    public void ShowLeaderBoard()
    {
        // Init();
        // RefeshLeaderBoard();

        var request = new GetLeaderboardRequest
        {
            StatisticName = PlayfabController.Instance.leaderboard,
            StartPosition = 0,
            MaxResultsCount = 10
        };

        PlayFabClientAPI.GetLeaderboard(request,
        (result) =>
        {
            // get leader board and show it
            var board = result.Leaderboard;

            for (int i = 0; i <= board.Count - 1; i++)
            {
                // get value
                var id = board[i].PlayFabId;
                var ranking = (i + 1).ToString();
                var name = board[i].DisplayName;
                var score = board[i].StatValue.ToString();

                // show item
                var item = Instantiate(itemPrefab, contentLeaderBoard);
                item.Init(new ItemLeaderBoardData(id, ranking, name, score));
            }

        },
        (error) => { });
    }


    public void OnClickButtonPostScore()
    {
        ShowPopup(true);
    }


    public void OnClickButtonPost()
    {
        if (string.IsNullOrEmpty(inputName.text))
        {
            ShowTextWrongName();
            return;
        }

        // ShowTextLoading();

        // var currentScore = PlayfabController.Instance.currentScore;
        // PlayfabController.Instance.SetDisplayName(inputName.text);
        // PlayfabController.Instance.SendLeaderboard(currentScore, () =>
        // {
        //     ShowTextPostSucces();
        //     ShowLeaderBoard();
        // });
    }


    public void ShowObjLeaderBoard(bool value)
    {
        objLeaderBoard.SetActive(value);
    }


    public void ShowObjUpdateInfo(bool value)
    {
        objUpdateInfo.SetActive(value);
    }


    public void ShowObjLoading(bool value)
    {
        objLoading.SetActive(value);
    }



    private void RefeshLeaderBoard()
    {
        foreach (Transform child in contentLeaderBoard.transform)
        {
            if (child != null) Destroy(child.gameObject);
        }
    }


    private void ShowPopup(bool value)
    {
        uiPopup.SetActive(value);
    }


    private void ShowTextWrongName()
    {
        txtInfomation.gameObject.SetActive(true);
        txtInfomation.text = "Please enter your name!";
        txtInfomation.color = Color.red;
    }


    private void ShowTextPostSucces()
    {
        txtInfomation.gameObject.SetActive(true);
        txtInfomation.text = "Post score success!";
        txtInfomation.color = Color.green;
    }


    private void ShowTextLoading()
    {
        txtInfomation.gameObject.SetActive(true);
        txtInfomation.text = "Loading...";
        txtInfomation.color = Color.green;
    }



}
