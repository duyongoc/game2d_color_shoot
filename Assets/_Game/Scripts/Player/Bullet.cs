using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{


    // inspector
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject bulletEffect;


    #region UNITY
    private void Start()
    {
    }

    // private void Update()
    // {
    // }
    #endregion


    public void InitBullet(Color color, Vector2 vecDir, float force)
    {
        spriteRenderer.color = color;
        GetComponent<Rigidbody2D>().AddForce(vecDir * force);

        ParticleSystem.MainModule particle = bulletEffect.GetComponent<ParticleSystem>().main;
        particle.startColor = new ParticleSystem.MinMaxGradient(color);
    }


    public void SelfDestroy()
    {
        GetComponent<Collider2D>().enabled = false;
        bulletEffect.SpawnToGarbage(transform.position, Quaternion.identity);
        Destroy(gameObject);
    }


    private void ImpactShield()
    {
        SoundMgr.Instance.PlaySFX(SoundMgr.SFX_SHOOT_HIT);
        SelfDestroy();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Shield"))
        {
            other.GetComponentInParent<ObstacleShield>()?.ImpactBullet(transform);

            GameScene.Instance.HasFinish = true;
            GameScene.Instance.GameOver();
            ImpactShield();
        }
    }


}
