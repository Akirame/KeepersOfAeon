using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementBehaviour : MonoBehaviour
{
    public Vector2 velocity = new Vector2();
    public float speed = 200;
    public float knockbackTime = 0.2f;
    private bool isKnockback = false;
    private Rigidbody2D rig;
    private SpriteRenderer rend;
    private float timer;
    private int movementDirection;


    private void Start()
    {        
        rig = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        SetDirection();
    }

    private void FixedUpdate()
    {
        if (isKnockback)
        {
            velocity.x = speed/2 * -movementDirection * Time.deltaTime;
            timer += Time.deltaTime;
            if (timer > knockbackTime)
            {
                timer = 0;
                isKnockback = false;
            }
        }
        else
            velocity.x = speed * movementDirection * Time.deltaTime;
        rig.velocity = velocity;
    }


    public void SetDirection()
    {
        if (transform.position.x > 0)
        {
            rend.flipX = true;
            movementDirection = -1;
        }
        else
        {
            rend.flipX = false;
            movementDirection = 1;
        }
    }

    public void Deactivate()
    {
        rig.velocity = Vector2.zero;
        this.enabled = false;
    }

    public void KnockBack()
    {
        rig.AddForce(new Vector2(10 * -movementDirection, 0), ForceMode2D.Impulse);
        isKnockback = true;
    }
}
