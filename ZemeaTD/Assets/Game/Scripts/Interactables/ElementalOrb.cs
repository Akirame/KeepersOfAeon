using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalOrb :MonoBehaviour
{

    public delegate void OrbActions(ElementalOrb e);
    public static OrbActions OnConsumption;
    public enum ELEMENT_TYPE { WATER, FIRE, EARTH, NONE };
    public ELEMENT_TYPE elementType;
    public Color c;
    public bool pickedUp = false;
    public GameObject playerAttached;
    public GameObject orbStash;
    private Rigidbody2D rigid;
    private Animator anim;

    private void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    public void AttachToPlayer(GameObject player, Transform newPos)
    {
        pickedUp = true;
        playerAttached = player;
        rigid.bodyType = RigidbodyType2D.Kinematic;
        transform.position = newPos.position;
        transform.parent = playerAttached.transform;
        rigid.velocity = new Vector2();
    }

    public void ActivateOutline(bool set)
    {
        anim.SetBool("onTouch", set);
    }

    public void Throw()
    {
        rigid.bodyType = RigidbodyType2D.Dynamic;
        if(playerAttached.GetComponent<CharacterController2D>().lookingRight)

            rigid.velocity = new Vector2(12, 15);
        else
            rigid.velocity = new Vector2(-12, 15);

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

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if(collision.gameObject.tag == "Detector")
            ActivateOutline(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Detector")
            ActivateOutline(false);
    }

}
