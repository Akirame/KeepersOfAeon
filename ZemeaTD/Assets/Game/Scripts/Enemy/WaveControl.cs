﻿using System;
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
    public int enemyCount;
    public int currentWave;
    public int timeBetweenWaves;
    public int timeBetweenEnemies;
    public GameObject miniBoss;
    public int waveMiniBoss;
    public GameObject enemiesParent;
    public int enemyIncrementFactor = 2;
    public int totalEnemyCount;
    public int timeForFirstWave = 30;
    private List<GameObject> enemyList;
    private List<int> enemyTypeList;
    private float timerFirstWave;
    private float timerWaves;
    private float timerEnemies;
    private bool canSpawn = false;
    private bool firstWave = true;


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
        if (firstWave)
        {
            timerFirstWave += Time.deltaTime;
            if (timerFirstWave >= timeForFirstWave)
            {
                canSpawn = true;
                firstWave = false;
            }
        }
        else
        {
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
        if (enemyCount <= 0)
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
            Vector3 offsetPosition = new Vector3();
            offsetPosition.y = UnityEngine.Random.Range(-3, 3);
            GameObject enemy = Instantiate(enemyPrefab[enemyTypeList[i]].gameObject, t.position + offsetPosition, Quaternion.identity, enemiesParent.transform);
            enemy.GetComponent<Enemy>().MultiplyHealth((int)currentWave/5);
            enemyList.Add(enemy);
            yield return new WaitForSeconds(timeBetweenEnemies);
        }
    }

    private void EnemyKilled(Enemy e)
    {
        enemyList.Remove(e.gameObject);
        Destroy(e.gameObject);
        enemyCount--;
    }

    private void GenerateEnemyList()
    {
        for (int i = 0; i < meleeCount + (int)(enemyIncrementFactor * currentWave); i++)
        {
            enemyTypeList.Add(0);
        }
        for (int i = 0; i < rangeCount + (int)(enemyIncrementFactor * currentWave / 3); i++)
        {
            enemyTypeList.Add(1);
        }
        for (int i = 0; i < flyCount + (int)(enemyIncrementFactor * currentWave / 5); i++)
        {
            enemyTypeList.Add(2);
        }
        RandomizeEnemyList();
        enemyCount = enemyTypeList.Count;
        totalEnemyCount = enemyTypeList.Count;
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
