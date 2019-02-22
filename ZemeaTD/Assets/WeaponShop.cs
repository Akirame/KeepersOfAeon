using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShop : MonoBehaviour
{
    public Animator anim;
    public ParticleSystem ps;
    public bool upgradeReady = false;    
    private GameObject playerOnUse;

    private void Start()
    {
        PlayerLevel.OnLevelUp += OnPlayerLevelUp;
    }
    private void Update()
    {
        if(upgradeReady && !ps.isEmitting)
        {
            ps.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && !playerOnUse)
        {
            if(collision.gameObject.GetComponent<PlayerLevel>().upgradeLevel)
            {
                playerOnUse = collision.gameObject;
                anim.SetTrigger("TriggerEnter");
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject == playerOnUse)
        {            
            playerOnUse = null;
            anim.SetTrigger("TriggerExit");
        }
    }
    private void OnDestroy()
    {
        PlayerLevel.OnLevelUp -= OnPlayerLevelUp;
    }
    private void OnPlayerLevelUp(PlayerLevel pl)
    {
        upgradeReady = true;
    }
}

