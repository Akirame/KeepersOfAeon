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
    private int enemyConta;
    private float timeBetweenWaves;
    private float timeBetweenEnemies;
    private float timer;
    private bool nextWave;    

    private void Start()
    {
        Enemy.Death += EnemyKilled;
        nextWave = true;
        currentWave = 1;
        wave = new GameObject();
        timeBetweenWaves = 5;
        timeBetweenEnemies = 1.5f;
        enemyConta = 0;
        timer = 0;
        currentEnemies = 0;        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Enemy e = EnemyWaves[currentWave - 1].enemies[Random.Range(0,EnemyWaves[currentWave-1].enemies.Count)];
            Transform t = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(e.transform.gameObject, t.position, Quaternion.identity, wave.transform);
        }
        if (currentWave <= EnemyWaves.Count)
        {
            if (nextWave)
            {
                SpawnWave();
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
        if ((enemyConta < EnemyWaves[currentWave - 1].enemies.Count))
        {
            if (timer >= timeBetweenEnemies)
            {
                Enemy e = EnemyWaves[currentWave - 1].enemies[enemyConta];
                Transform t = spawnPoints[Random.Range(0, spawnPoints.Length)];
                Instantiate(e.transform.gameObject, t.position, Quaternion.identity, wave.transform);
                enemyConta++;
                timer = 0;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
        else
        {
            currentEnemies = enemyConta;
            nextWave = false;
            enemyConta = 0;
            timer = 0;
        }

    }
    private void EnemyKilled(Enemy e)
    {
        Destroy(e.gameObject);
        currentEnemies--;
    }
    private void OnDestroy()
    {
        Enemy.Death -= EnemyKilled;
    }
}
