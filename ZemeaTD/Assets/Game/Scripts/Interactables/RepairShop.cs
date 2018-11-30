using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairShop : MonoBehaviour {
    public bool repairOn = false;
    public Rampart shield;
    public ParticleSystem particles;
    public GameObject repairIcon;
    private List<GameObject> playerList;

    private void Start()
    {
        playerList = new List<GameObject>();
    }
    private void Update() {            
        repairOn = (playerList.Count > 0) ? true : false;
        if(repairOn) {
            shield.RepairRampart(playerList.Count);
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
            playerList.Add(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player")
            playerList.Remove(collision.gameObject);
    }
}
