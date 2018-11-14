using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLevel : MonoBehaviour
{
    public delegate void PlayerLevelAnimation(PlayerLevel pl);
    public PlayerLevelAnimation OnLevelUp;
    public int playerLevel = 1;
    public int playerExperience = 0;
    public int expNeededPerLevel = 300;
    public ParticleSystem particlesLevelUp;
    public Text levelUpText;
    public Text levelVisor;
    public Canvas levelCanvas;

    private void Start()
    {
        levelUpText.gameObject.SetActive(false);
        levelVisor.text = playerLevel.ToString();
    }


    public void LevelUpPlayer()
    {
        playerLevel++;
        levelVisor.text = playerLevel.ToString();
        GetComponent<CharacterController2D>().playerData.LevelUp();
        levelUpText.gameObject.SetActive(true);
        levelUpText.GetComponent<Animator>().Play(0);
        LevelUpTo(playerLevel);
    }

    private void SetFlip()
    {
        bool facingRight = gameObject.GetComponent<CharacterController2D>().lookingRight;                
        if(facingRight)
        {
            levelCanvas.transform.localScale = new Vector2(0.08f, 0.08f);
        }
        else
        {
            levelCanvas.transform.localScale = new Vector2(-0.08f, 0.08f);
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
        OnLevelUp(this);
        particlesLevelUp.Play();
    }    
}
