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
    public Sprite[] sprites;
    public bool canJump = true;
    public bool dobleJump = false;
    public bool lookingRight;
    public ParticleSystem psDust;
    private bool canMove = true;
    private bool onFloor = false;
    private Rigidbody2D rig;
    private AttackBehaviour attackComponent;
    private Animator anim;
    private Vector2 movement;
    private float changuiTime = 0.162f;
    private float timerChangui;
    private float timer = 0;
    private float chickenTimer = 7.6666666f;
    private bool chickenOn = false;
    public AudioClip chickenSound;
    private AudioSource aSource;

    private void Start()
    {
        var playerData2 = Instantiate(playerData);
        playerData = playerData2;
        rig = GetComponent<Rigidbody2D>();
        SetAttackMode(false);
        inputControl = GetComponent<InputControl>();
        attackComponent = GetComponent<AttackBehaviour>();
        anim = GetComponent<Animator>();
        lookingRight = true;
        psDust.Stop();
        Item.ChickenConsumed += TurnIntoChicken;
        aSource = GetComponent<AudioSource>();
        AudioManager.Get().AddSound(aSource);
    }
    private void OnDestroy()
    {
        Item.ChickenConsumed -= TurnIntoChicken;
    }
    private void Update()
    {
        StateController();
        StateBehaviour();
        if(chickenOn)
        {
            if(timer < chickenTimer)
            {
                timer += Time.deltaTime;
            }
            else
            {
                chickenOn = false;
                spriteRend.sprite = sprites[0];
                timer = 0;
            }
        }
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
        RaycastHit2D hit = Physics2D.Raycast(floorContact, Vector2.down, 3f, floorLayer);
        if (hit)
        {
            if(hit.transform.gameObject.tag == "OneWay" && (Input.GetAxis(inputControl.axisY) + Input.GetAxis(inputControl.axisYKey) > 0) && Input.GetButtonDown(inputControl.jump))
            {
                hit.transform.GetComponent<OneWayPlatform>().Deactivate(GetComponent<CapsuleCollider2D>());
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
        if (val && !chickenOn)
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
        float xScale = Mathf.Abs(transform.localScale.x);
        if (faceRight)
        {
            transform.localScale = new Vector2(xScale, transform.localScale.y);
        }
        else
        {
            transform.localScale = new Vector2(-xScale, transform.localScale.y);
        }
        lookingRight = faceRight;
    }
    public void TurnIntoChicken(Item item)
    {        
        spriteRend.sprite = sprites[1];
        if(currentState == PLAYER_STATES.Attack)
        SetAttackMode(false);
        chickenOn = true;
        aSource.PlayOneShot(chickenSound);
    }
}
