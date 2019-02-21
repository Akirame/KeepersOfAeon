using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour  
{
    public GameObject mainMenuCanvas;
    public GameObject creditsCanvas;
    public Text versionText;
    public GameObject currentPanel;
    private AudioSource aSource;
    private Animator anim;

    private void Start()
    {
        aSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        versionText.text = "v" + Application.version;
        currentPanel = mainMenuCanvas;
        StartCoroutine(FocusOnButton());
    }
    public void PlayButtonPressed() {
        anim.SetTrigger("playButton");
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

    IEnumerator FocusOnButton()
    {
        yield return new WaitForEndOfFrame();
        GameObject b = currentPanel.GetComponentInChildren<Button>().gameObject;
        b.GetComponent<Button>().Select();
        EventSystem.current.SetSelectedGameObject(b,null);
    }

    public void GoToColorSelectionScreen()
    {
        SceneManager.LoadScene("ColorSelectionScreen");
    }

}
