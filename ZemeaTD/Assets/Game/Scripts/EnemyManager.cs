using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    #region singleton
    private static EnemyManager instance;
    public static EnemyManager GetInstance()
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

    [System.Serializable]
    public class EnemyList
    {
        public List<Enemy> enemies;
    }
    public List<EnemyList> EnemyWaves;
    public Transform[] spawnPoints;
    public GameObject wave;
    private int currentWave;
    private int currentEnemies;
    private float timeBetweenWaves;
    private float timer;
    private bool nextWave;

    private void Start()
    {
        Enemy.Hitted += EnemyKilled;
        nextWave = true;
        currentWave = 1;
        wave = new GameObject();
        timeBetweenWaves = 5;
        timer = 0;
        currentEnemies = 0;        
    }
    private void Update()
    {
        if (currentWave <= EnemyWaves.Count)
        {
            if (nextWave)
            {
                SpawnWave();
                nextWave = false;
            }
            if (currentEnemies <= 0)
            {
                if (timer >= timeBetweenWaves)
                {
                    currentWave++;
                    nextWave = true;
                    timer = 0;
                }
                else
                    timer += Time.deltaTime;
            }
        }                    
    }
    private void SpawnWave()
    {
        wave.name = "Wave " + currentWave;
        for (int i = 0; i < EnemyWaves[currentWave - 1].enemies.Count; i++)
        {
            Enemy e = EnemyWaves[currentWave - 1].enemies[i];
            Transform t = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(e.transform.gameObject, t.position, Quaternion.identity, wave.transform);                                    
        }
        currentEnemies = EnemyWaves[currentWave - 1].enemies.Count;
    }
    private void EnemyKilled(Enemy e)
    {
        Destroy(e.gameObject);
        currentEnemies--;
    }
}
