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
        element = (ElementalOrb.ELEMENT_TYPE)Random.Range(0,3);
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
    }

    private void Update()
    {
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(gameObject.tag == "Enemy")
        if (collision.gameObject.tag == "Tower")
        {            
            Death(this);
        }                    
    }

    public void AttackAnimEnd()
    {
        syncroAttackWithAnim = true;        
    }

    public void TakeDamage(int bulletDamage, ElementalOrb playerElement)
    {        
        float totalDamage = bulletDamage;
        if (element == playerElement.elementType)
        {
            totalDamage *= 0.5f;
        }
        else if (element == ElementalOrb.ELEMENT_TYPE.FIRE && playerElement.elementType == ElementalOrb.ELEMENT_TYPE.WATER ||
                 element == ElementalOrb.ELEMENT_TYPE.WATER && playerElement.elementType == ElementalOrb.ELEMENT_TYPE.EARTH ||
                 element == ElementalOrb.ELEMENT_TYPE.EARTH && playerElement.elementType == ElementalOrb.ELEMENT_TYPE.FIRE)
        {            
            totalDamage *= 2;
        }
        else if (element == ElementalOrb.ELEMENT_TYPE.FIRE && playerElement.elementType == ElementalOrb.ELEMENT_TYPE.EARTH ||
                 element == ElementalOrb.ELEMENT_TYPE.WATER && playerElement.elementType == ElementalOrb.ELEMENT_TYPE.FIRE ||
                 element == ElementalOrb.ELEMENT_TYPE.EARTH && playerElement.elementType == ElementalOrb.ELEMENT_TYPE.WATER)
        {
            totalDamage *= 0.1f;
        }
        health -= (int)totalDamage;
        movementBehaviour.KnockBack(50);
        if (health <= 0)
        {
            PlayerLevel playerLevel = playerElement.gameObject.transform.parent.GetComponent<PlayerLevel>();
            playerLevel.AddExperience(experience);
            Death(this);
        }
    }
}
