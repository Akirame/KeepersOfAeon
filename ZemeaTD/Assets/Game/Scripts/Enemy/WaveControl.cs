using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveControl : MonoBehaviour {

    #region singleton
    private static WaveControl instance;
    public static WaveControl GetInstance()
    {
        return instance;
    }
    private void Awake()
    {
        if (!instance)
            instance = this;
        else
            Destroy(this.gameObject);
    }
    #endregion

    public Enemy[] enemyPrefab;
    public Transform[] spawnPoints;
    public int meleeCount;
    public int rangeCount;
    public int flyCount;
    public int currentWave;
    public int timeBetweenWaves;
    public int timeBetweenEnemies;
    public GameObject miniBoss;
    public int waveMiniBoss;
    public GameObject enemiesParent;
    public int enemyIncrementFactor = 2;
    private List<GameObject> enemyList;
    private List<int> enemyTypeList;
    private float timerWaves;
    private float timerEnemies;
    private bool canSpawn = true;


    // Use this for initialization
    void Start () {
        enemyTypeList = new List<int>();
        enemyList = new List<GameObject>();
        Enemy.Death += EnemyKilled;
        if (DebugScreen.GetInstance())
        {
            DebugScreen.GetInstance().AddButton("Kill All Enemies", KillAllEnemies);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.F1))
            KillAllEnemies();
        if (canSpawn)
        {
            timerWaves += Time.deltaTime;
            if (timerWaves >= timeBetweenWaves)
            {
                CleanWave();
                SpawnWave();
                TrySpawnMiniBoss();
                timerWaves = 0;
            }
        }
        else
            CheckEnemyCount();
	}

    private void TrySpawnMiniBoss()
    {
        if (currentWave % waveMiniBoss == 0)
        {
            Transform t = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
            GameObject enemy = Instantiate(miniBoss, t.position + new Vector3(0,20,0), Quaternion.identity, enemiesParent.transform);
            enemyList.Add(enemy);
        }
    }

    private void CheckEnemyCount()
    {
        if (enemyList.Count <= 0)
        {
            canSpawn = true;
        }
    }

    private void SpawnWave()
    {
        GenerateEnemyList();
        StopAllCoroutines();
        StartCoroutine(SpawnEnemyList());
        canSpawn = false;
        currentWave++;
    }

    IEnumerator SpawnEnemyList()
    {
        for (int i = 0; i < enemyTypeList.Count; i++)
        {
            Transform t = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
            GameObject enemy = Instantiate(enemyPrefab[enemyTypeList[i]].gameObject, t.position, Quaternion.identity, enemiesParent.transform);
            enemyList.Add(enemy);
            yield return new WaitForSeconds(timeBetweenEnemies);
        }
    }

    private void EnemyKilled(Enemy e)
    {
        enemyList.Remove(e.gameObject);
        Destroy(e.gameObject);
    }

    private void GenerateEnemyList()
    {
        for (int i = 0; i < meleeCount + enemyIncrementFactor * currentWave; i++)
        {
            enemyTypeList.Add(0);
        }
        for (int i = 0; i < rangeCount + enemyIncrementFactor * currentWave; i++)
        {
            enemyTypeList.Add(1);
        }
        for (int i = 0; i < flyCount + enemyIncrementFactor * currentWave; i++)
        {
            enemyTypeList.Add(2);
        }
        RandomizeEnemyList();
    }

    private void RandomizeEnemyList()
    {
        for (int i = 0; i < enemyTypeList.Count; i++)
        {
            int randomIndex = UnityEngine.Random.Range(i, enemyTypeList.Count);
            int aux = enemyTypeList[i];
            enemyTypeList[i] = enemyTypeList[randomIndex];
            enemyTypeList[randomIndex] = aux;
        }
    }

    public void KillAllEnemies()
    {
        StopAllCoroutines();
        for (int i = enemyList.Count - 1; i >= 0; i--)
        {
            enemyList[i].GetComponent<Enemy>().Kill();
        }
        CleanWave();
        canSpawn = true;
    }

    private void CleanWave()
    {
        enemyTypeList.Clear();
        enemyList.Clear();
    }
}
