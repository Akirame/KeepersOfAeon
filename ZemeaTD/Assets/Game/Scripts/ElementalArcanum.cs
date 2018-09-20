using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalArcanum : MonoBehaviour {

    public GameObject[] orbs;
    private GameObject player;
    public ElementalOrb orbSelected;
    private InputControl inputPlayer;
    private int orbIndex = 0;
    public GameObject orbPivot;

    private void Start()
    {
        orbSelected = orbs[orbIndex].GetComponent<ElementalOrb>();
    }

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
        player.GetComponent<AttackBehaviour>().ChangeElement(orbSelected);
    }

    private void NextOrb()
    {
        orbIndex++;
        if (orbIndex > orbs.Length - 1)
        {
            orbIndex = 0;
        }
        orbSelected = orbs[orbIndex].GetComponent<ElementalOrb>();
        ChangeOrbsOrder();
    }

    private void ChangeOrbsOrder()
    {
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
