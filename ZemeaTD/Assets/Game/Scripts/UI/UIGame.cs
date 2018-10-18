using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIGame : MonoBehaviour
{    
    public Text waveText;
    public Text P1Level;
    public Text P2Level;
    public Image orbPlayer1;
    public Image orbPlayer2;
    public Image expBarP1;
    public Image expBarP2;
    public AttackBehaviour player1;
    public AttackBehaviour player2;
    public Sprite[] orbSprites;     
    private string waveTextAux;
    private PlayerLevel p1;
    private PlayerLevel p2;
    private int p1Level;
    private int p2Level;
    private int p1Exp;
    private int p2Exp;
    private int wave;
    private Animator anim;
    private bool updateLevels = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
        p1 = player1.gameObject.GetComponent<PlayerLevel>();
        p2 = player2.gameObject.GetComponent<PlayerLevel>();
        p1.GetComponent<PlayerLevel>().OnLevelUp += LevelUpPlayer1AnimationUI;
        p2.GetComponent<PlayerLevel>().OnLevelUp += LevelUpPlayer2AnimationUI;
        p1Level = p1.playerLevel;
        p2Level = p2.playerLevel;        
        p1Exp = p1.playerExperience;
        p2Exp = p2.playerExperience;
        P1Level.text = p1.playerLevel.ToString();
        P2Level.text = p2.playerLevel.ToString();
        expBarP1.fillAmount = p1.playerExperience / (p1.expNeededPerLevel * p1.playerLevel);
        expBarP2.fillAmount = p2.playerExperience / (p2.expNeededPerLevel * p2.playerLevel);
        wave = WaveControl.GetInstance().currentWave;
        waveText.text = "NEW GAME";
    }

    private void Update()
    {
        if (wave != WaveControl.GetInstance().currentWave) //delegate
        {
            wave = WaveControl.GetInstance().currentWave;
            GetComponent<Animator>().SetTrigger("wave");
        }
        ChangeOrbImage();


        if (p1Exp != p1.playerExperience)
        {
            p1Exp = p1.playerExperience;
            expBarP1.fillAmount = (float)p1.playerExperience/ (p1.expNeededPerLevel * p1.playerLevel);
        }

        if (p2Exp != p2.playerExperience)
        {
            p2Exp = p2.playerExperience;
            expBarP2.fillAmount = (float)p2.playerExperience / (p2.expNeededPerLevel * p2.playerLevel);
        }

        if ((p1.playerLevel != p1Level || p2.playerLevel != p2Level )&& updateLevels)
        {            
            updateLevels = false;
            p1Level = p1.playerLevel;
            P1Level.text = p1.playerLevel.ToString();            
            p2Level = p2.playerLevel;
            P2Level.text = p2.playerLevel.ToString();
        }

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
        waveText.text = "Wave " + wave.ToString();
    }

    public void UpdateLevelsText()
    {
        updateLevels = true;
    }

    private void LevelUpPlayer1AnimationUI(PlayerLevel pl)
    {
        anim.SetTrigger("levelUp1");
    }
    private void LevelUpPlayer2AnimationUI(PlayerLevel pl)
    {
        anim.SetTrigger("levelUp2");
    }
}
