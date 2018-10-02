using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {
    public GameObject UIGame;
    public GameObject UIPause;
    private float timeScale;

    private void Start() {
        timeScale = Time.timeScale;
        SetAllCanvas(true, false);        
    }
    private void Update() {
        if(Input.GetKeyDown(KeyCode.P))
            PauseGame();
    }
    private void PauseGame() {
        Time.timeScale = 0f;
        SetAllCanvas(false, true);
    }
    public void UnpauseGame() {
        Time.timeScale = timeScale;
        SetAllCanvas(true, false);
    }
    private void SetAllCanvas(bool game, bool pause) {
        UIGame.SetActive(game);
        UIPause.SetActive(pause);
    }
}
