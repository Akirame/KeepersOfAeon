using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour {

    public ElementalOrb2 orb;
    public Transform orbPosition;
    public InputControl playerInput;

    private void Update()
    {
        if (orb)
        {
            if (!orb.pickedUp && Input.GetKeyDown(playerInput.primaryButton))
            {
                PickUpOrb();
            }
            if (orb.pickedUp && Input.GetKeyDown(playerInput.secondaryButton))
            {
                ThrowOrb();
            }
        }
    }

    private void ThrowOrb()
    {
        orb.Throw();
    }

    private void PickUpOrb()
    {
        orb.AttachToPlayer(transform.parent.gameObject, orbPosition);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ElementalOrb")
        {
            orb = collision.gameObject.GetComponent<ElementalOrb2>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ElementalOrb")
        {
            orb = null;
        }
    }

}
