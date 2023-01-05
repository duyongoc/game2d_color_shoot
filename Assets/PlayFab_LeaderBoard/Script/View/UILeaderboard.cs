using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
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
    [SerializeField] private List<UILeaderboardItem> itemCaches;

    [Header("Pop up input name")]
    [SerializeField] private GameObject uiPopup;
    [SerializeField] private TMP_InputField inputName;
    [SerializeField] private TMP_Text txtYourScore;
    [SerializeField] private TMP_Text txtInfomation;


    #region UNITY
    private void Start()
    {
        Init();
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


    public void RefeshLeaderBoard()
    {
        // clear old item cache
        itemCaches.ForEach(x => { if (x != null) Destroy(x.gameObject); });
        itemCaches.Clear();

        ShowObjLeaderBoard(true);
    }


    public void CreateItemPlayer(PlayerLeaderboardEntry player, int index)
    {
        // get value
        var id = player.PlayFabId;
        var ranking = (index + 1).ToString();
        var name = player.DisplayName;
        var score = player.StatValue.ToString();

        // show item
        var item = Instantiate(itemPrefab, contentLeaderBoard);
        item.Init(new ItemLeaderBoardData(id, ranking, name, score));
        itemCaches.Add(item);
    }


    public void CheckMeOnLeaderBoard(string id)
    {
        var mine = itemCaches.Find(x => x.GetData.id.Equals(id));
        print($"id {id} mine: {mine}");
        if (mine != null)
        {
            mine.ShowMineBackground(true);
        }
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

    private void ShowYourScore()
    {
        PlayfabController.Instance.GetDisplayName((name) =>
        {
            print("boom " + name);
            txtYourScore.text = $"Your name - {name}: {PlayfabController.Instance.CurrentScore}";
        });
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


    public void OnClickButtonExit()
    {
        ShowObjLeaderBoard(false);
        ShowObjUpdateInfo(false);
        ShowObjLoading(false);
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
