using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeBehavior : Enemy
{
    public float timeToAttack;
    public bool enableShake = false;  
    private void Update()
    {
        if (rampart)
            if (rampart.IsAlive())
            {
                if (canAttack)
                {
                    timer += Time.deltaTime;
                    if (timer >= timeToAttack)
                    {
                        rampart.Attacked(damage);
                        if (enableShake)
                        {
                           CameraShake.GetInstance().Shake(0.001f, 0.01f); 
                        }
                        timer = 0;
                    }
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
