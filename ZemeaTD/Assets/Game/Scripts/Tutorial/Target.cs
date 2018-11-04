using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

    public WaveControl waveControl;
    public GameObject tutorial;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            if (!waveControl.gameStarted)
            {
                waveControl.gameStarted = true;
                TutorialEnd();
            }
        }
    }

    private void TutorialEnd()
    {
        foreach (Transform item in tutorial.GetComponentsInChildren<Transform>())
        {
            Destroy(item.gameObject);
        }
    }
}
