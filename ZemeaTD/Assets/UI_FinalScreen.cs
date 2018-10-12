using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_FinalScreen : MonoBehaviour {

    public void ContinuePressed()
    {
        LoaderManager.Get().LoadSceneQuick("MainMenu");
    }

    public void ExitPressed()
    {
        Application.Quit();
    }

    public void RestartPressed()
    {
        LoaderManager.Get().LoadSceneQuick("SampleScene");
    }
}
