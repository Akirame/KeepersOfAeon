using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rampart : MonoBehaviour
{
    public float health;
    public float healthPerSecond;
    private SpriteRenderer rend;
    private BoxCollider2D coll;

	void Start ()
    {        
        rend = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
	}
    private void Update()
    {
  
    }
    public void Attacked(int damage)
    {
        health -= damage;        
        rend.color = Color.red;
        CheckAlive();
    }
    private bool IsAlive()
    {
        if (health > 0)
            return true;
        else
            return false;
    }
    private void CheckAlive() {
        if(IsAlive()) {
            coll.enabled = true;
            Color c = Color.white;
            rend.color = c;
        }
        else {
            coll.enabled = false;
            Color c = new Vector4(0, 0, 0, 0);
            rend.color = c;
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
    public void RepairRampart() {
        if(health < 100) {
            Debug.Log("xd");
            health += healthPerSecond * Time.deltaTime;
            CheckAlive();
        }
    }
}
