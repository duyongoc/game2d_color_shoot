using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;


public class UILeaderboardItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [Space]
    [SerializeField] private GameObject objMine;
    [SerializeField] private GameObject objHover;

    [Space]
    [SerializeField] private TMP_Text txtRanking;
    [SerializeField] private TMP_Text txtName;
    [SerializeField] private TMP_Text txtScore;


    [Header("DEBUG")]
    [SerializeField] private ItemLeaderBoardData _data;



    public void Init(ItemLeaderBoardData newData)
    {
        _data = newData;

        txtRanking.text = _data.ranking;
        txtName.text = _data.name;
        txtScore.text = _data.score;


        print($"data {_data.id} - {PlayfabController.Instance.playFabId} " + _data.id.Equals(PlayfabController.Instance.playFabId));
        
        switch (_data.id.Equals(PlayfabController.Instance.playFabId))
        {
            case true: objMine.SetActive(true); break;
            case false: objMine.SetActive(false); break;
        }
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        objHover.SetActive(true);
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        objHover.SetActive(false);
    }


}
