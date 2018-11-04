using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairShop : MonoBehaviour {
    public bool repairOn = false;
    public Rampart shield;
    public ParticleSystem particles;
    public GameObject repairIcon;

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
        if (shield.shield < shield.maxShield)
        {
            repairIcon.SetActive(true);
        }
        else
        {
            repairIcon.SetActive(false);
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
