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
    public Text healthText;
    
    private void Start()
    {
        if (healthText)
        {
            healthText.text = health + "%";
        }
    }

    private void Update()
    {
        if (health <= 0)
            TowerDestroyed(this);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (healthText)
        {
            healthText.text = health + "%";
        }
    }
}
