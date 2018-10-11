using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveControl : MonoBehaviour {

    public Enemy[] enemyList;
    public Transform[] spawnPoints;
    public int meleeCount;
    public int rangeCount;
    public int flyCount;
    public int currentWave;
    public int timeBetweenWaves;
    public int timeBetweenEnemies;
    public List<GameObject> enemies;
    private List<int> enemyTypeList;
    private float timerWaves;
    private float timerEnemies;
    private bool canSpawn = true;


    // Use this for initialization
    void Start () {
        enemyTypeList = new List<int>();
        enemies = new List<GameObject>();
        Enemy.Death += EnemyKilled;
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
                timerWaves = 0;
            }
        }
        else
            CheckEnemyCount();
	}

    private void CheckEnemyCount()
    {
        if (enemies.Count <= 0)
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
    }

    IEnumerator SpawnEnemyList()
    {
        for (int i = 0; i < enemyTypeList.Count; i++)
        {
            Transform t = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
            GameObject enemy = Instantiate(enemyList[enemyTypeList[i]].gameObject, t.position, Quaternion.identity, transform);
            enemies.Add(enemy);
            yield return new WaitForSeconds(timeBetweenEnemies);
        }
    }

    private void EnemyKilled(Enemy e)
    {
        enemies.Remove(e.gameObject);
        Destroy(e.gameObject);
    }

    private void GenerateEnemyList()
    {
        for (int i = 0; i < meleeCount; i++)
        {
            enemyTypeList.Add(0);
        }
        for (int i = 0; i < rangeCount; i++)
        {
            enemyTypeList.Add(1);
        }
        for (int i = 0; i < flyCount; i++)
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
        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].GetComponent<Enemy>().Kill();
        }
        canSpawn = true;
    }

    private void CleanWave()
    {
        enemyTypeList.Clear();
        enemies.Clear();
    }
}
