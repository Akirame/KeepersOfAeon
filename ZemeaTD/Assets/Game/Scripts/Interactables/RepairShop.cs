using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairShop : MonoBehaviour {
    public bool repairOn = false;
    public Rampart shield;
    public ParticleSystem particles;

    private void Update() {
        if(repairOn) {
            shield.RepairRampart();
            if(!particles.isEmitting)
                particles.Play();
        }
        else
        {
            if(particles.isEmitting)
                particles.Stop();
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
