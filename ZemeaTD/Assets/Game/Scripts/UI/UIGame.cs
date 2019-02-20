using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIGame : MonoBehaviour
{


    #region Singleton
    public static UIGame instance;
    public bool winGame = false;

    public static UIGame Get()
    {
        return instance;
    }
    public virtual void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    public Text waveText;
    public Text enemyCountText;
    public GameObject rightWarning;
    public GameObject leftWarning;
    private int currentEnemies;
    private int currentWave;
    private string waveTextAux;
    private WaveControl wave;
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        wave = WaveControl.GetInstance();
        WaveControl.HordeIncoming += HordeIncomingTrigger;
        currentWave = wave.currentWave;
        waveText.text = "NEW GAME";
        DisableWarnings();
    }

    internal void DisableWarnings()
    {
        rightWarning.SetActive(false);
        leftWarning.SetActive(false);
    }

    private void Update()
    {
        if (currentWave != wave.currentWave)
        {
            currentWave = wave.currentWave;
            GetComponent<Animator>().SetTrigger("wave");
            enemyCountText.text = wave.enemyCount.ToString() + "/" + wave.totalEnemyCount.ToString();
        }
        if (currentEnemies != wave.enemyCount)
        {
            currentEnemies = wave.enemyCount;
            enemyCountText.text = currentEnemies.ToString() + "/" + wave.totalEnemyCount.ToString();
            if (currentEnemies <= 0)
            {
                enemyCountText.text = "";
            }
        }
    }

    public void EnableWarning(Vector3 position)
    {
        if (position.x < 0)
        {
            rightWarning.SetActive(true);
        }
        else
        {
            leftWarning.SetActive(true);
        }
    }

    private void UpdateText()
    {
        waveText.text = "Wave " + currentWave.ToString();
    }
    public void HordeIncomingTrigger(WaveControl wc)
    {
        anim.SetTrigger("horde");
    }
    private void OnDestroy()
    {
        WaveControl.HordeIncoming -= HordeIncomingTrigger;
    }
}
