using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{


    [Header("[Setting]")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject bulletEffect;


    #region UNITY
    // private void Start()
    // {
    // }

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
        SoundManager.Instance.PlaySFX(SoundManager.SFX_SHOOT_HIT);
        SelfDestroy();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!GameManager.Instance.IsInGameState)
            return;

        if (other.CompareTag("Shield"))
        {
            // impact with the bullet
            other.GetComponentInParent<ObstacleShield>()?.ImpactBullet(transform);

            // set gameover state
            ImpactShield();
            this.PostEvent(EventID.OnEvent_GameOver);
        }
    }


}
