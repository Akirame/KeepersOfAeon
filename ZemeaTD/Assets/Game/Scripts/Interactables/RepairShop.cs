using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairShop : MonoBehaviour {
    public bool repairOn = false;
    public Rampart shield;

    private void Update() {
        if(repairOn) {
            shield.RepairRampart();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player")
            repairOn = true;
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player")
            repairOn = false;
    }
}
