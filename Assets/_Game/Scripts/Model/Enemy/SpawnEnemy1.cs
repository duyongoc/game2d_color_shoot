using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnEnemy1 : MonoBehaviour
{

    public enum SpawnState
    {
        Init,
        Spawn,
        None
    };

    [Header("[Setting]")]
    public GameObject enemyPrefab;
    public SpawnState currentState;

    [Header("Pools")]
    [SerializeField] private int poolLength = 10;
    [SerializeField] private bool hasExpand = true;


    // [private]
    private List<GameObject> listEnemiesCreated;
    private Timer timeSpawn = new Timer();
    private Transform target;

    // private float moveSpeed;
    private float minRangeSpawn;
    private float maxRangeSpawn;



    #region UNITY
    private void Start()
    {
        CacheComponent();
        CacheDefine();
        InitPoolsObject();
    }

    private void Update()
    {
        // if (!GameMgr.Instance.InGameState)
        // return;

        switch (currentState)
        {
            case SpawnState.Init: InitSpawnWarningEnemy(); break;
            case SpawnState.Spawn: SpawnEnemy(); break;
            case SpawnState.None: break;
        }
    }
    #endregion



    private void InitSpawnWarningEnemy()
    {
        CreateEnemy(new Vector3(30, 30, 0), Quaternion.identity);
        CreateEnemy(new Vector3(-30, 30, 0), Quaternion.identity);
        currentState = SpawnState.Spawn;
    }


    private void SpawnEnemy()
    {
        timeSpawn.UpdateTime(Time.deltaTime);
        if (timeSpawn.IsDone)
        {
            CreateEnemy(GetRandomPoint(), Quaternion.identity);
            timeSpawn.Reset();
        }
    }


    private Vector2 GetRandomPoint()
    {
        Vector2 vec = new Vector3(target.position.x, target.position.y, 0);
        var randomDirection = (Random.insideUnitCircle * vec).normalized;
        var randomDistance = Random.Range(minRangeSpawn, maxRangeSpawn);
        var point = vec + randomDirection * randomDistance;
        return point;
    }


    private GameObject CreateEnemy(Vector3 newPos, Quaternion newRot)
    {
        GameObject newObject = GetPoolObject();
        if (newObject == null)
            Debug.Log("Not exist object from Pool.");

        newObject.transform.position = newPos;
        newObject.transform.rotation = newRot;
        newObject.gameObject.SetActive(true);
        return newObject;
    }


    private GameObject GetPoolObject()
    {
        GameObject newObject = null;
        for (int i = 0; i < listEnemiesCreated.Count; i++)
        {
            if (!listEnemiesCreated[i].activeSelf)
            {
                return listEnemiesCreated[i];
            }
        }

        if (hasExpand)
        {
            newObject = Instantiate(enemyPrefab, new Vector2(0, -100), Quaternion.identity, transform);
            listEnemiesCreated.Add(newObject);
        }

        return newObject;
    }


    private void InitPoolsObject()
    {
        for (int i = 0; i < poolLength; i++)
        {
            GameObject newObject = Instantiate(enemyPrefab, new Vector2(0, -100), Quaternion.identity, transform);
            listEnemiesCreated.Add(newObject);
        }
    }


    public void Reset()
    {
        timeSpawn.Reset();
        currentState = SpawnState.Init;
        // listEnemiesCreated.ForEach(x => x.GetComponent<Enemy>().Reset());
        // moveSpeed = 0;
    }


    private void CacheDefine()
    {
        listEnemiesCreated = new List<GameObject>();
        // minRangeSpawn = CONFIG.minRangeSpawn;
        // maxRangeSpawn = CONFIG.maxRangeSpawn;
        // moveSpeed = CONFIG.moveSpeed;
        // timeSpawn.SetDuration(CONFIG.timeSpawn);
    }


    private void CacheComponent()
    {
        // target = Character.Instance.transform;
    }

}
