﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBullet : MonoBehaviour {

    public float speed = 200f;
    public Vector2 direction;

    // Update is called once per frame
    void Update () {
        GetComponent<Rigidbody2D>().velocity = direction * speed * Time.deltaTime;
	}

    public void Shoot(Vector2 dir, Vector2 angleAttack)
    {
        direction = dir;
        transform.eulerAngles = angleAttack;
    }

}
