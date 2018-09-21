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
        if (waveTextAux != EnemyManager.GetInstance().wave.name) //delegate
        {
            GetComponent<Animator>().SetTrigger("wave");
            waveTextAux = EnemyManager.GetInstance().wave.name;            
        }
        ChangeOrbImage();
    }
    
    private void ChangeOrbImage()//delegate
    {
        if (player1.currentElement)
        {
            orbPlayer1.color = new Color(1f, 1f, 1f, 1f);
            orbPlayer1.sprite = orbSprites[(int)player1.currentElement.elementType];
        }
        else
        {
            orbPlayer1.color = new Color(1f, 1f, 1f, 0f);            
        }
        if (player2.currentElement)
        {
            orbPlayer2.color = new Color(1f, 1f, 1f, 1f);
            orbPlayer2.sprite = orbSprites[(int)player2.currentElement.elementType];
        }
        else
        {
            orbPlayer2.color = new Color(1f, 1f, 1f, 0f);            
        }
    }

    private void UpdateText()
    {
        waveText.text = waveTextAux;
    }
}
