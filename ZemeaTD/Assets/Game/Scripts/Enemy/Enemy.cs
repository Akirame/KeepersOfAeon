using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public delegate void EnemyActions(Enemy e);
    public static EnemyActions Death;
    public EnemyMovementBehavior movementBehaviour;
    public ColorAttribute.COLOR_TYPE color;
    public int damage;
    public int health = 100;
    public float healthMultiplier = 1;
    public int experience = 50;
    public Sprite[] sprites;
    public GameObject deathParticles;
    public GameObject expParticles;
    public ParticleSystem hitParticles;
    protected bool canAttack = true;
    protected float timer;
    protected Rampart rampart;
    protected int orderingLayer;
    private SpriteRenderer sr;

    protected virtual void Start()
    {
        timer = 0; 
        movementBehaviour = GetComponent<EnemyMovementBehavior>();
        color = ColorAttribute.COLOR_TYPE.YELLOW;
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = sprites[3];
        color = (ColorAttribute.COLOR_TYPE)UnityEngine.Random.Range(0, 3);
        if (sr)
        {
            switch (color)
            {
                case ColorAttribute.COLOR_TYPE.GREEN:
                    sr.sprite = sprites[0];
                    break;
                case ColorAttribute.COLOR_TYPE.MAGENTA:
                    sr.sprite = sprites[1];
                    break;
                case ColorAttribute.COLOR_TYPE.ORANGE:
                    sr.sprite = sprites[2];
                    break;
            }
        }
        tag = "Enemy";
        orderingLayer = Mathf.RoundToInt(transform.position.y * 100f) * -1;
        sr.sortingOrder = orderingLayer;
    }

    private void Update()
    {
        CheckCanAttack();
    }

    private void CheckCanAttack()
    {
        if (!canAttack)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                canAttack = true;
                timer = 0;
            }
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Tower")
        {
            collision.GetComponent<Tower>().TakeDamage(damage);
            Instantiate(deathParticles, transform.position, Quaternion.identity, transform.parent);
            Death(this);
        }
    }

    public void MultiplyHealth(int multiplier)
    {
        if (multiplier > 0)
        {
            health *= multiplier;
        }
    }

    IEnumerator FlickerEffect()
    {
        for (int i = 0; i < 5; i++)
        {
            if (i % 2 == 0)
            {
                sr.material.SetFloat("_FlashAmount", 0.7f);
            }
            else
            {
                sr.material.SetFloat("_FlashAmount", 0f);
            }
            yield return new WaitForSeconds(0.07f);
        }
        sr.material.SetFloat("_FlashAmount", 0);
    }

    public void TakeDamage(int bulletDamage, GameObject player)
    {
        if (health > 0)
        {
            hitParticles.Play();
            StopCoroutine("FlickerEffect");
            StartCoroutine("FlickerEffect");
            health -= bulletDamage;
            movementBehaviour.KnockBack();
            if (health <= 0)
            {
                KillByPlayer(player);
            }
        }

    }

    public bool IsAlive()
    {
        return health > 0;
    }

    public void KillByPlayer(GameObject player)
    {
        PlayerLevel playerLevel = player.GetComponent<PlayerLevel>();
        playerLevel.AddExperience(experience);
        GameObject ep = Instantiate(expParticles, transform.position, Quaternion.identity, transform.parent);
        ep.GetComponent<ExpParticles>().target = player.transform;
        Kill();
    }

    public void Kill()
    {
        Instantiate(deathParticles, transform.position, Quaternion.identity, transform.parent);
        Death(this);
    }

    public void SetCanAttack(bool val, float time)
    {
        canAttack = val;
        timer = time;
    }

}
