using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerLevel : MonoBehaviour
{
    public delegate void PlayerLevelAnimation(PlayerLevel pl);
    public PlayerLevelAnimation OnLevelUp;
    public int playerLevel = 1;
    public int playerExperience = 0;
    public int expNeededPerLevel = 300;
    public ParticleSystem particlesLevelUp;        
    

    public void LevelUpPlayer()
    {
        playerLevel++;
        LevelUpTo(playerLevel);
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
            playerLevel++;
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
