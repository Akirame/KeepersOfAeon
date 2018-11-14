using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    #region Singleton
    public static GameManager instance;
    private LoaderManager loader;
    public bool winGame = false;

    public static GameManager Get()
    {
        return instance;
    }
    public virtual void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion

    public PlayerLevel player1Level;
    public PlayerLevel player2Level;
    private bool tutorialDone = false;

    private void Start()
    {
        loader = LoaderManager.Get();
        Tower.TowerDestroyed += GameOver;
        LightBehaviour.LightFinished += GameWon;
        if (DebugScreen.GetInstance())
        {
            DebugScreen.GetInstance().AddButton("ResetGameScene", ResetGame);
            DebugScreen.GetInstance().AddButton("Add Players Level", LevelUpPlayers);
            DebugScreen.GetInstance().AddButton("Substract Players Level", LevelDownPlayers);
        }
    }

    private void ResetGame()
    {
        loader.LoadSceneQuick("SampleScene");
        winGame = false;
        tutorialDone = true;
    }
    private void GameWon(LightBehaviour l)
    {
        loader.LoadScene("FinalScreen");
        winGame = true;
    }

    private void LevelUpPlayers()
    {
        player1Level.LevelUpPlayer();
        player2Level.LevelUpPlayer();
    }

    private void LevelDownPlayers()
    {
        player1Level.LevelDownPlayer();
        player2Level.LevelDownPlayer();
    }

    public void ToMainMenu()
    {
        loader.LoadSceneQuick("MainMenu");
    }

    private void GameOver(Tower t)
    {
        loader.LoadSceneQuick("FinalScreen");
        winGame = false;
    }
}
