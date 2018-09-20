using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalProyectile : MonoBehaviour {

    public ElementalOrb element;
    public float speed = 100f;
    private Vector2 direction;
    public int damage = 10;
    private float timer;
    public int lifeTime = 5;
    public SpriteRenderer spriteRenderer;
    private Rigidbody2D rigid;
    private ParticleSystem.MainModule main;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        main = GetComponentInChildren<ParticleSystem>().main;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= lifeTime)
        {
            Destroy(gameObject);
        }
        transform.right = rigid.velocity;
    }

    public void Shoot(Vector2 dir,int _damage, ElementalOrb _element)
    {
        direction = dir;
        damage = _damage;
        element = _element;
        GetComponent<Rigidbody2D>().velocity = direction * speed;
        ChangeElementColor();
    }

    private void ChangeElementColor()
    {
        //switch (element)
        //{
        //    case ElementalArcanum.ORBS.WATER:
        //        spriteRenderer.color = Color.blue;
        //        break;
        //    case ElementalArcanum.ORBS.FIRE:
        //        spriteRenderer.color = Color.red;
        //        break;
        //    case ElementalArcanum.ORBS.EARTH:
        //        spriteRenderer.color = Color.green;
        //        break;
        //    default:
        //        break;
        //}
        main.startColor = new ParticleSystem.MinMaxGradient(spriteRenderer.color);
    }

    public int GetDamage()
    {
        return damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }

}
