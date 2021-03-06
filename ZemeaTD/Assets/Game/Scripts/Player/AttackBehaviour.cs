﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : MonoBehaviour {


    [Header("Attack Vars")]
    public float timeBetweenAttacks = 0.2f;
    public float minAttackSpeed = 0.01f;
    public int playerDamage;
    public float maxAngleAttack = 65f;
    public float angleAttackSpeed = 120f;
    public GameObject bullet;
    private Vector3 angleAttack;
    private GameObject bulletsContainer;
    private bool shooted = false;
    private float criticalChance;
    private bool criticalAttack;
    public GameObject boulder;
    public GameObject hommingMissile;

    [Header("Crosshair Vars")]
    public Transform crossPos;
    public GameObject crosshair;

    [Header("Color Attribute Vars")]
    public ColorAttribute colorOrb;
    public ColorAttribute currentColor;

    [Header("Misc Vars")]
    public Sprite[] aimSprites;
    public AudioClip[] audioClips;
    private AudioSource aSource;
    private float timer;
    public PopText popText;

    [Header("Controllers Vars")]
    private InputControl inputPlayer;
    private CharacterController2D player;
    private float rAxis = 0f;
    private float lAxis = 0f;
    private bool triggerTouched = false;

    public enum TypeOfShoot { Normal, MachineGun, ChargeShot, Boulder, Homming, Penetrating};

    [Header("Weapons Modifier")]
    public Transform weaponPos;
    public TypeOfShoot shootType = TypeOfShoot.Normal;
    public float chargeShotMax = 5f;
    public float chargeShotCounter = 1f;
    public bool penetratingBulletsOn = false;
    public int shootQuantity = 1;
    public bool boulderOn = false;
    public bool hommingOn = true;

    [Header("Item Modifier")]
    public float lifetimeItemDamageBonus = 5;
    private float itemDamageBonusTimer;
    public int itemDamageBonus = 1;
    private bool doubleDamage = false;
    public float lifetimeItemWeaponPowerUp = 20;
    private float WeaponPowerUpTimer;
    private bool weaponPowerUp = false;

    private void Start()
    {
        inputPlayer = GetComponent<InputControl>();
        bulletsContainer = new GameObject("BulletsContainer");
        player = GetComponent<CharacterController2D>();
        timer = timeBetweenAttacks;
        aSource = GetComponent<AudioSource>();
        AudioManager.Get().AddSound(aSource);
        criticalChance = player.playerData.criticalChance;
        Item.DamageConsumed += OnDoubleDamageGet;
        Item.WeaponPowerUpConsumed += OnWeaponPoweUpGet;
    }

    private void OnDestroy()
    {
        Item.DamageConsumed -= OnDoubleDamageGet;
        Item.WeaponPowerUpConsumed -= OnWeaponPoweUpGet;
    }

    private void Update()
    {
        rAxis = Input.GetAxis(inputPlayer.RTrigger);
        lAxis = Input.GetAxis(inputPlayer.LTrigger);

        if(rAxis >= 1f && !triggerTouched)
        {
            currentColor.CicleUpColor();
            UpdateAimColor();
            triggerTouched = true;
        }        
        if(lAxis >= 1f && !triggerTouched)
        {
            currentColor.CicleDownColor();
            UpdateAimColor();
            triggerTouched = true;
        }
        if(lAxis == 0f && rAxis == 0f)
            triggerTouched = false;

        if (doubleDamage)
        {
            itemDamageBonusTimer += Time.deltaTime;
            if (itemDamageBonusTimer >= lifetimeItemDamageBonus)
            {
                itemDamageBonusTimer = 0;
                doubleDamage = false;
                itemDamageBonus = 1;
            }
        }

        if (weaponPowerUp)
        {
            WeaponPowerUpTimer += Time.deltaTime;
            if (WeaponPowerUpTimer >= lifetimeItemWeaponPowerUp)
            {
                WeaponPowerUpTimer = 0;
                weaponPowerUp = false;
                shootType = TypeOfShoot.Normal;
                boulderOn = false;
                hommingOn = false;
            }
        }

    }

    public void AttackControl()
    {
        if(Input.GetAxis(inputPlayer.axisY) < 0 || Input.GetAxis(inputPlayer.axisYKey) > 0)
        {
            if(IsFacingRight())
            {
                angleAttack.z += Time.deltaTime * angleAttackSpeed * Math.Abs(Input.GetAxis(inputPlayer.axisY) + Input.GetAxis(inputPlayer.axisYKey));
            }
            else
            {
                angleAttack.z -= Time.deltaTime * angleAttackSpeed * Math.Abs(Input.GetAxis(inputPlayer.axisY) + Input.GetAxis(inputPlayer.axisYKey));
            }
        }
        else if(Input.GetAxis(inputPlayer.axisY) > 0 || Input.GetAxis(inputPlayer.axisYKey) < 0)
        {
            if(IsFacingRight())
            {
                angleAttack.z -= Time.deltaTime * angleAttackSpeed * Math.Abs(Input.GetAxis(inputPlayer.axisY) + Input.GetAxis(inputPlayer.axisYKey));
            }
            else
            {
                angleAttack.z += Time.deltaTime * angleAttackSpeed * Math.Abs(Input.GetAxis(inputPlayer.axisY) + Input.GetAxis(inputPlayer.axisYKey));
            }
        }
        if(angleAttack.z > maxAngleAttack)
        {
            angleAttack.z = maxAngleAttack;
        }
        else if(angleAttack.z < -maxAngleAttack)
        {
            angleAttack.z = -maxAngleAttack;
        }
        crosshair.transform.eulerAngles = angleAttack;

        CalculateAttackSpeed();

        switch(shootType)
        {
            case TypeOfShoot.Normal:
            case TypeOfShoot.Boulder:
            case TypeOfShoot.Homming:
            case TypeOfShoot.Penetrating:
                if(Input.GetButtonDown(inputPlayer.attackButton) && !shooted)
                {
                    Shoot();
                }
                break;
            case TypeOfShoot.MachineGun:
                if(Input.GetButton(inputPlayer.attackButton) && !shooted)
                {
                    Shoot();
                }
                break;
            case TypeOfShoot.ChargeShot:
                if(Input.GetButton(inputPlayer.attackButton) && !shooted)
                {
                    if(chargeShotCounter < chargeShotMax)
                        chargeShotCounter += 0.50f * Time.deltaTime;
                    else
                        chargeShotCounter = chargeShotMax;
                }
                else if(Input.GetButtonUp(inputPlayer.attackButton) && !shooted)
                {                    
                        Shoot();
                        chargeShotCounter = 1f;
                }
                break;
        }   

        if (shooted)
        {
            if(timer >= timeBetweenAttacks)
            {
                shooted = false;
            }
            else
                timer += Time.deltaTime;
        }
 
    }

    private void Shoot()
    {
        criticalAttack = false;
        shooted = true;
        CalculatePlayerDamage();
        //Normal Shoot        
        for(int i = 0; i < shootQuantity; i++)
        {
            GameObject b = Instantiate(bullet, weaponPos.position, transform.rotation, bulletsContainer.transform);
            Vector2 bulletDirection = crossPos.position - transform.position;
            bulletDirection = new Vector2(bulletDirection.x, (bulletDirection.y + UnityEngine.Random.Range(-1f, 1f)));
            b.transform.localScale = new Vector3(b.transform.localScale.x * chargeShotCounter, b.transform.localScale.y * chargeShotCounter,1);
            b.GetComponent<ColorProyectile>().Shoot(bulletDirection.normalized, playerDamage, currentColor.colorType, this.gameObject, criticalAttack, penetratingBulletsOn);
        }
        //Boulder Shoot       
        if(UnityEngine.Random.Range(0f,1f) > 0.7f && boulderOn)
        {
            Vector2 bulletDirection = crossPos.position - transform.position;
            GameObject g = Instantiate(boulder, weaponPos.position, transform.rotation, bulletsContainer.transform);
            g.GetComponent<Boulder>().Shoot(bulletDirection.normalized, playerDamage, this.gameObject);
        }        
        //Homming Shoot
       
        if(UnityEngine.Random.Range(0f, 1f) > 0.1f && hommingOn)
        {
            GameObject g = Instantiate(hommingMissile, weaponPos.position, transform.rotation, bulletsContainer.transform);
            Vector2 bulletDirection = crossPos.position - transform.position;            
            g.GetComponent<HommingMissile>().Shoot(bulletDirection.normalized, currentColor.colorType, this.gameObject);
        }
        timer = 0; 
        PlayRandomSound();
    }

    private void PlayRandomSound()
    {
        AudioClip ac = audioClips[UnityEngine.Random.Range(0, audioClips.Length)];
        aSource.PlayOneShot(ac);
    }

    private void CalculatePlayerDamage()
    {
        int baseDamage = UnityEngine.Random.Range(player.playerData.minDamage, player.playerData.maxDamage);
        playerDamage = baseDamage * ChargeShootMultiplier() * CriticalMultiplier() * itemDamageBonus;
    }

    private int CriticalMultiplier()
    {
        float chance = UnityEngine.Random.Range(0f, 1f);
        int criticalMultiplier = 1;
        if (criticalChance > chance)
        {
            criticalMultiplier = 2;
            criticalAttack = true;
        }
        return criticalMultiplier;
    }

    private int ChargeShootMultiplier()
    {
        int multiplier = 1;
        if (shootType == TypeOfShoot.ChargeShot)
            multiplier = (int)chargeShotCounter;
        return multiplier;
    }

    private void CalculateAttackSpeed()
    {
        if (timeBetweenAttacks != 1/player.playerData.attackSpeed)
        {
            timeBetweenAttacks = 1/player.playerData.attackSpeed;
        }
    }

    private bool IsFacingRight()
    {
        return transform.localScale.x > 0;

    }

    public void SetVisibilityCrosshair(bool val)
    {
        crossPos.gameObject.SetActive(val);
        angleAttack = Vector3.zero;
    }

    public void ChangeElement(ColorAttribute element)
    {
        colorOrb = element;
    }

    private void UpdateAimColor()
    {
        SpriteRenderer aimRenderer = crossPos.gameObject.GetComponent<SpriteRenderer>();
        switch (colorOrb.colorType)
        {
            case ColorAttribute.COLOR_TYPE.GREEN:
                aimRenderer.sprite = aimSprites[0];
                break;
            case ColorAttribute.COLOR_TYPE.MAGENTA:
                aimRenderer.sprite = aimSprites[1];
                break;
            case ColorAttribute.COLOR_TYPE.ORANGE:
                aimRenderer.sprite = aimSprites[2];
                break;
            case ColorAttribute.COLOR_TYPE.YELLOW:
                aimRenderer.sprite = aimSprites[3];
                break;
            case ColorAttribute.COLOR_TYPE.LAST:
                break;
            default:
                break;
        }
    }

    private void OnDoubleDamageGet(Item i)
    {
        itemDamageBonus = 2;
        doubleDamage = true;
        itemDamageBonusTimer = 0f;
    }
    private void OnWeaponPoweUpGet(Item i)
    {
        shootType = TypeOfShoot.ChargeShot;
        Debug.Log(shootType);
        boulderOn = shootType == TypeOfShoot.Boulder ? true : false;
        hommingOn = shootType == TypeOfShoot.Homming ? true : false;
        penetratingBulletsOn = (shootType == TypeOfShoot.Penetrating || shootType == TypeOfShoot.ChargeShot) ? true : false;

        weaponPowerUp = true;
        WeaponPowerUpTimer = 0f;
        PopLabel(shootType.ToString());
    }
    private void PopLabel(string label)
    {
        GameObject go = Instantiate(popText.gameObject, transform.position, Quaternion.identity, transform.parent);
        go.GetComponent<PopText>().CreateText(label, Color.white);
    }
}
