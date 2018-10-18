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
    public ElementalOrb.ELEMENT_TYPE element;
    public int damage;
    public int health = 100;
    public int healthMultiplier = 1;
    public int chanceOfElement = 25;
    public int experience = 50;
    public Sprite[] sprites;
    protected Rampart rampart;
    protected bool syncroAttackWithAnim;


    protected virtual void Start()
    {
        movementBehaviour = GetComponent<EnemyMovementBehaviour>();
        syncroAttackWithAnim = false;
        element = ElementalOrb.ELEMENT_TYPE.NONE;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.sprite = sprites[0];
        if (UnityEngine.Random.Range(0,100)<chanceOfElement)
        {
            element = (ElementalOrb.ELEMENT_TYPE)UnityEngine.Random.Range(0, 3);            
            if (sr)
            {
                switch (element)
                {
                    case ElementalOrb.ELEMENT_TYPE.WATER:
                        sr.sprite = sprites[1];
                        break;
                    case ElementalOrb.ELEMENT_TYPE.FIRE:
                        sr.sprite = sprites[2];
                        break;
                    case ElementalOrb.ELEMENT_TYPE.EARTH:
                        sr.sprite = sprites[3];
                        break;
                }

            }

        }
        tag = "Enemy";
    }


    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Tower")
        {
            Death(this);
            collision.GetComponent<Tower>().TakeDamage(damage);
        }
    }

    public void MultiplyHealth(int multiplier)
    {
        if (multiplier > 0)
        {
            health *= multiplier;            
        }
    }

    public void AttackAnimEnd()
    {
        syncroAttackWithAnim = true;
    }

    public void TakeDamage(int bulletDamage, GameObject player)
    {        
        health -= bulletDamage;
        movementBehaviour.KnockBack();
        if (health <= 0)
        {
            if (player)
            {
                PlayerLevel playerLevel = player.GetComponent<PlayerLevel>();
                playerLevel.AddExperience(experience);
            }
            Death(this);
        }
    }    
    public void Kill()
    {
        Death(this);
    }
}
