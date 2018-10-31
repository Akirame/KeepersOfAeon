using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour  
{
    public GameObject mainMenuCanvas;
    public GameObject howToPlayCanvas;
    public GameObject creditsCanvas;
    public GameObject Objectives;
    public GameObject Orbs;
    public Text versionText;
    public GameObject currentPanel;

    private void Start()
    {
        versionText.text = "Version " + Application.version;
    }

    public void PlayButtonPressed() {
        Orbs.SetActive(true);
        currentPanel = Orbs;
    }
    public void HowToPlayButtonPressed() {
        howToPlayCanvas.SetActive(true);
        currentPanel = howToPlayCanvas;
        EventSystem.current.SetSelectedGameObject(currentPanel.GetComponentInChildren<Button>().gameObject);
    }
    public void CreditsButtonpressed() {
        creditsCanvas.SetActive(true);
        currentPanel = creditsCanvas;
        EventSystem.current.SetSelectedGameObject(currentPanel.GetComponentInChildren<Button>().gameObject);
    }
    public void ExitButtonPressed() {
        Application.Quit();
    }
    public void BackButtonPressed() {
        currentPanel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(currentPanel.GetComponentInChildren<Button>().gameObject);
    }
    public void NextOrbsButtonPressed()
    {
        Orbs.SetActive(false);
        Objectives.SetActive(true);
        currentPanel = Objectives;
        EventSystem.current.SetSelectedGameObject(currentPanel.GetComponentInChildren<Button>().gameObject);
    }
    public void NextButtonPressed()
    {
        LoaderManager.Get().LoadScene("SampleScene");
    }
}
