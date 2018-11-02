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
    public Text levelText;

    private void Start()
    {
        levelText.gameObject.SetActive(false);
    }

    public void LevelUpPlayer()
    {
        SetFlip();
        playerLevel++;
        LevelUpTo(playerLevel);
        levelText.gameObject.SetActive(true);
        levelText.GetComponent<Animator>().Play(0);
    }

    private void SetFlip()
    {
        bool facingRight = gameObject.GetComponent<CharacterController2D>().lookingRight;
        Vector3 newScale = Vector3.zero;
        if (!facingRight)
        {
            newScale = levelText.transform.localScale;
            newScale.x = levelText.transform.localScale.x * -1;
            levelText.transform.localScale = newScale;
        }
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
            AddExperience(20);
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
        GetComponent<CharacterController2D>().playerData.LevelUp();
    }    
}
