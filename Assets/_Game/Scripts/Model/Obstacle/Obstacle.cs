using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    // inspecter
    [SerializeField] private int scorePlus = 5;
    [SerializeField] private GameObject scorePrefab;
    [SerializeField] private GameObject shieldPrefab;
    [SerializeField] private List<ObstacleShield> shields;

    [Space(10)]
    [SerializeField] private TextMesh textMesh;
    [SerializeField] private Transform aimTarget;
    [SerializeField] private SpriteRenderer[] sprites;

    [Header("Anim")]
    [SerializeField] private Animator aimAnim;
    [SerializeField] private Animator waveAnim;


    // private
    private Color _color;
    private Transform _nextObstacle;
    private int _value;

    private TurnData _turn;
    private bool _hasBroken = false;


    #region UNITY
    private void Start()
    {
    }

    // private void Update()
    // {
    // }
    #endregion



    public void InitObstacle(Color color, TurnData newTurn = null, bool hasBroken = false)
    {
        _color = color;
        _hasBroken = hasBroken;

        textMesh.color = _color;
        sprites.ToList().ForEach(x => x.color = _color);

        // if object already broken
        if (_hasBroken)
        {
            textMesh.gameObject.SetActive(false);
        }

        // set obstacle with level design
        if (newTurn != null)
        {
            _turn = newTurn;
            _value = Random.Range(_turn.minValue, _turn.maxValue);
            textMesh.text = _value.ToString();

            CreateShield();
        }

    }


    private void CreateShield()
    {
        int angle = 0;
        int angleOfShield = 360 / _turn.shieldCount;

        var randSpeed = Random.Range(_turn.minShieldSpeed, _turn.maxShieldSpeed + 1);
        randSpeed = Random.Range(0f, 1f) > 0.5f ? randSpeed : -randSpeed;

        for (int i = 0; i < _turn.shieldCount; i++)
        {
            var shield = Instantiate(shieldPrefab, transform);
            shield.transform.localEulerAngles = new Vector3(0f, 0f, angle);

            switch (_turn.randomRotate)
            {
                case false:
                    shield.GetComponent<ObstacleShield>().SetShield(randSpeed, _color); break;

                case true:
                    randSpeed = Random.Range(_turn.minShieldSpeed, _turn.maxShieldSpeed + 1);
                    randSpeed = Random.Range(0f, 1f) > 0.5f ? randSpeed : -randSpeed;
                    shield.GetComponent<ObstacleShield>().SetShield(randSpeed, _color); break;
            }

            angle += angleOfShield;
            shields.Add(shield.GetComponent<ObstacleShield>());
        }

        ChangeSpeedShields(_turn.timeChangeSpeed);
    }


    private async void ChangeSpeedShields(float timerChange)
    {
        await UniTask.Delay(System.TimeSpan.FromSeconds(timerChange));
        shields.ForEach(x => x.SetShieldSpeed(0));

        await UniTask.Delay(System.TimeSpan.FromSeconds(0.25f));

        var randSpeed = Random.Range(_turn.minShieldSpeed, _turn.maxShieldSpeed + 1);
        randSpeed = Random.Range(0f, 1f) > 0.5f ? randSpeed : -randSpeed;

        switch (_turn.randomRotate)
        {
            case false:
                shields.ForEach(x => x.SetShieldSpeed(randSpeed)); break;

            case true:
                shields.ForEach(x =>
                {
                    randSpeed = Random.Range(_turn.minShieldSpeed, _turn.maxShieldSpeed + 1);
                    randSpeed = Random.Range(0f, 1f) > 0.5f ? randSpeed : -randSpeed;
                    x.SetShieldSpeed(randSpeed);
                }); break;
        }

        ChangeSpeedShields(_turn.timeChangeSpeed);
    }


    private void InsideThePlayer()
    {
        waveAnim.Play("WaveInside");
        sprites[0].enabled = false;
    }


    private void OutsideThePlayer()
    {
        waveAnim.Play("WaveOut");
    }


    public void ActiveAniming(Vector3 target)
    {
        aimTarget.gameObject.SetActive(value: true);
        Vector2 vec = ((Vector2)target - (Vector2)transform.position).normalized;

        float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
        aimTarget.localEulerAngles = new Vector3(0f, 0f, angle - 90f);
    }


    public void ShakeObstacle()
    {
        Vector2 origin = transform.localPosition;
        transform.DOShakePosition(0.5f, Vector2.one * 0.01f, 10, 0)
            .OnComplete(() => { transform.localPosition = origin; });
    }


    private void AddScore(int newScore)
    {
        var score = scorePrefab.SpawnToGarbage(transform.position, Quaternion.identity);
        score.GetComponent<TextScore>().Init(newScore, _color);

        ScoreMgr.Instance.UpdateScore(newScore);
        GameScene.Instance.UpdateScore();
    }


    private void UpdateObstacleValue()
    {
        _value--;
        textMesh.text = _value.ToString();

        AddScore(scorePlus);
        ShakeObstacle();
        SoundMgr.Instance.PlaySFX(SoundMgr.SFX_SHOOT_HIT);
    }


    private void SelfDestroy()
    {
        _hasBroken = true;
        textMesh.transform.DOScale(Vector3.zero, 1);
        shields.ForEach(x => x.HideShield());

        GameScene.Instance.MovePlayer();
    }


    private void ImpactWithBullet(Bullet bullet)
    {
        if (GameScene.Instance.HasFinish)
            return;

        UpdateObstacleValue();
        bullet.SelfDestroy();

        if (_value <= 0)
        {
            SelfDestroy();
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Contains("Player"))
            InsideThePlayer();

        if (_hasBroken)
            return;

        switch (other.tag)
        {
            case "Bullet":
                ImpactWithBullet(other.GetComponent<Bullet>());
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Player": OutsideThePlayer(); break;
        }
    }


}
