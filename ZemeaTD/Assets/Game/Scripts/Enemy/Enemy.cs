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
    public int experience = 50;
    protected Rampart rampart;
    protected bool syncroAttackWithAnim;
    

    protected virtual void Start()
    {        
        movementBehaviour = GetComponent<EnemyMovementBehaviour>();
        syncroAttackWithAnim = false;
        element = (ElementalOrb.ELEMENT_TYPE)UnityEngine.Random.Range(0,3);
        switch (element)
        {
            case ElementalOrb.ELEMENT_TYPE.WATER:
                GetComponent<SpriteRenderer>().color = Color.blue;
                break;
            case ElementalOrb.ELEMENT_TYPE.FIRE:
                GetComponent<SpriteRenderer>().color = Color.red;
                break;
            case ElementalOrb.ELEMENT_TYPE.EARTH:
                GetComponent<SpriteRenderer>().color = Color.green;
                break;
        }
        tag = "Enemy";
    }

    private void Update()
    {
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Tower")
        {
            Death(this);
            collision.GetComponent<TowerTest>().TakeDamage(damage);
        }
    }

    public void AttackAnimEnd()
    {
        syncroAttackWithAnim = true;
    }

    public void TakeDamage(int bulletDamage, ElementalOrb playerElement, GameObject player)
    {        
        health -= bulletDamage;
        movementBehaviour.KnockBack(50);
        if (health <= 0)
        {
            PlayerLevel playerLevel = player.GetComponent<PlayerLevel>();
            playerLevel.AddExperience(experience);
            Death(this);
        }
    }
}
