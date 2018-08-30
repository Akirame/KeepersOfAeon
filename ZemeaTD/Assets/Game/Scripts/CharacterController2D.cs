﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour    
{
    #region singleton
    private static CharacterController2D instance;
    public static CharacterController2D GetInstance()
    {
        return instance;
    }
    private void Awake()
    {
        if (!instance)
            instance = this;
        else
            Destroy(instance.gameObject);
    }
    #endregion  

    public enum PLAYER_STATES {IDLE,RUNNING,JUMP,ATTACK,ON_ACTION};
    public float speed = 200f;
    public float airSpeed = 150f;
    public float jumpForce = 5f;
    private bool onDoor = false;
    private bool canMove = true;
    private Door lastDoorTouched;
    private bool onFloor = false;
    private bool onAttackMode = false;
    public LayerMask floorLayer;
    public PLAYER_STATES currentState;
    private Rigidbody2D rig;
    private void Start()
    {
        lastDoorTouched = null;
        currentState = PLAYER_STATES.IDLE;
        rig = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        StateController();
        StateBehaviour();
    }

    public void StateBehaviour()
    {
        switch (currentState)
        {
            case PLAYER_STATES.IDLE:
                Movement();
                GroundControl();
                JumpBehaviour();
                DoorControl();
                break;
            case PLAYER_STATES.RUNNING:
                Movement();
                GroundControl();
                JumpBehaviour();
                DoorControl();
                break;
            case PLAYER_STATES.JUMP:
                Movement();
                GroundControl();
                break;
            case PLAYER_STATES.ATTACK:
                break;
            default:
                break;
        }
    }

    private void StateController()
    {
        switch (currentState)
        {
            case PLAYER_STATES.IDLE:
                if (onFloor)
                {
                    if (rig.velocity.x != 0)
                        currentState = PLAYER_STATES.RUNNING;
                }
                else
                {
                    currentState = PLAYER_STATES.JUMP;
                }
                break;
            case PLAYER_STATES.RUNNING:
                if (onFloor)
                {
                    if (rig.velocity.x == 0)
                        currentState = PLAYER_STATES.IDLE;
                }
                else
                {
                    currentState = PLAYER_STATES.JUMP;
                }
                break;
            case PLAYER_STATES.JUMP:
                if (onFloor)
                {
                    currentState = PLAYER_STATES.IDLE;
                }
                break;
            case PLAYER_STATES.ATTACK:
                break;
            case PLAYER_STATES.ON_ACTION:
                break;
            default:
                break;
        }
    }

    private void AttackControl()
    {
        if (onAttackMode)
        {
            canMove = false;
        }
    }

    private void DoorControl()
    {
        if (onDoor)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                lastDoorTouched.GotoObjective(this.gameObject);
            }
        }
    }

    private void Movement()
    {
        if (canMove)
        {
            float movSpeed;
            if (onFloor)
            {
                movSpeed = speed;
            }
            else
            {
                movSpeed = airSpeed;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                GetComponent<SpriteRenderer>().flipX = true;
                rig.velocity = new Vector2(-movSpeed * Time.deltaTime, rig.velocity.y);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                GetComponent<SpriteRenderer>().flipX = false;
                rig.velocity = new Vector2(movSpeed * Time.deltaTime, rig.velocity.y);
            }
            else
            {
                if (onFloor)
                {
                    rig.velocity = new Vector2(0, rig.velocity.y);
                }
            }
        }

    }

    public void GroundControl()
    {
        Vector2 minSpriteSize = new Vector2(0, -GetComponent<SpriteRenderer>().size.y / 2);
        Vector2 floorContact = (Vector2)transform.position + minSpriteSize;
        if (Physics2D.Raycast(floorContact, Vector2.down, 0.1f, floorLayer))
        {
            onFloor = true;
        }
        else
        {
            onFloor = false;
        }
    }

    public void JumpBehaviour()
    {
        if (onFloor && Input.GetKeyDown(KeyCode.UpArrow))
        {
            onFloor = false;
            rig.velocity = new Vector2(0, jumpForce);
        }
    }

    public void SetCanMove(bool setMove)
    {
        canMove = setMove;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Door")
        {            
            if (!onDoor)
            {
                onDoor = true;
                lastDoorTouched = collision.gameObject.GetComponent<Door>();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Door")
        {            
            onDoor = false;
            lastDoorTouched = null;
        }
    }

    public void SetAttackMode(bool val)
    {
        onAttackMode = val;
    }

}
