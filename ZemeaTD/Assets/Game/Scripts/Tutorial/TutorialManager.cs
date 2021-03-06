﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour {

    public GameObject[] tutorials;
    public float timeBetweenTutorials;
    private float timer;
    private int tutorialIdx = 0;
    private int selectCount = 0;

    // Use this for initialization
    void Start () {
        if (!GameManager.Get().tutorialDone)
            ActivateFirstTutorial();
        else
            TutorialEnd();
    }

    private void ActivateFirstTutorial()
    {
        for (int i = 0; i < tutorials.Length; i++)
        {
            tutorials[i].SetActive(false);
        }
        tutorials[tutorialIdx].SetActive(true);
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetButtonDown("Back"))
        {
            selectCount++;
        }
        if (tutorialIdx == 4)
        {
            timer += Time.deltaTime;
            if (timer >= timeBetweenTutorials)
            {
                ChangeToNextTutorial();
            }
        }
        if (Input.GetKeyDown(KeyCode.F5) || selectCount > 5)
        {
            TutorialEnd();
        }
    }

    public void TutorialEnd()
    {
        foreach (GameObject item in tutorials)
        {
            Destroy(item);
        }
        GameManager.Get().tutorialDone = true;
        Destroy(gameObject);
    }

    public void ChangeToNextTutorial()
    {
        tutorialIdx++;
        if (tutorialIdx < tutorials.Length)
        {
            tutorials[tutorialIdx - 1].SetActive(false);
            tutorials[tutorialIdx].SetActive(true);
        }
    }
}
