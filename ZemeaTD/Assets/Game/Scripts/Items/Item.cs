﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
    public delegate void ItemActions(Item i);
    public static ItemActions InvulnerableConsumed;
    public static ItemActions ItemConsumed;
    public static ItemActions ChickenConsumed;
    public static ItemActions DamageConsumed;
    public static ItemActions WeaponPowerUpConsumed;
    public IceExplosion iceExplosion;
    public float lifeTime = 8;
    public AudioClip sound;
    private bool consumed = false;
    private float timer = 0;

    public enum TypeOfItem
    {
      InvulnerableShield,
      RalenticeEnemies,
      Chicken,
      DoubleDamage,
      WeaponPowerUp
    }
    public TypeOfItem itemType;

    private void Update()
    {
        if(timer < lifeTime)
            timer += Time.deltaTime;
        else
            ItemConsumed(this);
    }
    protected void ConsumeItem()
    {        
        switch(itemType)
        {
            case TypeOfItem.InvulnerableShield:
                InvulnerableConsumed(this);
                break;
            case TypeOfItem.RalenticeEnemies:
                CreateIceExplosion();
                break;
            case TypeOfItem.Chicken:
                ChickenConsumed(this);
                break;
            case TypeOfItem.DoubleDamage:
                DamageConsumed(this);
                break;
            case TypeOfItem.WeaponPowerUp:
                WeaponPowerUpConsumed(this);
                break;
        }
        consumed = true;
        ItemConsumed(this);
    }

    private void CreateIceExplosion()
    {
        Instantiate(iceExplosion.gameObject, transform.position, Quaternion.identity, transform.parent);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
            ConsumeItem();
    }

    public bool WasConsumed()
    {
        return consumed;
    }
}
