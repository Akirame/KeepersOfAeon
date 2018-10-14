using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    #region Singleton
    public static GameManager instance;

    public static GameManager Get()
    {
        return instance;
    }
    public virtual void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion

    public PlayerLevel player1Level;
    public PlayerLevel player2Level;

    private void Start()
    {
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
        LoaderManager.Get().LoadSceneQuick("SampleScene");
    }
    private void GameWon(LightBehaviour l)
    {
        LoaderManager.Get().LoadScene("FinalScreen");
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

    private void GameOver(Tower t)
    {
        LoaderManager.Get().LoadSceneQuick("FinalScreen");
    }
}
