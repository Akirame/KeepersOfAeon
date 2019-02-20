using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Proyectile : MonoBehaviour
{
    public float speed;
    public int damage;
    public PopText popText;
    public GameObject player;
    public SpriteRenderer sr;
    protected Rigidbody2D rigid;
    protected bool onGround = false;
    protected float lifeTimer;
    protected Vector2 direction;

    protected virtual void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();        
    }

    protected virtual void PopDamageText(int bulletDamage)
    {
        GameObject go = Instantiate(popText.gameObject, transform.position, Quaternion.identity, transform.parent);
        go.GetComponent<PopText>().CreateText(bulletDamage.ToString(), Color.black);
    }

    protected void OnBecameInvisible()
    {
        DestroyProyectile();
    }
    public int GetDamage()
    {
        return damage;
    }
    public void DestroyProyectile()
    {
        Destroy(gameObject);
    }
}
