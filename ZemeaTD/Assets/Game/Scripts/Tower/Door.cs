using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject nextDoor;
    public Animator doorDisplay;

    /// <summary>
    /// Transporta al jugador hacia la otra puerta,haciendo un fade para que parezca que entra a la puerta
    /// </summary>
    /// <param name="go"></param>
    public void GoToNextDoor(GameObject player)
    {
        player.transform.position = nextDoor.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            doorDisplay.SetBool("Appear", true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            doorDisplay.SetBool("Appear", false);
        }
    }
}
