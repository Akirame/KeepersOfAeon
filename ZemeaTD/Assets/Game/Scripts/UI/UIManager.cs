using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
    public GameObject UIGame;
    public GameObject UIPause;
    private bool gamePaused = false;
    private float timeScale;

    private void Start() {
        timeScale = Time.timeScale;
        SetAllCanvas(true, false);        
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.P))
            if (!gamePaused)
                PauseGame();
            else
                UnpauseGame();                            
    }
    private void PauseGame() {
        Time.timeScale = 0f;
        SetAllCanvas(false, true);
        gamePaused = true;
    }
    public void UnpauseGame() {
        Time.timeScale = timeScale;
        SetAllCanvas(true, false);
        gamePaused = false;
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
}
