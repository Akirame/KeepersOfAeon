using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalOrb2 : MonoBehaviour {

    public bool pickedUp = false;
    public GameObject playerAttached;
    private Rigidbody2D rigid;

    private void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
    }

    public void AttachToPlayer(GameObject player, Transform newPos)
    {
        pickedUp = true;
        playerAttached = player;
        rigid.bodyType = RigidbodyType2D.Kinematic;
        transform.position = newPos.position;
        transform.parent = newPos;
        rigid.velocity = new Vector2();
    }

    internal void Throw()
    {
        rigid.bodyType = RigidbodyType2D.Dynamic;
        if(playerAttached.GetComponent<CharacterController2D>().lookingRight)
        rigid.velocity = new Vector2(10, 10);
        else
            rigid.velocity = new Vector2(-10, 10);
        pickedUp = false;
        transform.parent = playerAttached.transform.parent;
        playerAttached = null;
    }
}
