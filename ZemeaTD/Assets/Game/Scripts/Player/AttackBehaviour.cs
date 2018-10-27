﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : MonoBehaviour {

    public float maxAngleAttack = 65f; 
    public float angleAttackSpeed = 30f;
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
    private CharacterController2D player;

    private void Start()
    {
        inputPlayer = GetComponent<InputControl>();
        bulletsContainer = new GameObject("BulletsContainer");
        player = GetComponent<CharacterController2D>();
        timer = timeBetweenAttacks;
    }

    public void AttackControl()
    {
        if (Input.GetAxis(inputPlayer.axisY) < 0)
        {
            if (IsFacingRight())
            {
                angleAttack.z += Time.deltaTime * angleAttackSpeed;
            }
            else
            {
                angleAttack.z -= Time.deltaTime * angleAttackSpeed;

            }
        }
        else if (Input.GetAxis(inputPlayer.axisY) > 0)
        {
            if (IsFacingRight())
            {
                angleAttack.z -= Time.deltaTime * angleAttackSpeed;
            }
            else
            {
                angleAttack.z += Time.deltaTime * angleAttackSpeed;
            }
        }
        if (angleAttack.z > maxAngleAttack)
        {
            angleAttack.z = maxAngleAttack;
        }
        else if (angleAttack.z < -maxAngleAttack)
        {
            angleAttack.z = -maxAngleAttack;
        }
        crosshair.transform.eulerAngles = angleAttack;

        CalculateAttackSpeed();

        if (Input.GetKey(inputPlayer.secondaryButton))
        {
            if(timer >= timeBetweenAttacks) {
                Shoot();
            }
            else
                timer += Time.deltaTime;
        }
    }

    private void Shoot()
    {
        GameObject b = Instantiate(bullet, transform.position, transform.rotation, bulletsContainer.transform);
        Vector2 bulletDirection = crossPos.position - transform.position;
        CalculatePlayerDamage();
        b.GetComponent<ElementalProyectile>().Shoot(bulletDirection.normalized, playerDamage, currentElement, this.gameObject);
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
