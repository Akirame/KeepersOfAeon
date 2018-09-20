using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalArcanum : MonoBehaviour {

    public GameObject[] orbs;
    public ElementalOrb[] orbsSprites;
    private GameObject player;
    public ElementalOrb orbSelected;
    private InputControl inputPlayer;
    private int orbIndex = 0;
    public GameObject orbPivot;
    private int rotation = 120;

    private void Start()
    {
        orbSelected = orbs[orbIndex].GetComponent<ElementalOrb>();
    }

    private void Update()
    {
        if (player)
        {
            if (Input.GetKeyDown(inputPlayer.primaryButton))
            {
                NextOrb();                
            }
            if (Input.GetKeyDown(inputPlayer.secondaryButton))
            {
                ChooseOrb();
            }
        }
    }

    private void ChooseOrb()
    {
        AttackBehaviour attackPlayer = player.GetComponent<AttackBehaviour>();
        if (!orbsSprites[(int)orbSelected.elementType].used)
        {
            if (!attackPlayer.currentElement)
            {
                orbsSprites[(int)orbSelected.elementType].SetActive(false);
                attackPlayer.currentElement = orbSelected;
            }
            if (attackPlayer.currentElement != orbSelected)
            {
                orbsSprites[(int)attackPlayer.currentElement.elementType].SetActive(true);
                attackPlayer.currentElement = orbSelected;
                orbsSprites[(int)orbSelected.elementType].SetActive(false);
            }
        }
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
        orbPivot.transform.eulerAngles = new Vector3(0, 0, rotation * orbIndex);
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
