using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {
    public float speed = 50f;
    public GameObject bulletParticles;
    public Transform posParticles;
    private int damage = 10;
    private Vector2 direction;
    private int deathTime = 25;
    private float timer;
    private Rigidbody2D rigid;

    private bool isAttached = false;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update () {
        timer += Time.deltaTime;
        if (timer >= deathTime)
        {
            Destroy(gameObject);
        }
        if (!isAttached)
        {
            transform.right = rigid.velocity;
        }
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void Shoot(Vector2 dir, int dmg)
    {
        direction = dir;
        damage = dmg;
        rigid.velocity = direction * speed;
    }

    public void SetDamage(int _damage)
    {
        damage = _damage;
    }
    public int GetDamage()
    {
        return damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Rampart")
        {
            GameObject bp = Instantiate(bulletParticles, posParticles.position, Quaternion.identity, transform.parent);
            if (direction.x < 0)
            {
                bp.transform.localScale = new Vector3(-1, 1, 1);
            }
            Destroy(this.gameObject);
        }
        if (collision.tag == "Tower")
        {
            collision.GetComponent<Tower>().TakeDamage(damage);
            rigid.velocity = new Vector2();
            rigid.gravityScale = 0;
            isAttached = true;
        }
    }    
}
