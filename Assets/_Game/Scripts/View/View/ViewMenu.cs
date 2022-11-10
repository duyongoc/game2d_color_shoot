using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewMenu : View
{



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
        if (Input.GetMouseButtonUp(0))
        {
            GameManager.Instance.PlayGame();
        }
    }

    public override void EndState()
    {
    }
    #endregion






}
