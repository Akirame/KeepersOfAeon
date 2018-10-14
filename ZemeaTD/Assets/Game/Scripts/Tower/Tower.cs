using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Tower : MonoBehaviour
{
    public delegate void TowerActions(Tower t);
    public static TowerActions TowerDestroyed;

    public int health = 100;
    public int maxHealth = 100;
    public Text healthText;
    public Image healthBar;
    public Rampart rampart;
    private bool canBeDestroyed = true;

    private void Start()
    {
        if (healthText)
        {
            healthText.text = health.ToString() + "/" + maxHealth.ToString();
        }
        if (DebugScreen.GetInstance())
        {
            DebugScreen.GetInstance().AddButton("TowerInvulnerable", TowerInvulnerable);
            DebugScreen.GetInstance().AddButton("RepairTower", RepairTower);
            DebugScreen.GetInstance().AddButton("RepairRamparts", RepairRamparts);
        }
    }

    private void Update()
    {

            if (health <= 0)
                TowerDestroyed(this);
        if (Input.GetKeyDown(KeyCode.F2))
        {
            TakeDamage(2);
        }
    }

    public void TakeDamage(int damage)
    {
        if(canBeDestroyed)
        health -= damage;
        if (healthText)
        {
            healthText.text = health.ToString() + "/" + maxHealth.ToString();
            healthBar.fillAmount = (float)health / maxHealth;
        }
    }

    private void RepairRamparts()
    {
            rampart.RepairAll();
    }

    private void RepairTower()
    {
        health = 100;
    }

    private void TowerInvulnerable()
    {
        canBeDestroyed = !canBeDestroyed;
    }
}
