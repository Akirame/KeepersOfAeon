using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public enum TypeOf { Ally,Enemy };
    public TypeOf type;
    public float speed = 50f;
    public Vector2 direction;
    public int damage = 10;
    private int deathTime = 5;
    private float timer;

    // Update is called once per frame
    void Update () {
        timer += Time.deltaTime;
        if (timer >= deathTime)
        {
            Destroy(gameObject);
        }
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void Shoot(Vector2 dir, Vector3 angleAttack)
    {
        direction = dir;
        transform.eulerAngles = angleAttack;
        GetComponent<Rigidbody2D>().velocity = direction * speed;
    }

    public void Shoot(Vector2 dir)
    {
        direction = dir;
        GetComponent<Rigidbody2D>().velocity = direction * speed;
    }

    public void SetDamage(int _damage)
    {
        damage = _damage;
    }
    public int GetDamage()
    {
        return damage;
    }
    public void SetType(TypeOf _type)
    {
        type = _type;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (type)
        {
            case TypeOf.Ally:
                if (collision.gameObject.tag == "Enemy")
                    Destroy(this.gameObject);
                    break;
            case TypeOf.Enemy:
                if (collision.gameObject.tag == "Rampart")
                    Destroy(this.gameObject);
                break;
        }        
    }    
}
