using System;
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
    public float invulnerableTime = 7;
    private CapsuleCollider2D coll;
    private Animator anim;
    private bool activateCollision = false;
    private bool canBeHurt = true;
    private float timer;

	void Start ()
    {        
        coll = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
        Item.InvulnerableConsume += ShieldInvulnerable;
	}

    private void OnDestroy()
    {
        Item.InvulnerableConsume -= ShieldInvulnerable;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F3))
        {            
            Attacked(50);
        }
        CheckCanBeHurt();
        UpdateShieldFill();
    }

    private void UpdateShieldFill()
    {
        shieldBar.fillAmount = (float)shield / maxShield;
    }

    private void CheckCanBeHurt()
    {
        if (!canBeHurt)
        {
            timer += Time.deltaTime;
            if (timer >= invulnerableTime)
            {
                timer = 0;
                canBeHurt = !canBeHurt;
                anim.SetBool("invulnerable", !canBeHurt);
            }
        }
    }

    public void Attacked(int damage)
    {
        if(canBeHurt)
        {
            shield -= damage;
            CheckAlive();
        }
    }

    public bool IsAlive()
    {
        return (shield > 0);
    }

    public void ShieldInvulnerable(Item i)
    {
        timer = 0;
        canBeHurt = false;
        anim.SetBool("invulnerable", !canBeHurt);
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
            if (!collision.GetComponent<EnemyRangedBehavior>())
            {
                Attacked(collision.GetComponent<Enemy>().damage);
            }
        }
        if (collision.gameObject.tag == "EnemyBullet")
        {
            Attacked(collision.GetComponent<EnemyBullet>().GetDamage());
        }
    }

    public void RepairAll()
    {
        shield = maxShield;
        CheckAlive();
    }

    public void RepairRampart(int multiplier) {
        if(shield < maxShield) {
            shield += healthPerSecond * Time.deltaTime * multiplier;
            CheckAlive();
        }
    }
}
