using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleShield : MonoBehaviour
{

    // inspector
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject shieldEffect;
    [SerializeField] private SpriteRenderer spriteRenderer;


    // private
    private float _rotateSpeed;



    #region UNITY
    private void Start()
    {
    }

    private void FixedUpdate()
    {
        if (!GameMgr.Instance.IsInGameState)
            return;

        RotateShield();
    }
    #endregion



    public void SetShield(float speed, Color color)
    {
        _rotateSpeed = speed;
        spriteRenderer.color = color;
    }

    public void SetShieldSpeed(float speed)
    {
        _rotateSpeed = speed;
    }


    private void RotateShield()
    {
        transform.Rotate(0f, 0f, _rotateSpeed * Time.deltaTime);
    }


    public void HideShield()
    {
        _rotateSpeed = 0;
        anim.Play("ShieldHide");
    }


    public void ImpactBullet(Transform other)
    {
        shieldEffect.SpawnToGarbage(other.transform.position, Quaternion.identity);
        anim.Play("ShieldBlink");
    }


}
