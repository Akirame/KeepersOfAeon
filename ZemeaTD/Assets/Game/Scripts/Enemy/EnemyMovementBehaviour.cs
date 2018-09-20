using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementBehaviour : MonoBehaviour
{
    private Rigidbody2D rig;
    private Dir direction;
    private SpriteRenderer rend;
    public float speed = 200;
    public enum Dir { Left, Right }


    private void Start()
    {        
        rig = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        SetDirection();
    }
    private void Update()
    {
        {
            switch (direction)
            {
                case Dir.Left:
                    rig.velocity = new Vector2(-speed * Time.deltaTime, rig.velocity.y);
                    break;
                case Dir.Right:
                    rig.velocity = new Vector2(speed * Time.deltaTime, rig.velocity.y);
                    break;
            }
        }
    }
    public void SetDirection()
    {
        if (transform.position.x > 0)
        {
            direction = Dir.Left;
            rend.flipX = true;
        }
        else
        {
            direction = Dir.Right;
            rend.flipX = false;
        }
    }
    public void Deactivate()
    {
        rig.velocity = Vector2.zero;
        this.enabled = false;
    }
    public void KnockBack(float force)
    {
        if(direction == Dir.Right)
        rig.AddForce(Vector2.left * force, ForceMode2D.Impulse);
        else
            rig.AddForce(Vector2.right * force, ForceMode2D.Impulse);
    }
}
