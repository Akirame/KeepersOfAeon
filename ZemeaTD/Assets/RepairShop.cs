using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairShop : MonoBehaviour {
    public bool repairOn = false;
    public Rampart[] ramparts;
    private void Update() {
        if(repairOn) {
            foreach(Rampart r in ramparts) {
                r.RepairRampart();
                print("holi");
            }
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
