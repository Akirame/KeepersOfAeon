using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveControl : MonoBehaviour
{

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
    public delegate void WaveControlActions(WaveControl wc);
    public static WaveControlActions HordeIncoming;    
    public Enemy[] enemyPrefab;
    public Transform[] spawnPoints;
    public GameObject enemiesParent;
    public Text waveTimeText;

    public int meleeCount;
    public int rangeCount;
    public int flyCount;
    public int enemyCount;

    public int currentWave;
    public int timeBetweenWaves;
    public float timeBetweenEnemies;

    public GameObject miniBoss;
    public int waveMiniBoss;

    public int enemyIncrementFactor = 2;
    public int totalEnemyCount;
    public int timeForFirstWave = 30;
    public bool gameStarted = false;
    private List<GameObject> enemyList;
    private List<int> enemyTypeList;
    private float timerWaves;
    private float timerEnemies;
    private bool canSpawn = false;

    public float hordeTime = 10f;
    public float percentHordeSpawnRate = 5f;
    private List<int> hordeWave;

    public GameObject meteorite;

    private float chanceOfHardRound;
    public float initialChanceOfHardRound = 0.2f;

    private int currentSpawnPoint = 0;

    public AudioClip hordeSound;
    private AudioSource aSource;

    // Use this for initialization
    void Start()
    {
        enemyTypeList = new List<int>();
        hordeWave = new List<int>();
        enemyList = new List<GameObject>();
        Enemy.Death += EnemyKilled;
        if (DebugScreen.GetInstance())
        {
            DebugScreen.GetInstance().AddButton("Kill All Enemies", KillAllEnemies);
            DebugScreen.GetInstance().AddButton("NextWave", NextWave);
        }
        Item.RalenticeConsume += RalenticeEnemies;
        chanceOfHardRound = initialChanceOfHardRound;
        timerWaves = timeBetweenWaves;
<<<<<<< HEAD
        aSource = GetComponent<AudioSource>();        
=======
        aSource = GetComponent<AudioSource>();
        AudioManager.Get().AddSound(aSource);
>>>>>>> 015f40744196356496434bc252aaf453b2a0f39a
    }

    private void NextWave()
    {
        KillAllEnemies();
        timerWaves = 0;
    }

    private void OnDestroy()
    {
        Item.RalenticeConsume -= RalenticeEnemies;
    }
    // Update is called once per frame
    void Update()
    {
        CheckOnTutorial();
        if (gameStarted)
        {
            if (canSpawn)
            {
                timerWaves -= Time.deltaTime;
                waveTimeText.text = timerWaves.ToString("00");
                if (timerWaves <= 0)
                {
                    PrepareNewWave();
                    CheckIfHardRound();
                }
            }
            else
            {
                CheckEnemyCount();
                waveTimeText.text = "";
            }
        }
    }

    private void CheckIfHardRound()
    {
        float randomChance = UnityEngine.Random.Range(0f, 1f);
        if (randomChance < chanceOfHardRound)
        {
            int enemyToSpawn = UnityEngine.Random.Range(0, 2);
            switch (enemyToSpawn)
            {
                case 0:
                    TrySpawnHorde();
                    break;
                case 1:
                    TrySpawnMeteorite();
                    break;
                default:
                    break;
            }
        }
    }

    private void TrySpawnMeteorite()
    {
        Transform t = spawnPoints[currentSpawnPoint];
        NextSpawnPoint();
        Vector3 pos = t.position + new Vector3(0, meteorite.transform.position.y, 0);
        Instantiate(meteorite, pos, Quaternion.identity, enemiesParent.transform);
    }

    private void PrepareNewWave()
    {
        CleanWave();
        SpawnWave();
        TrySpawnMiniBoss();
        timerWaves = timeBetweenWaves;
    }

    private void CheckOnTutorial()
    {
        if (GameManager.Get().tutorialDone && !gameStarted)
        {
            gameStarted = true;
        }
    }

    private void TrySpawnHorde()
    {
        aSource.clip = hordeSound;
        aSource.PlayDelayed(2f);
        StartCoroutine(SpawnHorde());
    }

    private void TrySpawnMiniBoss()
    {
        if (currentWave % waveMiniBoss == 0)
        {
            Transform t = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
            GameObject enemy = Instantiate(miniBoss, t.position + new Vector3(0, 20, 0), Quaternion.identity, enemiesParent.transform);
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
        StopCoroutine(SpawnEnemyList());
        StartCoroutine(SpawnEnemyList());
        canSpawn = false;
        currentWave++;
    }

    IEnumerator SpawnHorde()
    {
        HordeIncoming(this);
        yield return new WaitForSeconds(2);
        GenerateHordeWave();
        percentHordeSpawnRate = 5f;
        Transform t = spawnPoints[currentSpawnPoint];
        NextSpawnPoint();
        for (int i = 0; i < hordeWave.Count; i++)
        {
            Vector3 offsetPosition = new Vector3();
            offsetPosition.y = UnityEngine.Random.Range(-3, 3);
            GameObject enemy = Instantiate(enemyPrefab[hordeWave[i]].gameObject, t.position + offsetPosition, Quaternion.identity, enemiesParent.transform);
            enemy.GetComponent<Enemy>().MultiplyHealth((int)currentWave / 5);
            enemyList.Add(enemy);
            yield return new WaitForSeconds(timeBetweenEnemies / 2);
        }
    }

    IEnumerator SpawnEnemyList()
    {
        for (int i = 0; i < enemyTypeList.Count; i++)
        {
            Transform t = spawnPoints[currentSpawnPoint];
            NextSpawnPoint();
            Vector3 offsetPosition = new Vector3();
            offsetPosition.y = UnityEngine.Random.Range(-3, 3);
            GameObject enemy = Instantiate(enemyPrefab[enemyTypeList[i]].gameObject, t.position + offsetPosition, Quaternion.identity, enemiesParent.transform);
            enemy.GetComponent<Enemy>().MultiplyHealth((int)currentWave / 5);
            enemyList.Add(enemy);
            yield return new WaitForSeconds(timeBetweenEnemies);
        }
    }

    private void NextSpawnPoint()
    {
        currentSpawnPoint++;
        currentSpawnPoint = currentSpawnPoint % spawnPoints.Length;
    }

    private void EnemyKilled(Enemy e)
    {
        enemyList.Remove(e.gameObject);
        Destroy(e.gameObject);
        enemyCount--;
    }

    private void GenerateEnemyList()
    {
        for (int i = 0; i < meleeCount + (int)(enemyIncrementFactor * currentWave * 3); i++)
        {
            enemyTypeList.Add(0);
        }
        for (int i = 0; i < rangeCount + (int)((enemyIncrementFactor * currentWave / 3) * 3); i++)
        {
            enemyTypeList.Add(1);
        }
        for (int i = 0; i < flyCount + (int)((enemyIncrementFactor * currentWave / 5) * 3); i++)
        {
            enemyTypeList.Add(2);
        }
        RandomizeEnemyList();
        enemyCount = enemyTypeList.Count;
        totalEnemyCount = enemyCount;
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
    }

    private void CleanWave()
    {
        enemyTypeList.Clear();
        enemyList.Clear();
        enemyCount = 0;
    }

    private void GenerateHordeWave()
    {
        hordeWave.Clear();
        for (int i = 0; i < meleeCount + (int)(enemyIncrementFactor * currentWave); i++)
        {
            hordeWave.Add(0);
        }
        for (int i = 0; i < rangeCount + (int)(enemyIncrementFactor * currentWave / 3); i++)
        {
            hordeWave.Add(1);
        }
        for (int i = 0; i < flyCount + (int)(enemyIncrementFactor * currentWave / 5); i++)
        {
            hordeWave.Add(2);
        }
        for (int i = 0; i < hordeWave.Count; i++)
        {
            int randomIndex = UnityEngine.Random.Range(i, hordeWave.Count);
            int aux = hordeWave[i];
            hordeWave[i] = hordeWave[randomIndex];
            hordeWave[randomIndex] = aux;
        }
        enemyCount += hordeWave.Count;
        totalEnemyCount = enemyCount;
    }
    private void RalenticeEnemies(Item it)
    {
        for (int i = 0; i < enemyList.Count; i++)
            enemyList[i].GetComponent<EnemyMovementBehaviour>().RalenticeMovement();
    }
}
