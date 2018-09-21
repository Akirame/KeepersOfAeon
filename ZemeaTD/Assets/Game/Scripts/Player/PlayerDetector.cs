using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour {

    public delegate void PlayerDetectorAction(PlayerDetector p, ElementalOrb e);
    public static PlayerDetectorAction ReturnOrb;
    public ElementalOrb orb;
    public ElementalOrb secondaryOrb;
    public Transform orbPosition;
    public InputControl playerInput;

    private void Update() {
        if(orb) {
            if(!orb.pickedUp && Input.GetKeyDown(playerInput.primaryButton)) {
                PickUpOrb();
            }
            else
                if(orb.pickedUp && Input.GetKeyDown(playerInput.secondaryButton)) {
                ThrowOrb();
            }
            else
            if(orb.pickedUp && Input.GetKeyDown(playerInput.primaryButton)) {
                ConsumeOrb();
            }
        }
    }

    private void ThrowOrb() {
        orb.Throw();
    }
    private void ConsumeOrb() {

        if(secondaryOrb)
            ReturnOrb(this, secondaryOrb);

        secondaryOrb = orb.Consume();
    }
    private void PickUpOrb() {
        orb.AttachToPlayer(transform.parent.gameObject, orbPosition);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "ElementalOrb") {
            orb = collision.gameObject.GetComponent<ElementalOrb>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if(collision.gameObject.tag == "ElementalOrb") {
            orb = null;
        }
    }

}
