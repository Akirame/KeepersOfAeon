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
    public ParticleSystem[] shieldParticles;
    private CircleCollider2D coll;
    private bool activateCollision = false;

	void Start ()
    {        
        coll = GetComponent<CircleCollider2D>();
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F3))
        {            
            Attacked(50);
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
            if (activateCollision)
            {
                coll.enabled = true;
                activateCollision = !activateCollision;
                foreach (ParticleSystem p in shieldParticles)
                {
                    p.Play(true);
                }
            }
        }
        else {
            if (!activateCollision)
            {
                coll.enabled = false;
                activateCollision = !activateCollision;
                foreach(ParticleSystem p in shieldParticles) {
                    p.Stop(true);
                }
            }
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
        CheckAlive();
    }

    public void RepairRampart() {
        if(shield < maxShield) {
            shield += healthPerSecond * Time.deltaTime;
            CheckAlive();
        }
    }
}
