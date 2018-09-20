using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public Text waveText;
    public Image orbPlayer1;
    public Image orbPlayer2;
    public AttackBehaviour player1;
    public AttackBehaviour player2;
    public Sprite[] orbSprites; 
    private string waveTextAux;

    private void Update()
    {
        if (waveTextAux != EnemyManager.GetInstance().wave.name)
        {
            GetComponent<Animator>().SetTrigger("wave");
            waveTextAux = EnemyManager.GetInstance().wave.name;            
        }
        ChangeOrbImage();
    }

    private void ChangeOrbImage()
    {
        //orbPlayer1.sprite = orbSprites[(int)player1.currentElement];
        //orbPlayer2.sprite = orbSprites[(int)player2.currentElement];
    }

    private void UpdateText()
    {
        waveText.text = waveTextAux;
    }
}
