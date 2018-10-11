using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : Enemy
{
    public float timeToAttack;
    private float timer;
    private bool isAttacking;

    protected override void Start()
    {
        base.Start();
        timer = 0;
    }
    private void Update()
    {
        if (rampart)
            if (rampart.IsAlive())
            {
                timer += Time.deltaTime;
                if (timer >= timeToAttack)
                {
                    rampart.Attacked(damage);
                    timer = 0;
                }
            }
            else
            {
                rampart = null;
                movementBehaviour.SetCanMove(true);
            }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.gameObject.tag == "Rampart")
        {
            rampart = collision.GetComponent<Rampart>();
            movementBehaviour.SetCanMove(false);
        }
    }
}
