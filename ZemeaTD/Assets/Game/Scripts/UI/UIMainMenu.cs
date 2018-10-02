using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainMenu : MonoBehaviour  
{
    public GameObject mainMenuCanvas;
    public GameObject howToPlayCanvas;
    public GameObject creditsCanvas;

    public void PlayButtonPressed() {
        LoaderManager.Get().LoadScene("SampleScene");
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
    private void SetCanvas(bool mainMenu, bool howto, bool credits) {
        mainMenuCanvas.SetActive(mainMenu);
        howToPlayCanvas.SetActive(howto);
        creditsCanvas.SetActive(credits);
    }
}
