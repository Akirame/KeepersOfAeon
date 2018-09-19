using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalArcanum : MonoBehaviour {

    public enum ORBS {WATER,FIRE,EARTH, LAST };
    public ORBS currentOrb = ORBS.WATER;
    private GameObject player;
    private InputControl inputPlayer;
    private int orbIndex = 0;
    public GameObject orbPivot;

    private void Update()
    {
        if (player)
        {
            if (Input.GetKeyDown(inputPlayer.openDoor))
            {
                NextOrb();
            }
            if (Input.GetKeyDown(inputPlayer.attack))
            {
                ChooseOrb();
            }
        }
    }

    private void ChooseOrb()
    {
        player.GetComponent<AttackBehaviour>().ChangeElement(currentOrb);
    }

    private void NextOrb()
    {
        orbIndex++;
        if (orbIndex != 0 && orbIndex % (int)ORBS.LAST == 0)
        {
            orbIndex = 0;
        }
        currentOrb = (ORBS)orbIndex;
        ChangeOrbsOrder();
    }

    private void ChangeOrbsOrder()
    {
        switch (currentOrb)
        {
            case ORBS.WATER:
                orbPivot.transform.eulerAngles = new Vector3(0, 0, 0);
                break;
            case ORBS.FIRE:
                orbPivot.transform.eulerAngles = new Vector3(0, 0, 128);
                break;
            case ORBS.EARTH:
                orbPivot.transform.eulerAngles = new Vector3(0, 0, 235);
                break;
            case ORBS.LAST:
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player = collision.gameObject;
            inputPlayer = player.GetComponent<InputControl>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player = null;
            inputPlayer = null;
        }
    }

}
