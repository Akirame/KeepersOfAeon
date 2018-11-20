using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIGame : MonoBehaviour
{    
    public Text waveText;
    public Text enemyCountText;
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

    private void UpdateText()
    {
        waveText.text = "Wave " + currentWave.ToString();
    }
    public void HordeIncomingTrigger(WaveControl wc)
    {
        anim.SetTrigger("horde");
    }
}
