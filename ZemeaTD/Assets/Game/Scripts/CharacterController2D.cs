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

    public float speed = 10f;
    private bool onDoor = false;
    private bool canMove = true;
    private Door lastDoorTouched;
    private bool canJump = false;
    public LayerMask floorLayer;

    private void Start()
    {
        lastDoorTouched = null;
    }
    private void Update()
    {
        if(canMove)
            Movement();
        GroundControl();
        JumpBehaviour();
    }

    private void Movement()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            GetComponent<SpriteRenderer>().flipX = true;
            transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            GetComponent<SpriteRenderer>().flipX = false;
            transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
        }
        if (onDoor)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                lastDoorTouched.GotoObjective(this.gameObject);
            }
        }
    }

    public void GroundControl()
    {
        Vector2 minSpriteSize = new Vector2(0, -GetComponent<SpriteRenderer>().size.y / 2);
        Vector2 floorContact = (Vector2)transform.position + minSpriteSize;
        if (Physics2D.Raycast(floorContact, Vector2.down, 0.1f, floorLayer))
            canJump = true;
        else
            canJump = false;
    }

    public void JumpBehaviour()
    {
        if (canJump && Input.GetKeyDown(KeyCode.UpArrow))
        {
            canJump = false;
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 5);
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
}
