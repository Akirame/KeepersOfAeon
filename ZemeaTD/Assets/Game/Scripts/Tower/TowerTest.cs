using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TowerTest : MonoBehaviour
{
    public int health = 100;
    public Text healthText;

    private void Start()
    {
        healthText.text = health + "%";
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthText.text = health + "%";
    }
}
