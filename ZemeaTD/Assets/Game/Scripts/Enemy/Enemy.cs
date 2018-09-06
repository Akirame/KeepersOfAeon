using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public delegate void EnemyActions(Enemy e);
    public static EnemyActions Hitted;
    public EnemyMovementBehaviour movementBehaviour;
    public int damage;
    protected Rampart rampart;

    protected virtual void Start()
    {        
        movementBehaviour = GetComponent<EnemyMovementBehaviour>();        
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Tower")
        {
            Hitted(this);
        }
        if (collision.gameObject.tag == "Bullet")
            Hitted(this);
    }        
}
