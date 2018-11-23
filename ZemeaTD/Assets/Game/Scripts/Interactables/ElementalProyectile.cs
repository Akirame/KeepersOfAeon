using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalProyectile : MonoBehaviour
{

    public ElementalOrb.ELEMENT_TYPE element;
    public float speed = 100f;
    private Vector2 direction;
    public int damage = 10;
    public int lifeTime = 100;
    public SpriteRenderer spriteRenderer;
    public GameObject player;
    public PopText popText;
    private Rigidbody2D rigid;
    private ParticleSystem.MainModule main;
    private bool onGround = false;
    private float timer;
    private ParticleSystem ps;


    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        ps = GetComponentInChildren<ParticleSystem>();
        main = ps.main;
    }

    private void Update()
    {
        if(!onGround)
        {
            transform.right = rigid.velocity;
        }
        timer += Time.deltaTime;
        if(timer >= lifeTime)
        {
            Destroy(gameObject);
        }
    }

    public void Shoot(Vector2 dir, int _damage, ElementalOrb.ELEMENT_TYPE _element, GameObject _player)
    {
        direction = dir;
        damage = _damage;
        element = _element;
        player = _player;
        rigid.velocity = direction * speed;
        ChangeElementColor();
    }

    private void ChangeElementColor()
    {
        Color c = Color.white;
        switch(element)
        {
            case ElementalOrb.ELEMENT_TYPE.EARTH:
                c = Color.green;
                break;
            case ElementalOrb.ELEMENT_TYPE.FIRE:
                c = Color.red;
                break;
            case ElementalOrb.ELEMENT_TYPE.WATER:
                c = Color.blue;
                break;
            case ElementalOrb.ELEMENT_TYPE.NONE:
                c = Color.white;
                break;
        }
        spriteRenderer.color = c;
        main.startColor = new ParticleSystem.MinMaxGradient(c);
    }

    public int GetDamage()
    {
        return damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy" && !onGround)
        {
            damage = CalculateElementalDamage(damage, element, collision.GetComponent<Enemy>().element);
            collision.GetComponent<Enemy>().TakeDamage(damage, player);
            PopDamageText(damage);
            Destroy(gameObject);
        }
        if(collision.tag == "Ground")
        {
            onGround = true;
            rigid.velocity = new Vector2();
            rigid.gravityScale = 0;
            ps.gameObject.SetActive(false);
        }
    }

    private void PopDamageText(int bulletDamage)
    {
        GameObject go = Instantiate(popText.gameObject, transform.position, Quaternion.identity, transform.parent);
        go.GetComponent<PopText>().CreateText(bulletDamage.ToString(), Color.black);
    }

    private int CalculateElementalDamage(int damage, ElementalOrb.ELEMENT_TYPE playerOrb, ElementalOrb.ELEMENT_TYPE enemyOrb)
    {
        float pureDamage = damage;        
        if(playerOrb == enemyOrb)
        {
            pureDamage *= 0.6f;
        }
        else
        {
            pureDamage *= 0.1f;
        }
        if(pureDamage < 1)
        {
            pureDamage = 1;
        }
        return (int)pureDamage;
    }

}
