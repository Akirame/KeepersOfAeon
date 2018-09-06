using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour {

    public enum EXIT_DIRECTION {RIGHT, LEFT}
    public GameObject player;
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
            player.GetComponent<CharacterController2D>().SetAttackMode(false);
            player = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !isActivated)
        {
            isActivated = true;
            collision.GetComponent<CharacterController2D>().SetAttackMode(true);
            player = collision.gameObject;
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
