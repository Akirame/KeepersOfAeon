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
    private AudioSource aSource;

    private void Start()
    {
        aSource = GetComponent<AudioSource>();
        versionText.text = "v" + Application.version;
        currentPanel = mainMenuCanvas;
        StartCoroutine(FocusOnButton());
    }
    public void PlayButtonPressed() {
        Orbs.SetActive(true);
        currentPanel = Orbs;
        StartCoroutine(FocusOnButton());
    }
    public void HowToPlayButtonPressed() {
        howToPlayCanvas.SetActive(true);
        currentPanel = howToPlayCanvas;
        StartCoroutine(FocusOnButton());
    }
    public void CreditsButtonpressed() {
        creditsCanvas.SetActive(true);
        currentPanel = creditsCanvas;
        StartCoroutine(FocusOnButton());
    }
    public void ExitButtonPressed() {
        Application.Quit();
    }
    public void BackButtonPressed() {
        currentPanel.SetActive(false);
        currentPanel = mainMenuCanvas;
        StartCoroutine(FocusOnButton());
    }
    public void NextOrbsButtonPressed()
    {
        Orbs.SetActive(false);
        Objectives.SetActive(true);
        currentPanel = Objectives;
        StartCoroutine(FocusOnButton());

    }
    public void NextButtonPressed()
    {
        LoaderManager.Get().LoadScene("SampleScene");
    }
    IEnumerator FocusOnButton()
    {
        yield return new WaitForEndOfFrame();
        GameObject b = currentPanel.GetComponentInChildren<Button>().gameObject;
        b.GetComponent<Button>().Select();
        EventSystem.current.SetSelectedGameObject(b,null);
    }
}
