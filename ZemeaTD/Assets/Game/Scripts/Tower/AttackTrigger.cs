using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour {

    public enum EXIT_DIRECTION {RIGHT, LEFT}
    public GameObject player;
    public InputControl inputPlayer;
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
        if (Input.GetKeyDown(inputPlayer.moveLeft) && exitDirection == EXIT_DIRECTION.LEFT || Input.GetKeyDown(inputPlayer.moveRight) && exitDirection == EXIT_DIRECTION.RIGHT)
        {
            player.GetComponent<CharacterController2D>().SetAttackMode(false);
            player = null;
            inputPlayer = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !isActivated)
        {
            isActivated = true;
            player = collision.gameObject;
            inputPlayer = player.GetComponent<InputControl>();
            player.GetComponent<CharacterController2D>().SetAttackMode(true);
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
