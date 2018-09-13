using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : Enemy
{
    public float timeToAttack;    
    private float timer;    
    private bool isAttacking;
    private Animator anim;
    

    protected override void Start()
    {
        base.Start();
        timer = 0;        
        anim = GetComponent<Animator>();
    }
    private void Update()
    {        
        if (rampart)
        {            
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
            anim.SetBool("AttackOn",true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Rampart")
        {
            rampart = null;
            movementBehaviour.enabled = true;
            anim.SetBool("AttackOn",false);
        }
    }
}
