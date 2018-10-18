using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{

    public delegate void PlayerDetectorAction(PlayerDetector p, ElementalOrb e);
    public static PlayerDetectorAction ReturnOrb;
    public ElementalOrb orbToPick;
    public ElementalOrb currentOrb;
    public Transform orbPosition;
    public InputControl playerInput;
    private List<ElementalOrb> elementals;
    private float forceToThrow = 25;
    private bool picked = false;
    private int maxForceThrow = 25;
    private float forceCalculation = 0;


    private void Start()
    {
        elementals = new List<ElementalOrb>();
    }

    private void Update()
    {
        if(!picked)
        UpdateOrbToPick();
        if (orbToPick)
        {
            if (orbToPick.pickedUp && Input.GetKeyDown(playerInput.primaryButton))
            {                
                ConsumeOrb();                
                picked = false;
            }
            else
                if (orbToPick.pickedUp && Input.GetKey(playerInput.secondaryButton))
            {
                CalculateThrowForce();                
            }
            else
            if (!orbToPick.pickedUp && Input.GetKeyDown(playerInput.primaryButton))
            {                
                PickUpOrb();                
                picked = true;
            }
            if (forceCalculation > 0)
            {
                if (Input.GetKeyUp(playerInput.secondaryButton))
                {
                    ThrowOrb();
                    picked = false;
                    forceCalculation = 0;
                }
            }
        }
    }

    private void UpdateOrbToPick()
    {
        if (elementals.Count != 0)
        {
            orbToPick = elementals.Last();
            Debug.Log(elementals.Last());
        }
        else
            orbToPick = null;
    }

    private void CalculateThrowForce()
    {
        forceCalculation += forceToThrow * Time.deltaTime;
        if (forceCalculation >= maxForceThrow)
        {
            forceCalculation = maxForceThrow;
        }
    }

    private void ThrowOrb()
    {
        orbToPick.Throw(forceCalculation);
    }
    private void ConsumeOrb()
    {
        if (currentOrb)
            ReturnOrb(this, currentOrb);
        currentOrb = orbToPick.Consume();
    }
    private void PickUpOrb()
    {
        orbToPick.AttachToPlayer(transform.parent.gameObject, orbPosition);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ElementalOrb")
        {
            elementals.Add(collision.gameObject.GetComponent<ElementalOrb>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ElementalOrb")
        {
            elementals.Remove(collision.gameObject.GetComponent<ElementalOrb>());
        }
    }

}
