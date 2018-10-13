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
    private BoxCollider2D coll;

	void Start ()
    {        
        rend = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
	}
    public void Attacked(int damage)
    {
        shield -= damage;        
        rend.color = Color.red;
        CheckAlive();
    }
    public bool IsAlive()
    {
        return (shield > 0);
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
