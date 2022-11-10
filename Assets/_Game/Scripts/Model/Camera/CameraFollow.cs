using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [Header("Follow speed")]
    [Range(0f, 50f)] public float moveSpeed = 5f;

    [Header("Camera Offset")]
    [Range(-10f, 10f)] public float yOffset;
    [Range(-10f, 10f)] public float xOffset;


    // private 
    private Transform _target;


    #region UNITY
    private void OnEnable()
    {
        GameManager.EVENT_RESET_INGAME += ResetData;
    }

    private void OnDisable()
    {
        GameManager.EVENT_RESET_INGAME -= ResetData;
    }


    private void Start()
    {
        _target = GameScene.Instance.GetPlayer;
    }

    private void LateUpdate()
    {
        if (!GameManager.Instance.IsInGameState)
            return;

        FollowToTarget();
    }
    #endregion



    private void FollowToTarget()
    {
        var speed = moveSpeed * Time.deltaTime;
        if (_target.position.y + yOffset > transform.position.y)
        {
            var vec = new Vector3(_target.position.x + xOffset, _target.position.y + yOffset, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, vec, speed);
        }
    }


    private void ResetData()
    {
        transform.position = new Vector3(0, 0, transform.position.z);
    }

}
