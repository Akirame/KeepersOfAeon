﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : MonoBehaviour {

    public float maxAngleAttack = 65f; 
    public float angleAttackSpeed = 120f;
    public GameObject crosshair;
    public GameObject bullet;
    public Transform crossPos;
    public ElementalOrb currentElement;
    private Vector3 angleAttack;
    private InputControl inputPlayer;
    private GameObject bulletsContainer;
    private int playerDamage;
    private float timer;
    public float timeBetweenAttacks = 0.2f;
    public float minAttackSpeed = 0.01f;
    public ElementalOrb element;
    private CharacterController2D player;
    private float rAxis = 0f;
    private float lAxis = 0f;
    private bool triggerTouched = false;
    private bool shooted = false;
    private void Start()
    {
        inputPlayer = GetComponent<InputControl>();
        bulletsContainer = new GameObject("BulletsContainer");
        player = GetComponent<CharacterController2D>();
        timer = timeBetweenAttacks;
        
    }
    private void Update()
    {
        rAxis = Input.GetAxis(inputPlayer.RTrigger);
        lAxis = Input.GetAxis(inputPlayer.LTrigger);

        if(rAxis >= 1f && !triggerTouched)
        {
            element.CicleUpElement();
            triggerTouched = true;
        }        
        if(lAxis >= 1f && !triggerTouched)
        {
            element.CicleDownElement();
            triggerTouched = true;
        }
        if(lAxis == 0f && rAxis == 0f)
            triggerTouched = false;
    }
    public void AttackControl()
    {
        if(Input.GetAxis(inputPlayer.axisY) < 0 || Input.GetAxis(inputPlayer.axisYKey) < 0)
        {
            if(IsFacingRight())
            {
                angleAttack.z += Time.deltaTime * angleAttackSpeed * Math.Abs(Input.GetAxis(inputPlayer.axisY));
                angleAttack.z += Time.deltaTime * angleAttackSpeed * Math.Abs(Input.GetAxis(inputPlayer.axisYKey));
            }
            else
            {
                angleAttack.z -= Time.deltaTime * angleAttackSpeed * Math.Abs(Input.GetAxis(inputPlayer.axisY));
                angleAttack.z -= Time.deltaTime * angleAttackSpeed * Math.Abs(Input.GetAxis(inputPlayer.axisYKey));
            }
        }
        else if(Input.GetAxis(inputPlayer.axisY) > 0 || Input.GetAxis(inputPlayer.axisYKey) > 0)
        {
            if(IsFacingRight())
            {
                angleAttack.z -= Time.deltaTime * angleAttackSpeed * Math.Abs(Input.GetAxis(inputPlayer.axisY));
                angleAttack.z -= Time.deltaTime * angleAttackSpeed * Math.Abs(Input.GetAxis(inputPlayer.axisYKey));
            }
            else
            {
                angleAttack.z += Time.deltaTime * angleAttackSpeed * Math.Abs(Input.GetAxis(inputPlayer.axisY));
                angleAttack.z += Time.deltaTime * angleAttackSpeed * Math.Abs(Input.GetAxis(inputPlayer.axisYKey));
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

        if(shooted)
        {
            if(timer >= timeBetweenAttacks)
            {
                shooted = false;
            }
            else
                timer += Time.deltaTime;         
        }
        if(Input.GetButton(inputPlayer.attackButton) && !shooted)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        shooted = true;
        GameObject b = Instantiate(bullet, transform.position, transform.rotation, bulletsContainer.transform);
        Vector2 bulletDirection = crossPos.position - transform.position;
        CalculatePlayerDamage();
        b.GetComponent<ElementalProyectile>().Shoot(bulletDirection.normalized, playerDamage, element.elementType, this.gameObject);
        timer = 0;
    }

    private void CalculatePlayerDamage()
    {
        playerDamage = UnityEngine.Random.Range(player.playerData.minDamage, player.playerData.maxDamage);
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

    public void ChangeElement(ElementalOrb element)
    {
        currentElement = element;
    }
}
