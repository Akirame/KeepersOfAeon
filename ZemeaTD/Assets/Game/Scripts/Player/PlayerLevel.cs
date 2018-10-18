using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevel : MonoBehaviour
{

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
        playerLevel--;
        LevelUpTo(playerLevel);
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
        particlesLevelUp.Play();
        GetComponent<CharacterController2D>().playerData.LevelUp();
    }
}
