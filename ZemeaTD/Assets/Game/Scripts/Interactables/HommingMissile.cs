using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HommingMissile : Proyectile
{

    public ColorAttribute.COLOR_TYPE colorType;
    public float rotatingSpeed = 200;
    public GameObject target;
    public Sprite[] sprites;
    public CircleCollider2D detector;
    private bool shoot = false;
    private bool targetLockOn = false;
    private float timer = 0;
    private BoxCollider2D boxCollider;

    protected override void Awake()
    {
        base.Awake();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    private void Start()
    {
        speed = 40f;
        damage = 10;
        lifeTimer = 3f;
    }
    private void Update()
    {
        if(timer < lifeTimer)
            timer += Time.deltaTime;
        else
            DestroyProyectile();
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
            if(targetLockOn && target)
            {
                direction = (Vector2)transform.position - (Vector2)target.transform.position;
                direction.Normalize();
                float value = Vector3.Cross(direction, transform.right).z;
                rigid.angularVelocity = rotatingSpeed * value;
                rigid.velocity = transform.right * speed;
            }
            else
            {
                transform.right = rigid.velocity;
            }
        }
    }

    public void Shoot(Vector2 directionection,ColorAttribute.COLOR_TYPE _element, GameObject _player)
    {
        shoot = true;
        colorType = _element;
        player = _player;
        direction = directionection;
        rigid.velocity = direction * speed;
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
            if(collision.IsTouching(boxCollider))
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
    }
}
