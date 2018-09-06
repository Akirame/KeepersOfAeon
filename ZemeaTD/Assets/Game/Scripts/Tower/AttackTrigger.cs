using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour {

    public enum EXIT_DIRECTION {RIGHT, LEFT}
    public GameObject player;
    public CharacterController2D characterPlayer;
    public EXIT_DIRECTION exitDirection;
    private bool isActivated = false;

    private void Update()
    {
        if (player)
        {
            ExitControl();
        }
    }

    private void ExitControl()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && exitDirection == EXIT_DIRECTION.LEFT || Input.GetKeyDown(KeyCode.RightArrow) && exitDirection == EXIT_DIRECTION.RIGHT)
        {
            characterPlayer.SetAttackMode(false);
            characterPlayer = null;
            player = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !isActivated)
        {
            isActivated = true;
            player = collision.gameObject;
            characterPlayer = player.GetComponent<CharacterController2D>();
            characterPlayer.SetAttackMode(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isActivated = false;
        }
    }

}
