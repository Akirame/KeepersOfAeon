using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : Enemy
{
    public float timeToAttack;
    public int damage;
    private float timer;
    private Rampart rampart;
    private bool isAttacking;
    public SpriteRenderer sprite;

    protected override void Start()
    {
        base.Start();
        timer = 0;
        sprite = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {        
        if (rampart)
        {
            sprite.color = Color.red;
            if (timer < timeToAttack)
            {
                timer += Time.deltaTime;
            }
            else
            {
                rampart.Attacked(damage);
                timer = 0;
            }
        }
        else
        {
            sprite.color = Color.white;
            timer = 0;
        }        
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.gameObject.tag == "Rampart")
        {
            rampart = collision.GetComponent<Rampart>();
            movementBehaviour.Deactivate();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Rampart")
        {
            rampart = null;
            movementBehaviour.enabled = true;
        }
    }
}
