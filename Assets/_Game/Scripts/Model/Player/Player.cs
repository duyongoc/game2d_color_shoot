using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Player : MonoBehaviour
{


    [Header("Bullet")]
    [SerializeField] private Transform bulletPrefab;
    [SerializeField] private float bulletSpeed = 25f;

    [Space(10)]
    [SerializeField] private float timeMove = 2f;
    [SerializeField] private SpriteRenderer spriteRenderer;


    // private
    private Color _color;
    private GameScene _gameScene;
    private List<Bullet> bulletList;
    private bool _isMoving = false;


    #region UNITY
    private void Start()
    {
        CacheComponent();
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
        if (_isMoving)
            return;

        if (Input.GetMouseButtonDown(0))
        {
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
        ClearAllBullets();

        transform.DOMove(target, timeMove)
            .SetEase(Ease.InOutQuad)
            .OnComplete(() =>
            {
                callback?.Invoke();
                _isMoving = false;
            });
    }


    private void ClearAllBullets()
    {
        bulletList.ForEach(x =>
        {
            if (x != null)
            {
                x.SelfDestroy();
                // DG.Tweening.DOVirtual.DelayedCall(0.25f, () => { });
            }
        });
        bulletList.Clear();
    }


    public void CacheComponent()
    {
        _gameScene = GameScene.Instance;
        bulletList = new List<Bullet>();
    }


    public void ResetData()
    {
        transform.DOKill();
        transform.position = new Vector2(0, -2.5f);
    }

}
