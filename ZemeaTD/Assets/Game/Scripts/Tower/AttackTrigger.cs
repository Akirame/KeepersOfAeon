using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour {

    public enum FACING_DIRECTION {RIGHT, LEFT}
    public GameObject player;
    public InputControl inputPlayer;
    public FACING_DIRECTION facingDirection;
    private bool isActivated = false;

    private void Update()
    {
    }

    private void ExitControl()
    {
        if (Input.GetKeyDown(inputPlayer.openDoor))
        {
            ExitAttackInstance();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player = collision.gameObject;
            inputPlayer = player.GetComponent<InputControl>();
            if (!isActivated && Input.GetKeyDown(inputPlayer.openDoor))
            {
                isActivated = true;
                player.GetComponent<CharacterController2D>().SetAttackMode(true);
                FacingClamp();
            }
            else if (isActivated && Input.GetKeyDown(inputPlayer.openDoor))
            {
                ExitControl();
            }
        }
    }

    private void FacingClamp()
    {
        switch (facingDirection)
        {
            case FACING_DIRECTION.RIGHT:
                player.GetComponent<CharacterController2D>().SetFacing(true);
                break;
            case FACING_DIRECTION.LEFT:
                player.GetComponent<CharacterController2D>().SetFacing(false);
                break;
            default:
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            ExitAttackInstance();
        }
    }

    private void ExitAttackInstance()
    {
        isActivated = false;
        player.GetComponent<CharacterController2D>().SetAttackMode(false);
        player = null;
        inputPlayer = null;
    }
}
