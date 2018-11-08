using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    public enum PLAYER_STATES { Idle, Running, Jump, Attack, OnAction };
    public CharacterData playerData;
    public PLAYER_STATES currentState;
    public LayerMask floorLayer;
    public InputControl inputControl;
    public SpriteRenderer spriteRend;
    public bool canJump = true;
    public bool dobleJump = false;
    public bool lookingRight;
    private bool canMove = true;
    private bool onFloor = false;
    public ParticleSystem psDust;
    private Rigidbody2D rig;
    private AttackBehaviour attackComponent;
    private Animator anim;
    private Vector2 movement;
    private float changuiTime = 0.162f;
    private float timerChangui;

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        SetAttackMode(false);
        inputControl = GetComponent<InputControl>();
        attackComponent = GetComponent<AttackBehaviour>();
        anim = GetComponent<Animator>();
        lookingRight = true;
        psDust.Stop();
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
            case PLAYER_STATES.Idle:
                Movement();
                GroundControl();
                JumpBehaviour();
                break;
            case PLAYER_STATES.Running:
                Movement();
                GroundControl();
                JumpBehaviour();
                break;
            case PLAYER_STATES.Jump:
                Movement();
                GroundControl();
                JumpBehaviour();
                break;
            case PLAYER_STATES.Attack:
                AttackMode();
                break;
            default:
                break;
        }
    }

    private void AttackMode()
    {
        attackComponent.AttackControl();
        anim.SetFloat("axis", 0);
    }

    private void StateController()
    {
        switch (currentState)
        {
            case PLAYER_STATES.Idle:
                if (onFloor)
                {
                    if (rig.velocity.x != 0)
                        currentState = PLAYER_STATES.Running;
                }
                else
                {
                    currentState = PLAYER_STATES.Jump;
                }
                break;
            case PLAYER_STATES.Running:
                if (onFloor)
                {
                    if (rig.velocity.x == 0)
                        currentState = PLAYER_STATES.Idle;
                }
                else
                {
                    currentState = PLAYER_STATES.Jump;
                }
                break;
            case PLAYER_STATES.Jump:
                if (onFloor)
                {
                    currentState = PLAYER_STATES.Idle;
                }
                break;
            case PLAYER_STATES.Attack:
                break;
            case PLAYER_STATES.OnAction:
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
            movement.x = (Input.GetAxis(inputControl.axisH) + Input.GetAxis(inputControl.axisHKey));
            rig.velocity = new Vector2(movSpeed * Time.deltaTime * movement.x,rig.velocity.y);
            if (movement.x > 0)
            {
                SetFacingRight(true);
            }
            else if(movement.x < 0)
            {
                SetFacingRight(false);
            }
            anim.SetFloat("axis", Mathf.Abs(movement.x));
        }
    }

    public void GroundControl()
    {
        Vector2 minSpriteSize = new Vector2(0, -spriteRend.size.y / 2);
        Vector2 floorContact = (Vector2)transform.position + minSpriteSize;
        RaycastHit2D hit = Physics2D.Raycast(floorContact, Vector2.down, 2f, floorLayer);                
        if (hit)
        {
            if(hit.transform.gameObject.tag == "OneWay" && (Input.GetAxis(inputControl.axisY) + Input.GetAxis(inputControl.axisYKey) > 0) && Input.GetButtonDown(inputControl.jump))
            {                
                hit.transform.GetComponent<OneWayPlatform>().Deactivate();
                onFloor = false;                
            }
            else
            onFloor = true;
            timerChangui = 0;
        }
        else
        {
            timerChangui += Time.deltaTime;
            if (timerChangui > changuiTime)
            {
                onFloor = false;
            }
        }
    }

    public void JumpBehaviour()
    {
        if(onFloor && Input.GetButtonDown(inputControl.jump) && canJump)
        {
            onFloor = false;
            dobleJump = true;
            rig.velocity = new Vector2(0, playerData.jumpForce);
            psDust.Play();
        }
        else if(!onFloor && Input.GetButtonDown(inputControl.jump) && dobleJump)
        {
            rig.velocity = new Vector2(0, playerData.jumpForce);
            dobleJump = false;
            psDust.Play();
        }
        else if(dobleJump && onFloor)
            dobleJump = false;
    }

    public void SetCanMove(bool setMove)
    {
        canMove = setMove;
    }

    public void SetAttackMode(bool val)
    {
        if (val)
        {
            currentState = PLAYER_STATES.Attack;
            GetComponent<AttackBehaviour>().SetVisibilityCrosshair(true);
        }
        else
        {
            currentState = PLAYER_STATES.Idle;
            GetComponent<AttackBehaviour>().SetVisibilityCrosshair(false);
        }
    }

    public void SetFacingRight(bool faceRight)
    {
        if (faceRight)
        {
            transform.localScale = new Vector2(2, 2);
        }
        else
        {
            transform.localScale = new Vector2(-2, 2);
        }
        lookingRight = faceRight;
    }

}
