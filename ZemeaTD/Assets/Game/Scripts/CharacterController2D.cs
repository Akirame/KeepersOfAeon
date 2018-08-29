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

    private void Start()
    {
        lastDoorTouched = null;
    }
    private void Update()
    {
        if(canMove)
        Movement();
    }

    private void Movement()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
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
