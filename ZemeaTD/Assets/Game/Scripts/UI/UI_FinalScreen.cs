using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
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
        print(GameManager.Get().winGame);
        if (GameManager.Get().winGame)
        {
            victoryPanel.SetActive(true);
            FocusOnButton(victoryPanel);
        }
        else
        {
            defeatPanel.SetActive(true);
            FocusOnButton(defeatPanel);
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

    IEnumerator FocusOnButton(GameObject currentPanel)
    {
        yield return new WaitForEndOfFrame();
        GameObject b = currentPanel.GetComponentInChildren<Button>().gameObject;
        b.GetComponent<Button>().Select();        
        EventSystem.current.SetSelectedGameObject(b,null);
    }
}
