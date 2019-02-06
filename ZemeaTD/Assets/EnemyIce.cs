using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIce : MonoBehaviour {

    private Animator anim;
    private SpriteRenderer sr;
    private int orderingLayer;
    private ParticleSystem ps;
    private float timer = 10;
    private bool isActive = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
        sr = GetComponentInChildren<SpriteRenderer>();
        ps = GetComponentInChildren<ParticleSystem>();
        ps.Pause();
    }

    private void Update()
    {
        if (isActive)
        {
            timer -= Time.deltaTime;
            anim.SetFloat("timeLeft", timer);
            if (timer <= 0)
            {
                Activate(false);
                timer = 10;
            }
        }
    }

    public void SetOrderingLayer(int l)
    {
        orderingLayer = l;
        sr.sortingOrder = orderingLayer;
    }

    public void Activate(bool v)
    {
        anim.SetBool("activated",v);
        isActive = v;
    }

    private void StartParticles()
    {
        ps.Play();
    }

}
