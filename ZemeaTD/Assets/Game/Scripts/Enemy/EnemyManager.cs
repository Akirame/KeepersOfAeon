using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
    #region singleton
    private static EnemyManager instance;
    public static EnemyManager GetInstance() {
        return instance;
    }
    private void Awake() {
        if(!instance)
            instance = this;
        else
            Destroy(this.gameObject);
    }
    #endregion

    public Transform[] spawnPoints;
    public GameObject wave;
    public int minCantMelee = 5;
    public int minCantRanged = 1;
    public int minCantFlyer = 1;
    public List<Enemy> TypeOfEnemies;
    public float timeBetweenWaves;
    public float timeBetweenEnemies;
    public float timeForFirstWave = 75;
    public GameObject miniBoss;
    public int waveMiniBoss = 1;
    private List<int> enemiesIndex;
    private List<Enemy> enemies;
    private int currentWave;
    private int enemyMultiplier;
    private int currentEnemies;
    private int enemyConta;
    private int totalEnemiesToSpawn;
    private float timer;
    private bool nextWave;
    private bool waveCalculated;
    private bool firstWave;
    private bool bossSpawn = false;

    private void Start() {
        Enemy.Death += EnemyKilled;
        currentWave = 1;
        wave = new GameObject();
        enemyConta = 0;
        timer = 0;
        currentEnemies = 0;
        waveCalculated = false;
        enemiesIndex = new List<int>();
        enemies = new List<Enemy>();
        enemyMultiplier = 0;
        firstWave = true;
    }
    private void Update() {
        if (Input.GetKeyDown(KeyCode.F1))
            KillAllEnemies();
        if (firstWave)
        {
            timer += Time.deltaTime;
            if (timer >= timeForFirstWave)
            {
                nextWave = true;
                timer = 0;
                firstWave = false;
            }
        }
        else
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
                    CleanWave();
                    timer = 0;
                    print(1);
                }
                else
                    timer += Time.deltaTime;
            }
        }

    }
    private void CalculateWave() {
        for(int i = 0; i < minCantMelee + enemyMultiplier; i++) {
            enemiesIndex.Add(0);
        }
        for(int i = 0; i < minCantRanged + (int)enemyMultiplier / 3; i++) {
            enemiesIndex.Add(1);
        }
        for(int i = 0; i < minCantFlyer + (int)enemyMultiplier / 5; i++) {
            enemiesIndex.Add(2);
        }
        enemyConta = enemiesIndex.Count;
        for(int i = 0; i < enemyConta; i++) {
            int randomIndexNumber = Random.Range(0, enemiesIndex.Count);
            enemies.Add(TypeOfEnemies[enemiesIndex[randomIndexNumber]]);
            enemiesIndex.RemoveAt(randomIndexNumber);
        }
        totalEnemiesToSpawn = enemies.Count;
        waveCalculated = true;
        enemyConta = 0;
        enemyMultiplier++;

    }
    private void SpawnWave() {
        wave.name = "Wave " + currentWave;
        if(!waveCalculated)
            CalculateWave();
        if (currentWave % waveMiniBoss == 0 && !bossSpawn)
        {
            bossSpawn = true;
            Transform t = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(miniBoss, t.position + new Vector3(0, miniBoss.transform.position.y, 0), Quaternion.identity, wave.transform);
            currentEnemies++;
            enemyConta++;
        }
        if ((enemyConta < totalEnemiesToSpawn)) {
            if(timer >= timeBetweenEnemies) {
                Enemy e = enemies[enemyConta];
                Transform t = spawnPoints[Random.Range(0, spawnPoints.Length)];
                Instantiate(e.transform.gameObject, t.position, Quaternion.identity, wave.transform);
                currentEnemies++;
                enemyConta++;
                timer = 0;
            }
            else {                
                timer += Time.deltaTime;
            }
        }
        else {            
            nextWave = false;
            waveCalculated = false;
            enemyConta = 0;
            timer = 0;
            bossSpawn = false;
        }

    }
    private void CleanWave() {
        enemies.Clear();        
        enemiesIndex.Clear();        
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
    public void KillAllEnemies()
    {
        Enemy[] enemyList = wave.GetComponentsInChildren<Enemy>();
        foreach (Enemy e in enemyList)
        {
            e.Kill();
        }
        CleanWave();
    }
}
