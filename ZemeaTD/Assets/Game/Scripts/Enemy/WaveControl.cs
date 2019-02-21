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

    [Header("-Current wave-")]
    public int currentWave;

    [Header("-Enemy List-")]
    public Enemy[] enemyPrefab;
    private List<int> hordeWave;
    private List<GameObject> enemyList;
    private List<int> enemyTypeList;
    private GameObject enemiesParent;

    [Header("-Spawn Point List-")]
    public Transform[] spawnPoints;
    private int currentSpawnPoint = 0;

    [Header("-Enemy Count For Wave-")]
    public int meleeCount;
    public int rangeCount;
    public int flyCount;
    public int enemyCount;
    public int totalEnemyCount;
    public int enemyIncrementFactor = 2;

    [Header("-Wave Timers-")]
    public int timeBetweenWaves;
    public float timeBetweenEnemies;
    public int timeForFirstWave = 30;
    private float timerWaves;
    private float timerEnemies;

    [Header("-Boss Vars-")]
    public GameObject[] bossList;
    public int waveMiniBoss;

    [Header("-Hard Round Vars-")]
    public int initialWaveToStartHordes;
    public float percentHordeSpawnRate = 5f;
    public float initialChanceOfHardRound = 0.2f;
    private float chanceOfHardRound;

    [Header("-Misc Vars-")]
    public bool gameStarted = false;
    public Text waveTimeText;
    public AudioClip hordeSound;
    private bool canSpawn = false;
    private AudioSource aSource;


    // Use this for initialization
    void Start()
    {
        enemiesParent = GameObject.Find("Enemies");
        enemyTypeList = new List<int>();
        hordeWave = new List<int>();
        enemyList = new List<GameObject>();
        Enemy.Death += EnemyKilled;
        if (DebugScreen.GetInstance())
        {
            DebugScreen.GetInstance().AddButton("Kill All Enemies", KillAllEnemies);
            DebugScreen.GetInstance().AddButton("NextWave", NextWave);
        }
        chanceOfHardRound = initialChanceOfHardRound;
        timerWaves = timeBetweenWaves;
        aSource = GetComponent<AudioSource>();
        AudioManager.Get().AddSound(aSource);
    }

    private void NextWave()
    {
        KillAllEnemies();
        timerWaves = 0;
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
        if (currentWave >= initialWaveToStartHordes)
        {
            float randomChance = UnityEngine.Random.Range(0f, 1f);
            if (randomChance < chanceOfHardRound)
            {
                TrySpawnHorde();
            }
        }
    }

    private void PrepareNewWave()
    {
        CleanWave();
        SpawnWave();
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

    private void TrySpawnBoss()
    {
        if (currentWave % waveMiniBoss == 0 && currentWave != 0)
        {
            Transform t = spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length)];
            GameObject enemy = Instantiate(bossList[UnityEngine.Random.Range(0,bossList.Length)], t.position + new Vector3(0, 20, 0), Quaternion.identity, enemiesParent.transform);
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
        TrySpawnBoss();
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

    public void RalenticeEnemies()
    {
        for (int i = 0; i < enemyList.Count; i++)
            enemyList[i].GetComponent<EnemyMovementBehavior>().RalenticeMovement();
    }
}
