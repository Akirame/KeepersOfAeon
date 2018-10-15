using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class PlayerDetector :MonoBehaviour
{

    public delegate void PlayerDetectorAction(PlayerDetector p, ElementalOrb e);
    public static PlayerDetectorAction ReturnOrb;
    public ElementalOrb orbToPick;
    public ElementalOrb currentOrb;
    public Transform orbPosition;
    public InputControl playerInput;
    private List<ElementalOrb> elementals;

    private void Start()
    {
        elementals = new List<ElementalOrb>();
    }

    private void Update()
    {
        UpdateOrbToPick();
        if(orbToPick)
        {
            if(!orbToPick.pickedUp && Input.GetKeyDown(playerInput.primaryButton))
            {
                PickUpOrb();
            }
            else
                if(orbToPick.pickedUp && Input.GetKeyDown(playerInput.secondaryButton))
            {
                ThrowOrb();
            }
            else
            if(orbToPick.pickedUp && Input.GetKeyDown(playerInput.primaryButton))
            {
                ConsumeOrb();
            }
        }
    }

    private void UpdateOrbToPick()
    {
        if(elementals.Count != 0)
            orbToPick = elementals.Last();
        else
            orbToPick = null;
    }

    private void ThrowOrb()
    {
        orbToPick.Throw();
    }
    private void ConsumeOrb()
    {
        if(currentOrb)
            ReturnOrb(this, currentOrb);
        currentOrb = orbToPick.Consume();
    }
    private void PickUpOrb()
    {
        orbToPick.AttachToPlayer(transform.parent.gameObject, orbPosition);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "ElementalOrb")
        {            
              elementals.Add(collision.gameObject.GetComponent<ElementalOrb>());            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "ElementalOrb")
        {
            elementals.Remove(collision.gameObject.GetComponent<ElementalOrb>());            
        }
    }

}
