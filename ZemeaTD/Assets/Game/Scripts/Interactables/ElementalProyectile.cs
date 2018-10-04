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
    public GameObject player;
    public PopText popText;

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

    public void Shoot(Vector2 dir, int _damage, ElementalOrb _element, GameObject _player)
    {        
        direction = dir;
        damage = _damage;
        element = _element;
        player = _player;
        GetComponent<Rigidbody2D>().velocity = direction * speed;
        ChangeElementColor();
    }

    private void ChangeElementColor()
    {
        if (element)
        {
            spriteRenderer.color = element.c;
            main.startColor = new ParticleSystem.MinMaxGradient(element.c);
        }
    }

    public int GetDamage()
    {
        return damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            damage = CalculateElementalDamage(damage, player.GetComponent<AttackBehaviour>().currentElement, collision.GetComponent<Enemy>().element);
            collision.GetComponent<Enemy>().TakeDamage(damage, player);
            PopDamageText(damage);
            Destroy(gameObject);
        }
    }

    private void PopDamageText(int bulletDamage)
    {
        GameObject go = Instantiate(popText.gameObject, transform.position, Quaternion.identity, transform.parent);
        go.GetComponent<PopText>().CreateText(bulletDamage.ToString(), Color.black);
    }

    private int CalculateElementalDamage(int damage, ElementalOrb playerOrb, ElementalOrb.ELEMENT_TYPE enemyOrb)
    {
        float pureDamage = damage;
        if (enemyOrb != ElementalOrb.ELEMENT_TYPE.NONE)
        {
            if (playerOrb)
            {
                if (playerOrb.elementType == enemyOrb)
                {
                    pureDamage *= 0.5f;
                }
                else if (enemyOrb == ElementalOrb.ELEMENT_TYPE.FIRE && playerOrb.elementType == ElementalOrb.ELEMENT_TYPE.WATER ||
                         enemyOrb == ElementalOrb.ELEMENT_TYPE.WATER && playerOrb.elementType == ElementalOrb.ELEMENT_TYPE.EARTH ||
                         enemyOrb == ElementalOrb.ELEMENT_TYPE.EARTH && playerOrb.elementType == ElementalOrb.ELEMENT_TYPE.FIRE)
                {
                    pureDamage *= 2;
                }
                else if (enemyOrb == ElementalOrb.ELEMENT_TYPE.FIRE && playerOrb.elementType == ElementalOrb.ELEMENT_TYPE.EARTH ||
                         enemyOrb == ElementalOrb.ELEMENT_TYPE.WATER && playerOrb.elementType == ElementalOrb.ELEMENT_TYPE.FIRE ||
                         enemyOrb == ElementalOrb.ELEMENT_TYPE.EARTH && playerOrb.elementType == ElementalOrb.ELEMENT_TYPE.WATER)
                {
                    pureDamage *= 0.1f;
                }
            }
            else
            {
                pureDamage *= 0.2f;
            }
        }

        if (pureDamage < 1)
        {
            pureDamage = 1;
        }
        return (int)pureDamage;
    }

}
