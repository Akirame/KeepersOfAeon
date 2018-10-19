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
    private bool canMove = true;
    private Rigidbody2D rig;
    private float timer;
    private int movementDirection;
    private int knockbackForce = 2;


    private void Start()
    {        
        rig = GetComponent<Rigidbody2D>();
        SetDirection();
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            if (isKnockback)
            {
                velocity.x = speed / 2 * -movementDirection * Time.deltaTime;
                timer += Time.deltaTime;
                if (timer > knockbackTime)
                {
                    timer = 0;
                    isKnockback = false;
                }
            }
            else
                velocity.x = speed * movementDirection * Time.deltaTime;
            rig.velocity = new Vector2(velocity.x,rig.velocity.y);
        }
        else
            rig.velocity = Vector2.zero;

    }


    public void SetDirection()
    {
        if (transform.position.x > 0)
        {
            movementDirection = -1;
        }
        else
        {
            movementDirection = 1;
        }
        transform.localScale = new Vector3(movementDirection, 1, 1);
    }

    public void SetCanMove(bool val)
    {
        canMove = val;
    }

    public void KnockBack()
    {
        rig.AddForce(new Vector2(knockbackForce * -movementDirection, 0), ForceMode2D.Impulse);
        isKnockback = true;
    }
}
