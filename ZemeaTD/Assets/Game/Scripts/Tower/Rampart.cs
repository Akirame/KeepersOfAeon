using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rampart : MonoBehaviour
{
    public float shield = 200;
    public int maxShield = 200;
    public float healthPerSecond;
    public Image shieldBar;
    private SpriteRenderer rend;
    private CircleCollider2D coll;

	void Start ()
    {        
        rend = GetComponent<SpriteRenderer>();
        coll = GetComponent<CircleCollider2D>();
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F3))
        {
            print("DEBUG - Rampart.cs - 10 DANIO SHIELD");
            shield -= 10;
        }
        shieldBar.fillAmount = (float)shield / maxShield;
    }

    public void Attacked(int damage)
    {
        shield -= damage;
        CheckAlive();
    }
    public bool IsAlive()
    {
        return (shield > 0);
    }
    private void CheckAlive() {
        if(IsAlive()) {
            coll.enabled = true;
        }
        else {
            coll.enabled = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (!collision.GetComponent<EnemyRanged>())
            {
                Attacked(collision.GetComponent<Enemy>().damage);                
            }
        }
        if (collision.gameObject.tag == "EnemyBullet")
        {
            Attacked(collision.GetComponent<Bullet>().GetDamage());            
        }
    }

    public void RepairAll()
    {
        shield = maxShield;
    }

    public void RepairRampart() {
        if(shield < maxShield) {
            shield += healthPerSecond * Time.deltaTime;
            CheckAlive();
        }
    }
}
