using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalProyectile : MonoBehaviour
{    
    public ColorAttribute.COLOR_TYPE colorType;
    public float speed = 100f;
    private Vector2 direction;
    public int damage = 10;    
    public SpriteRenderer sr;
    public GameObject player;
    public PopText popText;
    public Sprite[] sprites;
    private Rigidbody2D rigid;
    private ParticleSystem.MainModule main;
    private bool onGround = false;
    private ParticleSystem ps;
    private float groundTimer;
    private Animator anim;
    private float lifeTimer;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        ps = GetComponentInChildren<ParticleSystem>();
        main = ps.main;
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();        
    }
    private void Start()
    {
        lifeTimer = UnityEngine.Random.Range(30.0f, 100.0f);        
    }

    private void UpdateColor()
    {
        Color c = Color.black;
        switch (colorType)
        {
            case ColorAttribute.COLOR_TYPE.GREEN:
                sr.sprite = sprites[0];
                c = Color.green;
                break;
            case ColorAttribute.COLOR_TYPE.MAGENTA:
                sr.sprite = sprites[1];
                c = Color.magenta;
                break;
            case ColorAttribute.COLOR_TYPE.ORANGE:
                sr.sprite = sprites[2];
                c = Color.red;
                break;
            case ColorAttribute.COLOR_TYPE.YELLOW:
                sr.sprite = sprites[3];
                c = Color.yellow;
                break;
            case ColorAttribute.COLOR_TYPE.LAST:
                break;
            default:
                break;
        }
        main.startColor = c;
    }

    private void Update()
    {
        if(!onGround)
        {
            transform.right = rigid.velocity;
        }
        else
        {
            groundTimer += Time.deltaTime;
            if (rigid.velocity.y != 0)
            {                
                if (groundTimer > UnityEngine.Random.Range(0, 0.15f))
                {
                    rigid.velocity = new Vector2();
                    rigid.gravityScale = 0;
                    ps.gameObject.SetActive(false);
                }                                
            }
            if(groundTimer >= lifeTimer)
            {
                anim.SetTrigger("Disappear");
            }
        }
    }

    public void Shoot(Vector2 dir, int _damage, ColorAttribute.COLOR_TYPE _element, GameObject _player)
    {
        direction = dir;
        damage = _damage;
        colorType = _element;
        player = _player;
        rigid.velocity = direction * speed;
        UpdateColor();
    }

    public int GetDamage()
    {
        return damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy" && !onGround)
        {
            damage = CalculateElementalDamage(damage, colorType, collision.GetComponent<Enemy>().element);
            collision.GetComponent<Enemy>().TakeDamage(damage, player);
            PopDamageText(damage);
            Destroy(gameObject);
        }
        if(collision.tag == "Balloon" && !onGround)
        {            
            collision.GetComponent<Balloon>().TakeDamage(colorType);
            Destroy(gameObject);
        }
        if(collision.tag == "Ground")
        {
            onGround = true;
        }
    }

    private void PopDamageText(int bulletDamage)
    {
        GameObject go = Instantiate(popText.gameObject, transform.position, Quaternion.identity, transform.parent);
        go.GetComponent<PopText>().CreateText(bulletDamage.ToString(), Color.black);
    }

    private int CalculateElementalDamage(int damage, ColorAttribute.COLOR_TYPE playerOrb, ColorAttribute.COLOR_TYPE enemyOrb)
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

    private void OnBecameInvisible()
    {
        DestroyProyectile();
    }
    public void DestroyProyectile()
    {
        Destroy(gameObject);
    }
}
