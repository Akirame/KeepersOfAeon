using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalOrb :MonoBehaviour
{

    public delegate void OrbActions(ElementalOrb e);
    public static OrbActions OnConsumption;
    public enum ELEMENT_TYPE
    {
        WATER = 0,
        FIRE,
        EARTH,
        NONE,
        Count
    };

    public ELEMENT_TYPE elementType;
    public Color c;
    public bool pickedUp = false;
    public GameObject playerAttached;
    public GameObject orbStash;
    public ElementalExplosion explosion;
    public float respawnTime;
    public Sprite[] orbSprites;
    private bool exploded = false;
    private Vector2 initialPos;
    private Rigidbody2D rigid;
    private Animator anim;
    private GameObject lastPlayerAttached;
    private SpriteRenderer rend;
    private float timerRespawn;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        rigid = gameObject.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        initialPos = transform.position;
    }

    private void Update()
    {
        if (exploded)
        {            
            timerRespawn += Time.deltaTime;
            if (timerRespawn >= respawnTime)
            {
                rend.enabled = true;
                transform.position = initialPos;
                rigid.velocity = new Vector2();
                exploded = false;
                timerRespawn = 0;
            }
        }
    }

    public void AttachToPlayer(GameObject player, Transform newPos)
    {
        pickedUp = true;
        playerAttached = player;
        lastPlayerAttached = player;
        rigid.bodyType = RigidbodyType2D.Kinematic;
        transform.position = newPos.position;
        transform.parent = playerAttached.transform;
        rigid.velocity = new Vector2();
    }

    public void ActivateOutline(bool set)
    {
        anim.SetBool("onTouch", set);
    }

    public void Throw(float force)
    {
        rigid.bodyType = RigidbodyType2D.Dynamic;
        if(playerAttached.GetComponent<CharacterController2D>().lookingRight)
            rigid.velocity = new Vector2(1, 2) * force;
        else
            rigid.velocity = new Vector2(-1, 2) * force;

        pickedUp = false;
        transform.parent = playerAttached.transform.parent;
        playerAttached = null;
    }
    public ElementalOrb Consume()
    {
        playerAttached.GetComponent<AttackBehaviour>().currentElement = this;
        pickedUp = false;
        OnConsumption(this);
        return this;
    }
    public void UpdateColor()
    {
        switch(elementType)
        {
            case ELEMENT_TYPE.EARTH:
                c = Color.green;
                rend.sprite = orbSprites[0];
                break;
            case ELEMENT_TYPE.FIRE:
                c = Color.red;
                rend.sprite = orbSprites[1];
                break;
            case ELEMENT_TYPE.WATER:
                c = Color.blue;
                rend.sprite = orbSprites[2];
                break;
            case ELEMENT_TYPE.NONE:
                c = Color.white;
                rend.sprite = orbSprites[3];
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if(collision.gameObject.tag == "Detector")
            ActivateOutline(true);
        if (collision.tag == "Ground")
        {
            Explode();
        }
    }

    private void Explode()
    {
        GameObject e = Instantiate(explosion.gameObject, transform.position, Quaternion.identity, transform.parent);
        e.GetComponent<ElementalExplosion>().SetPlayerThrow(lastPlayerAttached);
        exploded = true;
        rend.enabled = false;        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Detector")
            ActivateOutline(false);
    }

}
