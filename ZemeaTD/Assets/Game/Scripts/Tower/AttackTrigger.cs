using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour {

    public enum FACING_DIRECTION {RIGHT, LEFT}
    public FACING_DIRECTION facingDirection;
    public GameObject player;
    public InputControl inputPlayer;
    public bool isUsed = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !isUsed)
        {
            AddPlayerReferences(collision);
        }
    }

    private void AddPlayerReferences(Collider2D col)
    {
        player = col.gameObject;
        inputPlayer = player.GetComponent<InputControl>();
    }

    private void Update()
    {
        if (player)
        {
            if (Input.GetButtonDown(inputPlayer.toggleAttackButton))
            {
                if (!isUsed)
                {
                    isUsed = true;
                    player.GetComponent<CharacterController2D>().SetAttackMode(true);
                    FacingClamp();
                }
                else
                {
                    isUsed = false;
                    player.GetComponent<CharacterController2D>().SetAttackMode(false);
                }
            }
        }
    }

    private void FacingClamp()
    {
        switch (facingDirection)
        {
            case FACING_DIRECTION.RIGHT:
                player.GetComponent<CharacterController2D>().SetFacingRight(true);
                break;
            case FACING_DIRECTION.LEFT:
                player.GetComponent<CharacterController2D>().SetFacingRight(false);
                break;
            default:
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            RemovePlayerReferences();
        }
    }

    private void RemovePlayerReferences()
    {
        player.GetComponent<CharacterController2D>().SetAttackMode(false);
        player = null;
        isUsed = false;
        inputPlayer = null;
    }
}
