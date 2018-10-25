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
    private SpriteRenderer rend;
    private CircleCollider2D coll;
    private bool activateRenderer = false;

	void Start ()
    {        
        rend = GetComponent<SpriteRenderer>();
        coll = GetComponent<CircleCollider2D>();
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F3))
        {            
            Attacked(50);
        }
        shieldBar.fillAmount = (float)shield / maxShield;
        rend.color = Color.Lerp(Color.red, Color.white, shieldBar.fillAmount);
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
            if (activateRenderer)
            {
                coll.enabled = true;
                rend.enabled = true;
                activateRenderer = !activateRenderer;
                foreach (ParticleSystem p in shieldParticles)
                {
                    p.Play(true);
                }
            }
        }
        else {
            if (!activateRenderer)
            {
                coll.enabled = false;                
                rend.enabled = false;
                activateRenderer = !activateRenderer;
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
