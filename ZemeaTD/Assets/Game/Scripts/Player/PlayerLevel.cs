using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevel : MonoBehaviour
{

    public int playerLevel = 1;
    public int playerExperience = 0;
    public int expNeededPerLevel = 300;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            playerLevel++;
            print("DEBUG - LEVEL UP - PlayerLevel.cs");
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            AddExperience(500);
            print("DEBUG - ADD EXPERIENCE - PlayerLevel.cs");
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
    }
}
