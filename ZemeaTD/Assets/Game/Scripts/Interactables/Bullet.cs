using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    public enum TypeOf { Ally,Enemy };
    public TypeOf type;
    public float speed = 200f;
    public Vector2 direction;
    public int damage = 10;

    // Update is called once per frame
    void Update () {
        GetComponent<Rigidbody2D>().velocity = direction * speed * Time.deltaTime;        
	}
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void Shoot(Vector2 dir, Vector3 angleAttack)
    {
        direction = dir;
        transform.eulerAngles = angleAttack;
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
