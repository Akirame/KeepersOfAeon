using System;
using System.Collections;
using System.Collections.Generic;
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

    private void Start()
    {        
    }

    private void Update()
    {
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
            if(!orbToPick)
            orbToPick = collision.gameObject.GetComponent<ElementalOrb>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "ElementalOrb")
        {
            if(orbToPick)         
            if(orbToPick.gameObject == collision.gameObject)
            orbToPick = null;
        }
    }

}
