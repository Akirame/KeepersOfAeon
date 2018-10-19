using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour  
{
    public GameObject mainMenuCanvas;
    public GameObject howToPlayCanvas;
    public GameObject creditsCanvas;
    public GameObject Objectives;
    public Text versionText;

    private void Start()
    {
        versionText.text = "Version " + Application.version;
    }

    public void PlayButtonPressed() {
        Objectives.SetActive(true);
        SetCanvas(false, false, false);
    }
    public void HowToPlayButtonPressed() {
        SetCanvas(false, true, false);
    }
    public void CreditsButtonpressed() {
        SetCanvas(false, false, true);
    }
    public void ExitButtonPressed() {
        Application.Quit();
    }
    public void BackButtonPressed() {
        SetCanvas(true, false, false);
    }
    public void NextButtonPressed()
    {
        LoaderManager.Get().LoadScene("SampleScene");
    }
    private void SetCanvas(bool mainMenu, bool howto, bool credits) {
        mainMenuCanvas.SetActive(mainMenu);
        howToPlayCanvas.SetActive(howto);
        creditsCanvas.SetActive(credits);
    }
}
