using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorProyectile :Proyectile
{
    public ColorAttribute.COLOR_TYPE colorType;
    public Sprite[] sprites;
    private ParticleSystem.MainModule main;
    private ParticleSystem ps;
    private float groundTimer;
    private Animator anim;
    private bool critical;
    private bool penetrating;

    private void Start()
    {        
        ps = GetComponentInChildren<ParticleSystem>();        
        main = ps.main;
        anim = GetComponent<Animator>();
        lifeTimer = UnityEngine.Random.Range(30.0f, 100.0f);
        speed = 100f;
        damage = 10;        
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
            if(rigid.velocity.y != 0)
            {
                if(groundTimer > UnityEngine.Random.Range(0, 0.15f))
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

    public void Shoot(Vector2 dir, int _damage, ColorAttribute.COLOR_TYPE _element, GameObject _player, bool _critical, bool penetrate)
    {
        direction = dir;
        damage = _damage;
        colorType = _element;
        player = _player;
        rigid.velocity = direction * speed;
        critical = _critical;
        penetrating = penetrate;
        UpdateColor();
    }

    private void UpdateColor()
    {
        Color c = Color.black;
        switch(colorType)
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
        }        
        //main.startColor = c;
    }
    protected override void PopDamageText(int bulletDamage)
    {
        GameObject go = Instantiate(popText.gameObject, transform.position, Quaternion.identity, transform.parent);
        go.GetComponent<PopText>().CreateText(bulletDamage.ToString(), Color.black);
        if(critical)
        {
            go.GetComponent<PopText>().CreateCriticalText(bulletDamage.ToString());
        }
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy" && !onGround && collision.GetComponent<Enemy>().IsAlive())
        {
            damage = CalculateElementalDamage(damage, colorType, collision.GetComponent<Enemy>().color);
            collision.GetComponent<Enemy>().TakeDamage(damage, player);
            PopDamageText(damage);
            if(!penetrating)
                Destroy(gameObject);
        }
        if(collision.tag == "Balloon" && !onGround)
        {
            collision.GetComponent<Balloon>().TakeDamage(colorType, player);
            if(!penetrating)
                Destroy(gameObject);
        }
        if(collision.tag == "Ground")
        {
            onGround = true;
        }
    }


}
