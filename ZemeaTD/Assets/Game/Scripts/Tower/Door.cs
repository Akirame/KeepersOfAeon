using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject nextDoor;
    public Animator doorDisplay;
    public GameObject player;

    private void Update()
    {
        if (player)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                GoToNextDoor();
            }
        }
    }

    /// <summary>
    /// Transporta al jugador hacia la otra puerta,haciendo un fade para que parezca que entra a la puerta
    /// </summary>
    /// <param name="go"></param>
    public void GoToNextDoor()
    {
        player.transform.position = nextDoor.transform.position;
        player.GetComponent<CharacterController2D>().SetCanMove(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            doorDisplay.SetBool("Appear", true);
            player = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            doorDisplay.SetBool("Appear", false);
            player.GetComponent<CharacterController2D>().SetCanMove(true);
            player = null;
        }
    }
}
