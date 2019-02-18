using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HommingMissile : MonoBehaviour
{

    public float speed = 7;
    public float rotatingSpeed = 200;
    public GameObject target;    
    public ColorAttribute.COLOR_TYPE colorType;
    public int damage = 10;
    public PopText popText;
    public GameObject player;
    public Sprite[] sprites;
    public CircleCollider2D detector;
    public SpriteRenderer sr;
    public Rigidbody2D rigid;
    private bool shoot = false;
    private bool targetLockOn = false;
    private float lifeTimer = 3f;
    private float timer = 0;
    private Vector2 dir;

    void Start()
    {        
        rigid = GetComponent<Rigidbody2D>();
        sr = this.GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if(timer < lifeTimer)
            timer += Time.deltaTime;
        else
            Destroy(this.gameObject);
        if(!target.gameObject)
        {            
            targetLockOn = false;
            detector.enabled = true;
        }
    }    
    void FixedUpdate()
    {        
        if(shoot)
        {
            if(targetLockOn)
            {
                dir = (Vector2)transform.position - (Vector2)target.transform.position;
                dir.Normalize();
                float value = Vector3.Cross(dir, transform.right).z;
                rigid.angularVelocity = rotatingSpeed * value;
                rigid.velocity = transform.right * speed;
            }
            else
            {
                transform.right = rigid.velocity;
            }
        }
    }

    public void Shoot(Vector2 direction,ColorAttribute.COLOR_TYPE _element, GameObject _player)
    {        
        shoot = true;
        colorType = _element;
        player = _player;
        dir = direction;
        rigid.velocity = dir * speed;
        UpdateColor();
    }


    private void UpdateColor()
    {
        switch(colorType)
        {            
            case ColorAttribute.COLOR_TYPE.GREEN:
                sr.sprite = sprites[0];
                break;
            case ColorAttribute.COLOR_TYPE.MAGENTA:
                sr.sprite = sprites[1];
                break;
            case ColorAttribute.COLOR_TYPE.ORANGE:
                sr.sprite = sprites[2];
                break;
            case ColorAttribute.COLOR_TYPE.YELLOW:
                sr.sprite = sprites[3];
                break;
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

    private void PopDamageText(int bulletDamage)
    {
        GameObject go = Instantiate(popText.gameObject, transform.position, Quaternion.identity, transform.parent);
        go.GetComponent<PopText>().CreateText(bulletDamage.ToString(), Color.black);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(!targetLockOn)
        {
            if(collision.tag == "Enemy" || collision.tag == "Balloon")
            {
                target = collision.gameObject;
                targetLockOn = true;
                detector.enabled = false;
            }
        }
        else
        {
            if(collision.tag == "Enemy" && collision.GetComponent<Enemy>().IsAlive())
            {
                damage = CalculateElementalDamage(damage, colorType, collision.GetComponent<Enemy>().color);
                collision.GetComponent<Enemy>().TakeDamage(damage, player);
                PopDamageText(damage);
                Destroy(gameObject);
            }
            if(collision.tag == "Balloon")
            {
                collision.GetComponent<Balloon>().TakeDamage(colorType, player);
                Destroy(gameObject);
            }
        }
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
