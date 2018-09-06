using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public delegate void EnemyActions(Enemy e);
    public static EnemyActions Hitted;
    public enum Dir { LEFT,RIGHT}
    public float speed = 10;
    private Dir direction;

    private void Awake()
    {
        SetDirection();
    }
    private void Update()
    {
        switch (direction)
        {
            case Dir.LEFT:
                GetComponent<Rigidbody2D>().velocity = new Vector2(-speed * Time.deltaTime, 0);
                break;
            case Dir.RIGHT:
                GetComponent<Rigidbody2D>().velocity = new Vector2(speed * Time.deltaTime, 0);
                break;            
        }
    }
    public void SetDirection()
    {
        if (transform.position.x > 0)
            direction = Dir.LEFT;
        else 
            direction = Dir.RIGHT;        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Tower")
        {
            Hitted(this);
        }
        if (collision.gameObject.tag == "Bullet")
            Hitted(this);
    }    
}
