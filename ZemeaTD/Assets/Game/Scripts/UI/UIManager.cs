using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    public GameObject UIGame;
    public GameObject UIPause;
    public GameObject ControlPanel;
    private bool gamePaused = false;
    private float timeScale;

    private void Start() {
        timeScale = Time.timeScale;
        SetAllCanvas(true, false);
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape) || (Input.GetButtonDown("Pause")))
            if (!gamePaused)
                PauseGame();
            else
                UnpauseGame();
    }
    private void PauseGame() {
        Time.timeScale = 0f;
        SetAllCanvas(false, true);
        gamePaused = true;
        StartCoroutine(FocusOnButton(UIPause));
    }
    public void UnpauseGame() {
        Time.timeScale = timeScale;
        SetAllCanvas(true, false);
        gamePaused = false;
        ControlPanel.SetActive(false);
    }

    public void ReturnToMenu()
    {
        Time.timeScale = timeScale;
        GameManager.Get().ToMainMenu();
    }

    private void SetAllCanvas(bool game, bool pause) {
        UIGame.SetActive(game);
        UIPause.SetActive(pause);
    }

    public void ButtonControlPressed()
    {
        ControlPanel.SetActive(true);
        StartCoroutine(FocusOnButton(ControlPanel));
    }
    public void ButtonBackControlPressed()
    {
        ControlPanel.SetActive(false);
        StartCoroutine(FocusOnButton(UIPause));
    }
    IEnumerator FocusOnButton(GameObject currentPanel)
    {        
        yield return new WaitForEndOfFrame();        
        GameObject b = currentPanel.GetComponentInChildren<Button>().gameObject;
        b.GetComponent<Button>().Select();
        EventSystem.current.SetSelectedGameObject(b,null);
    }
}
