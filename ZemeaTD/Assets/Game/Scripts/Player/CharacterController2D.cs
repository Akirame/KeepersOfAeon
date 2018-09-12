using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour    
{
    public enum PLAYER_STATES {IDLE,RUNNING,JUMP,ATTACK,ON_ACTION};
    public CharacterData playerData;
    private bool canMove = true;
    private bool onFloor = false;
    public LayerMask floorLayer;
    public PLAYER_STATES currentState;
    private Rigidbody2D rig;
    public InputControl inputControl;


    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        SetAttackMode(false);
        inputControl = GetComponent<InputControl>();
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
                break;
            case PLAYER_STATES.RUNNING:
                Movement();
                GroundControl();
                JumpBehaviour();
                break;
            case PLAYER_STATES.JUMP:
                Movement();
                GroundControl();
                break;
            case PLAYER_STATES.ATTACK:
                AttackMode();
                break;
            default:
                break;
        }
    }

    private void AttackMode()
    {
        GetComponent<AttackBehaviour>().AttackControl();
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

    private void Movement()
    {
        if (canMove)
        {
            float movSpeed;
            if (onFloor)
            {
                movSpeed = playerData.floorSpeed;
            }
            else
            {
                movSpeed = playerData.airSpeed;
            }
            if (Input.GetKey(inputControl.moveLeft))
            {
                transform.localScale = new Vector3(-1, 1, 1);
                rig.velocity = new Vector2(-movSpeed * Time.deltaTime, rig.velocity.y);
            }
            else if (Input.GetKey(inputControl.moveRight))
            {
                transform.localScale = new Vector3(1, 1, 1);
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
        if (onFloor && Input.GetKeyDown(inputControl.jump))
        {
            onFloor = false;
            rig.velocity = new Vector2(0, playerData.jumpForce);
        }
    }

    public void SetCanMove(bool setMove)
    {
        canMove = setMove;
    }

    public void SetAttackMode(bool val)
    {
        if (val)
        {
            currentState = PLAYER_STATES.ATTACK;
            GetComponent<AttackBehaviour>().SetVisibilityCrosshair(true);
        }
        else
        {
            currentState = PLAYER_STATES.IDLE;
            GetComponent<AttackBehaviour>().SetVisibilityCrosshair(false);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKeyDown(inputControl.openDoor) && collision.tag == "Door")
        {
            collision.GetComponent<Door>().GoToNextDoor(gameObject);
        }
    }

}
