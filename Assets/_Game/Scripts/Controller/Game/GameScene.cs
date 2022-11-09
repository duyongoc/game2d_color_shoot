using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Zenject;

public class GameScene : Singleton<GameScene>
{

    [Header("Config")]
    public LevelDesign levelDesign;

    // inspector
    [Space(10)]
    [SerializeField] private Player player;
    [SerializeField] private Transform obstaclePrefab;
    [SerializeField] private Color[] colors;

    [SerializeField] private float minDistanceY = 5f;
    [SerializeField] private float maxDistanceY = 8f;
    [SerializeField] private float maxDistanceX = 2.5f;


    // private
    private GameObject _curObstacle;
    private GameObject _nextObstacle;
    private List<GameObject> obstacleList;
    private TurnData _currentTurn;
    private int _currentIndex = 0;
    private Color _curColor;
    private Color _nextColor;
    private bool _hasFinish;


    // DI
    [Inject] ViewInGame _viewInGame;


    public Transform GetNextObstacle => _nextObstacle.transform;
    public Transform GetPlayer => player.transform;
    public bool HasFinish { get => _hasFinish; set => _hasFinish = value; }


    #region UNITY
    private void Start()
    {
        CacheComponent();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            CreateNextObstacle();
        }
    }
    #endregion


    public void Init()
    {
        FirstSpawnObstacle();
    }


    private void FirstSpawnObstacle()
    {
        // create first obstable and break it for the player
        _curColor = colors[Random.Range(0, colors.Length)];
        player.SetPlayer(_curColor);

        _curObstacle = obstaclePrefab.SpawnToGarbage(player.transform.position, Quaternion.identity);
        _curObstacle.GetComponent<Obstacle>().InitObstacle(_curColor, null, true);
        obstacleList.Add(_curObstacle);

        // create next obstacle
        CreateNextObstacle();
    }


    private void CreateNextObstacle()
    {
        _nextColor = colors[Random.Range(0, colors.Length)];
        var positionX = Random.Range(-maxDistanceX, maxDistanceX);
        var positionY = Random.Range(minDistanceY, maxDistanceY);

        var pos = new Vector2(_curObstacle.transform.position.x + positionX, _curObstacle.transform.position.y + positionY);
        _nextObstacle = obstaclePrefab.SpawnToGarbage(pos, Quaternion.identity);
        obstacleList.Add(_nextObstacle);

        _currentIndex++;
        _currentTurn = levelDesign.GetTurn(_currentIndex);
        _nextObstacle.GetComponent<Obstacle>().InitObstacle(_nextColor, _currentTurn, false);
        _curObstacle.GetComponent<Obstacle>().ActiveAniming(_nextObstacle.transform.position);
    }


    private void NextTurn()
    {
        _curColor = _nextColor;
        player.SetPlayer(_curColor);

        // re-sign current obstacle
        _curObstacle = _nextObstacle;
        CreateNextObstacle();
    }


    public void MovePlayer()
    {
        SoundMgr.Instance.PlaySFX(SoundMgr.SFX_PASS_OBSTACLE);
        player.MoveToTarget(_nextObstacle.transform.position, () => { NextTurn(); });
    }


    public void UpdateScore()
    {
        _viewInGame.UpdateScore(ScoreMgr.Instance.score);
    }


    public void ShakeCamera()
    {
        var camera = Camera.main;
        camera.DOShakeRotation(1f, new Vector3(1.75f, 1.75f, 0), 10, 0)
            .OnComplete(() => { camera.transform.localRotation = Quaternion.identity; });
    }


    public void GameOver()
    {
        ShakeCamera();
        SoundMgr.StopMusic();

        DG.Tweening.DOVirtual.DelayedCall(1f, () => { GameMgr.Instance.GameOver(); });
    }


    private void CacheComponent()
    {
        obstacleList = new List<GameObject>();
    }


    public void ResetGame()
    {
        _hasFinish = false;
        _currentIndex = 0;
        _viewInGame.ResetData();
        player.ResetData();

        obstacleList.ForEach(x => { if (x != null) Destroy(x); });
        obstacleList.Clear();

        GameMgr.EVENT_RESET_INGAME?.Invoke();

        // refesh game
        Init();
    }




}