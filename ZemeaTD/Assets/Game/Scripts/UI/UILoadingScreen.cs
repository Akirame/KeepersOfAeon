using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UILoadingScreen : MonoBehaviour
{
    public Text loadingText;
    public Text pressText;
    public bool onGame = false;

    public void SetVisible(bool show)
    {
        gameObject.SetActive(show);
    }

    public void Update()
    {
        int loadingVal = (int)(LoaderManager.Get().loadingProgress * 100);
        loadingText.text = "Loading " + loadingVal;
        if (LoaderManager.Get().loadingProgress >= 1 && !onGame)
            Destroy(this.gameObject);
    }

    public void SetOnGame()
    {
        onGame = true;
        loadingText.gameObject.SetActive(false);
        pressText.gameObject.SetActive(true);
    }

}