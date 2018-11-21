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
    public Image healthBackBar;
    public Rampart rampart;
    public float decreaseBackHealthBarFactor = 10;
    public float healthTiltThreshold;
    private bool canBeDestroyed = true;
    private Animator healthAnimator;

    private void Start()
    {
        healthAnimator = healthBar.GetComponent<Animator>();
        if (healthText)
        {
            if(health <= 0)
                health = 0;
            healthText.text = health.ToString() + "/" + maxHealth.ToString();
        }
        if (DebugScreen.GetInstance())
        {
            DebugScreen.GetInstance().AddButton("DestroyTower", DestroyTower);
            DebugScreen.GetInstance().AddButton("TowerInvulnerable", TowerInvulnerable);
            DebugScreen.GetInstance().AddButton("RepairTower", RepairTower);
            DebugScreen.GetInstance().AddButton("RepairRamparts", RepairRamparts);
        }
    }

    private void DestroyTower()
    {
        health = -1;
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
            CancelInvoke();
            healthText.text = health.ToString() + "/" + maxHealth.ToString();
            healthBar.fillAmount = (float)health / maxHealth;
            InvokeRepeating("LowBackHealth", 1.5f, 0.05f);
        }
        healthAnimator.SetFloat("Health", health);
    }

    private void LowBackHealth()
    {
        healthBackBar.fillAmount -= (healthBackBar.fillAmount - healthBar.fillAmount)/decreaseBackHealthBarFactor;
        if (healthBackBar.fillAmount <= healthBar.fillAmount)
        {
            healthBackBar.fillAmount = healthBar.fillAmount;
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
