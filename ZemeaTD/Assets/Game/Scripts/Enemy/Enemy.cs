    using System.Collections;
using System.Collections.Generic;
    using System.Security;
    using UnityEngine;

public class Enemy : MonoBehaviour
{
    public delegate void EnemyActions(Enemy e);
    public static EnemyActions Hitted;
    public EnemyMovementBehaviour movementBehaviour;
    public int damage;
    protected Rampart rampart;
    protected bool syncroAttackWithAnim;

    protected virtual void Start()
    {        
        movementBehaviour = GetComponent<EnemyMovementBehaviour>();
        syncroAttackWithAnim = false;
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(gameObject.tag == "Enemy")
        if (collision.gameObject.tag == "Tower")
        {            
            Hitted(this);
        }
        if (collision.gameObject.tag == "Bullet")
            Hitted(this);
    }

    public bool AttackAnimEnd()
    {
        syncroAttackWithAnim = true;
    }
}
