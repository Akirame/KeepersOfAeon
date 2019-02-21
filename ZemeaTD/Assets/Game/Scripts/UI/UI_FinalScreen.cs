using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class UI_FinalScreen : MonoBehaviour {

    public GameObject victoryPanel;
    public GameObject defeatPanel;
    public AudioClip victorySound;
    private AudioSource aSource;
    private Animator anim;

    private void Awake()
    {
        victoryPanel.SetActive(false);
        defeatPanel.SetActive(false);
        aSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        GameManager.Get().tutorialDone = true;
    }

    private void Start()
    {
        if (GameManager.Get().winGame)
        {
            victoryPanel.SetActive(true);
            FocusOnButton(victoryPanel);
            aSource.PlayOneShot(victorySound);
            anim.SetTrigger("victory");
        }
        else
        {
            defeatPanel.SetActive(true);
            FocusOnButton(defeatPanel);
            anim.SetTrigger("defeat");
        }
    }

    public void ExitPressed()
    {
        LoaderManager.Get().LoadSceneQuick("MainMenu");
    }

    public void RestartPressed()
    {
        LoaderManager.Get().LoadSceneQuick("ColorSelectionScreen");
    }

    IEnumerator FocusOnButton(GameObject currentPanel)
    {
        yield return new WaitForEndOfFrame();
        GameObject b = currentPanel.GetComponentInChildren<Button>().gameObject;
        b.GetComponent<Button>().Select();
        EventSystem.current.SetSelectedGameObject(b,null);
    }
}
