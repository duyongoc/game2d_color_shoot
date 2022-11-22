using System.Collections;
using System.Collections.Generic;
using Pools;
using UnityEngine;

public class GameBoard : MonoBehaviour
{

    [Header("Config")]
    public LevelDesign levelDesign;

    [Space(15)]
    [SerializeField] private Player player;
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private GameObject objectPrefab;
    [SerializeField] private Color[] colors;


    [Space(15)]
    [SerializeField] private float minDistanceY = 5f;
    [SerializeField] private float maxDistanceY = 8f;
    [SerializeField] private float maxDistanceX = 2.5f;


    // private
    private List<GameObject> obstacleList;
    private GameObject _curObstacle;
    private GameObject _nextObstacle;
    private int _currentIndex = 0;

    private TurnData _currentTurn;
    private Color _curColor;
    private Color _nextColor;
    private bool _hasFinish;


    public void Start()
    {
        Init();

    }


    public void Init()
    {
        print("Init");
        obstaclePrefab.InitPoolPrefab(10, transform);
        objectPrefab.InitPoolPrefab(10, transform);
        // obstaclePrefab.Reuse();
        // FirstSpawnObstacle();

        Debug.Log(obstaclePrefab.Reuse());
    }


    private void FirstSpawnObstacle()
    {
        // create first obstable and break it for the player
        _curColor = colors[Random.Range(0, colors.Length)];
        player.SetPlayer(_curColor);



        // _curObstacle = obstaclePrefab.SpawnToGarbage(player.transform.position, Quaternion.identity);
        // _curObstacle.GetComponent<Obstacle>().InitObstacle(_curColor, null, true);
        // obstacleList.Add(_curObstacle);

        // // create next obstacle
        // CreateNextObstacle();
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
}
