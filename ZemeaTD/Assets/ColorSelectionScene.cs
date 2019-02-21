using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ColorSelectionScene : MonoBehaviour
{

    public Image[] colorOrbs;
    public Image[] orbsP1;
    public Image[] orbsP2;
    public Button startButton;
    public List<string> orbsNamesP1;
    public List<string> orbsNamesP2;
    private int p1Index = 0;
    private int p2Index = 0;
    private int orbIndex = 0;
    private bool orbsCompleted = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            ResetOrbs();
        }
        if (!orbsCompleted)
        {
            if (Input.GetButtonDown("P1_LTrigger") && p1Index < 2)
            {
                orbsP1[p1Index].sprite = colorOrbs[orbIndex].sprite;
                orbsNamesP1.Add(colorOrbs[orbIndex].sprite.name.ToString());
                p1Index++;
                ActivateNextOrb();
            }
            if (Input.GetButtonDown("P1_RTrigger") && p2Index < 2)
            {
                orbsP2[p2Index].sprite = colorOrbs[orbIndex].sprite;
                orbsNamesP2.Add(colorOrbs[orbIndex].sprite.name.ToString());
                p2Index++;
                ActivateNextOrb();
            }
        }
    }

    private void ActivateNextOrb()
    {
        colorOrbs[orbIndex].gameObject.SetActive(false);
        orbIndex++;
        if (orbIndex < colorOrbs.Length)
            colorOrbs[orbIndex].gameObject.SetActive(true);
        else
            orbsCompleted = !orbsCompleted;
        if (orbsCompleted)
        {
            startButton.interactable = !startButton.interactable;
        }
    }

    public void ResetOrbs()
    {
        p1Index = 0;
        p2Index = 0;
        orbIndex = 0;
        orbsCompleted = false;
        startButton.interactable = orbsCompleted;
        ClearPlayerOrbs();
        ResetColorOrbs();
    }

    private void ResetColorOrbs()
    {
        foreach (Image item in colorOrbs)
        {
            item.gameObject.SetActive(false);
        }
        colorOrbs[0].gameObject.SetActive(true);
    }

    private void ClearPlayerOrbs()
    {
        foreach (Image item in orbsP1)
            item.sprite = null;
        foreach (Image item in orbsP2)
            item.sprite = null;
        orbsNamesP1.Clear();
        orbsNamesP2.Clear();
    }

    public void OnStartButtonClicked()
    {
        GameManager.Get().SetPlayerOrbs(orbsNamesP1, orbsNamesP2);
        LoaderManager.Get().LoadScene("SampleScene");
    }

}
