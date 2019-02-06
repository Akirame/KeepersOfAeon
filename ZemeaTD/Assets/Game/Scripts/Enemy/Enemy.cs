using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public delegate void EnemyActions(Enemy e);
    public static EnemyActions Death;
    public EnemyMovementBehaviour movementBehaviour;
    public ColorAttribute.COLOR_TYPE color;
    public int damage;
    public int health = 100;
    public int healthMultiplier = 1;
    public int experience = 50;
    public Sprite[] sprites;
    public GameObject deathParticles;
    public GameObject expParticles;
    protected Rampart rampart;
    protected int orderingLayer;
    private SpriteRenderer sr;
    private bool isAlive = true;

    protected virtual void Start()
    {
        movementBehaviour = GetComponent<EnemyMovementBehaviour>();
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
        movementBehaviour.Initialize(orderingLayer);
        GetComponent<SpriteRenderer>().sortingOrder = orderingLayer;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Tower")
        {
            Death(this);
            collision.GetComponent<Tower>().TakeDamage(damage);
            Instantiate(deathParticles, transform.position, Quaternion.identity, transform.parent);
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
        if (IsAlive())
        {
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
        Death(this);
        Instantiate(deathParticles, transform.position, Quaternion.identity, transform.parent);
    }

    public void Kill()
    {
        Death(this);
        Instantiate(deathParticles, transform.position, Quaternion.identity, transform.parent);
    }
}
