using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_FinalScreen : MonoBehaviour {

    public GameObject victoryPanel;
    public GameObject defeatPanel;

    private void Awake()
    {
        victoryPanel.SetActive(false);
        defeatPanel.SetActive(false);
    }

    private void Start()
    {
        if (GameManager.Get().winGame)
        {
            victoryPanel.SetActive(true);
        }
        else
        {
            defeatPanel.SetActive(true);
        }
    }

    public void ExitPressed()
    {
        LoaderManager.Get().LoadSceneQuick("MainMenu");
    }

    public void RestartPressed()
    {
        LoaderManager.Get().LoadSceneQuick("SampleScene");
    }
}
