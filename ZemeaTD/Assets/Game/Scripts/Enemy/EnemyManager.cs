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

    private void Start() {
        Enemy.Death += EnemyKilled;
        nextWave = true;
        currentWave = 1;
        wave = new GameObject();
        enemyConta = 0;
        timer = 0;
        currentEnemies = 0;
        waveCalculated = false;
        enemiesIndex = new List<int>();
        enemies = new List<Enemy>();
        enemyMultiplier = 0;
    }
    private void Update() {
        if(nextWave) {
            SpawnWave();
        }
        if(currentEnemies <= 0) {
            if(timer >= timeBetweenWaves) {                
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
        if((enemyConta < totalEnemiesToSpawn)) {
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
        print(currentEnemies);
    }
    private void OnDestroy()
    {
        Enemy.Death -= EnemyKilled;
    }
}
