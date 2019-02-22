using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevel : MonoBehaviour
{
    public delegate void PlayerLevelAnimation(PlayerLevel pl);
    public static PlayerLevelAnimation OnLevelUp;
    public int playerLevel = 1;
    public int playerExperience = 0;
    public int expNeededPerLevel = 300;
    public ParticleSystem particlesLevelUp;
    public Animator levelUpAnim;
    //public Text levelUpText;
    public Text levelVisor;
    public Canvas levelCanvas;
    public bool upgradeLevel = false;

    private void Start()
    {
       // levelUpText.gameObject.SetActive(false);
        levelVisor.text = playerLevel.ToString();
    }


    public void LevelUpPlayer()
    {
        upgradeLevel = true;
        OnLevelUp(this);
        playerLevel++;
        levelVisor.text = playerLevel.ToString();
        GetComponent<CharacterController2D>().playerData.LevelUp();
        levelUpAnim.SetTrigger("levelUp");
        //levelUpText.gameObject.SetActive(true);
        //levelUpText.GetComponent<Animator>().Play(0);
        LevelUpTo(playerLevel);
    }

    private void SetFlip()
    {
        float xScale = Mathf.Abs(levelCanvas.transform.localScale.x);
        bool facingRight = gameObject.GetComponent<CharacterController2D>().lookingRight;
        if(facingRight)
        {
            levelCanvas.transform.localScale = new Vector2(xScale, levelCanvas.transform.localScale.y);
        }
        else
        {
            levelCanvas.transform.localScale = new Vector2(-xScale, levelCanvas.transform.localScale.y);
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
            AddExperience(20);
        SetFlip();
    }

    public void LevelDownPlayer()
    {
        if (playerLevel > 1)
        {
            playerLevel--;
            LevelUpTo(playerLevel);
        }
    }

    public void AddExperience(int amount)
    {
        playerExperience += amount;
        while (playerExperience >= playerLevel * expNeededPerLevel)
        {
            playerExperience -= playerLevel * expNeededPerLevel;
            LevelUpPlayer();
        }
    }

    public void LevelUpTo(int level)
    {
        playerLevel = level;
        //OnLevelUp(this);
        particlesLevelUp.Play();
    }    
}
