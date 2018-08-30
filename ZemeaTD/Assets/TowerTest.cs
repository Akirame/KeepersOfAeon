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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            health -= 10;
            healthText.text = health + "%";
        }
    }
}
