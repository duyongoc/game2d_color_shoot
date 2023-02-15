using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{


    [Header("[Setting]")]
    [SerializeField] private Transform bulletPrefab;
    [SerializeField] private float bulletSpeed = 25f;

    [Space(10)]
    [SerializeField] private float timeMove = 2f;
    [SerializeField] private SpriteRenderer spriteRenderer;


    // [private]
    private Color _color;
    private GameScene _gameScene;
    private List<Bullet> bulletList;
    private bool _isMoving = false;
    private bool _canShoot = true;



    #region UNITY
    private void OnEnable()
    {
        if (PlayfabController.Instance != null)
        {
            PlayfabController.Instance.OnEventShowLeaderBoard += OnShowLeaderBoard;
            PlayfabController.Instance.OnEventHideLeaderBoard += OnHideLeaderBoard;
        }
    }

    private void OnDisable()
    {
        if (PlayfabController.Instance != null)
        {
            PlayfabController.Instance.OnEventShowLeaderBoard -= OnShowLeaderBoard;
            PlayfabController.Instance.OnEventHideLeaderBoard -= OnHideLeaderBoard;
        }
    }

    private void Start()
    {
        _gameScene = GameScene.Instance;
        bulletList = new List<Bullet>();
    }

    private void Update()
    {
        if (!GameManager.Instance.IsInGameState)
            return;

        UpdateShotBullet();
    }
    #endregion




    private void UpdateShotBullet()
    {
        if (_isMoving || !_canShoot)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            // check if clicked on ui 
            if (EventSystem.current.IsPointerOverGameObject() && EventSystem.current.currentSelectedGameObject)
            {
                Debug.Log("Mouse Over: " + EventSystem.current.currentSelectedGameObject.name);
                return;
            }

            ShootingTarget();
        }
    }


    public void SetPlayer(Color color)
    {
        _color = color;
        spriteRenderer.color = _color;
    }


    public void ShootingTarget()
    {
        var bullet = bulletPrefab.SpawnToGarbage(transform.position, Quaternion.identity);
        var vecDir = _gameScene.GetNextObstacle.position - transform.position;
        bullet.GetComponent<Bullet>().InitBullet(_color, vecDir.normalized, bulletSpeed);
        bulletList.Add(bullet.GetComponent<Bullet>());

        SoundManager.Instance.PlaySFX(SoundManager.SFX_SHOOT);
    }


    public void MoveToTarget(Vector3 target, Action callback = null)
    {
        _isMoving = true;
        transform.DOKill();
        ClearCache();

        transform.DOMove(target, timeMove)
            .SetEase(Ease.InOutQuad)
            .OnComplete(() =>
            {
                callback?.Invoke();
                _isMoving = false;
            });
    }


    private void OnShowLeaderBoard()
    {
        _canShoot = false;
    }


    private void OnHideLeaderBoard()
    {
        _canShoot = true;
    }


    private void ClearCache()
    {
        bulletList.ForEach(x => { if (x != null) { x.SelfDestroy(); } });
        bulletList.Clear();
    }


    public void Reset()
    {
        _canShoot = true;
        _isMoving = false;
        transform.DOKill();
        transform.position = new Vector2(0, -2.5f);
    }

}
