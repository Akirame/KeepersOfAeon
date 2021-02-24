using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    public enum PLAYER_STATES { Idle, Running, Jump, Attack};
    [Header("Player State")]
    public PLAYER_STATES currentState;

    [Header("Player Data")]
    public CharacterData playerData;
    public InputControl inputControl;

    [Header("Movement Vars")]
    public bool canJump = true;
    public bool doubleJump = false;
    public bool lookingRight;

    [Header("Misc Vars")]
    public Sprite[] sprites;
    public LayerMask floorLayer;
    public ParticleSystem psDust;
    public AudioClip chickenSound;
    public ParticleSystem chickenPs;

    private SpriteRenderer spriteRend;
    private bool canMove = true;
    public bool onFloor = false;
    private Rigidbody2D rig;
    private AttackBehaviour attackComponent;
    private Animator anim;
    private Vector2 movement;
    private float coyoteTime = 0.162f;
    private float coyoteTimer;
    private float timer = 0;
    private float chickenTimer = 7.6666666f;
    private bool chickenOn = false;
    private AudioSource aSource;

    private void Start()
    {
        var playerData2 = Instantiate(playerData);
        playerData = playerData2;
        rig = GetComponent<Rigidbody2D>();
        spriteRend = GetComponentInChildren<SpriteRenderer>();
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
                if(!onFloor){
                    currentState = PLAYER_STATES.Jump;
                }
                if (rig.velocity.x != 0)
                    currentState = PLAYER_STATES.Running;
                break;
            case PLAYER_STATES.Running:
                if (!onFloor){
                    currentState = PLAYER_STATES.Jump;
                }
                if (Mathf.Abs(rig.velocity.x) <= 0.1)
                    currentState = PLAYER_STATES.Idle;
                break;
            case PLAYER_STATES.Jump:
                if (onFloor){
                    currentState = PLAYER_STATES.Idle;
                }
                break;
            default:
                break;
        }
    }

    private void Movement()
    {
        float movSpeed = onFloor ? playerData.floorSpeed : playerData.airSpeed;
        movement.x = (Input.GetAxis(inputControl.axisH) + Input.GetAxis(inputControl.axisHKey));
        rig.velocity = new Vector2(movSpeed * Time.deltaTime * movement.x, rig.velocity.y);
        if(movement.magnitude > 0.1){
            SetFacingRight(movement.x > 0.1);
        }
        anim.SetFloat("axis", Mathf.Abs(movement.x));
    }

    public void GroundControl()
    {
        Vector2 minSpriteSize = new Vector2(0, -spriteRend.size.y / 2);
        Vector2 floorContact = (Vector2)transform.position + minSpriteSize;
        RaycastHit2D hit = Physics2D.Raycast(floorContact, Vector2.down, 3f, floorLayer);
        if (hit)
        {
            if(hit.transform.gameObject.tag == "OneWay" && (Input.GetAxis(inputControl.axisY) + (-Input.GetAxis(inputControl.axisYKey)) > 0) && Input.GetButtonDown(inputControl.jump))
            {
                hit.transform.GetComponent<OneWayPlatform>().Deactivate(GetComponent<CapsuleCollider2D>());
                onFloor = false;
            }
            else
            onFloor = true;
            canJump = onFloor;
            doubleJump = false;
            coyoteTimer = 0;
        }
        else
        {
            coyoteTime += Time.deltaTime;
            if (coyoteTimer > coyoteTime)
            {
                canJump = false;
            }
        }
    }

    public void JumpBehaviour()
    {
        if (Input.GetButtonDown(inputControl.jump) && canJump)
        {
            doubleJump = true;
            rig.velocity = new Vector2(rig.velocity.x, playerData.jumpForce);
            psDust.Play();
        }
        if (Input.GetButtonDown(inputControl.jump) && doubleJump)
        {
            canJump = false;
            doubleJump = false;
            rig.velocity = new Vector2(rig.velocity.x, playerData.jumpForce);
            psDust.Play();
        }
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
        transform.localScale = new Vector2(faceRight ? xScale : -xScale, transform.localScale.y);
        lookingRight = faceRight;
    }

    public void TurnIntoChicken(Item item)
    {
        chickenPs.Play();
        spriteRend.sprite = sprites[1];
        if(currentState == PLAYER_STATES.Attack)
        SetAttackMode(false);
        chickenOn = true;
        aSource.PlayOneShot(chickenSound);
    }
}
