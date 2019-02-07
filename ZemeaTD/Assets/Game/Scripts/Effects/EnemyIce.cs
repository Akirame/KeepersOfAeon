using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIce : MonoBehaviour
{

    public Sprite[] sprites;
    public float iceDuration = 10;
    private int sortingOrd;
    private float iceAnimDuration = 12;
    private Animator anim;
    private ParticleSystem ps;
    private float timer = 0;
    private SpriteRenderer sr;
    private bool isActive;

    private void Start()
    {
        anim = GetComponent<Animator>();
        ps = GetComponentInChildren<ParticleSystem>();
        sr = GetComponentInChildren<SpriteRenderer>();
        sr.sprite = sprites[UnityEngine.Random.Range(0, sprites.Length)];
        ps.Pause();
        Activate(true);
    }

    private void Update()
    {
        if (isActive)
        {
            if (sr.sortingOrder != sortingOrd)
            {
                sr.sortingOrder = sortingOrd;
            }
            timer += Time.deltaTime;
            anim.SetFloat("timeLeft", iceDuration - timer);
            if (timer >= iceDuration)
            {
                Activate(false);
            }
            else if (timer >= iceAnimDuration)
            {
                Destroy(gameObject);
            }
        }
    }

    public void SetSortingOrder(int l)
    {
        sortingOrd = l + 1;
    }

    public void Activate(bool v)
    {
        anim.SetBool("activated", v);
        isActive = v;
    }

    private void StartParticles()
    {
        ps.Play();
    }

}
