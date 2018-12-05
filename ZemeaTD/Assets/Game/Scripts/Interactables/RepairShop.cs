using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairShop : MonoBehaviour {

    public bool repairOn = false;
    public Rampart shield;
    public ParticleSystem particles;
    public GameObject repairIcon;
    public AudioClip repairClip;
    private float audioVolume;
    private AudioSource aSource;
    private List<GameObject> playerList;
    private const float volumeDecreaseFactor = 0.4f;
    private float volumeDecrease;

    private void Start()
    {
        playerList = new List<GameObject>();
        aSource = GetComponent<AudioSource>();
        aSource.clip = repairClip;
        audioVolume = aSource.volume;
    }
    private void Update() {
        repairOn = (playerList.Count > 0) ? true : false;
        if(repairOn) {
            PlayRepairAudio();
            shield.RepairRampart(playerList.Count);
            if(!particles.isEmitting)
                particles.Play();
        }
        else
        {
            if (particles.isEmitting)
            {
                particles.Stop();
            }
            if (aSource.isPlaying)
            {
                if (aSource.volume <= 0)
                {
                    volumeDecrease = 0;
                    aSource.Stop();
                }
                else
                {
                    volumeDecrease += Time.deltaTime;
                    aSource.volume = Mathf.Lerp(0.3f, 0.0f, volumeDecrease * volumeDecreaseFactor);
                }

            }
        }
        repairIcon.SetActive(shield.shield < shield.maxShield);
    }

    private void PlayRepairAudio()
    {
        if (!aSource.isPlaying)
        {
            aSource.volume = audioVolume;
            aSource.Play();
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
