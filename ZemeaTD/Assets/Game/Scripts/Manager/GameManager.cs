using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    #endregion

    public List<string> p1Orbs;
    public List<string> p2Orbs;
    public bool onGameScene = false;
    public GameObject mage;
    public GameObject archer;
    public bool tutorialDone = false;
    public bool winGame = false;
    private LoaderManager loader;
    private AudioSource aSource;
    private bool isInitialized = false;

    private void Update()
    {
        if (!isInitialized && onGameScene)
        {
            Initialize();
        }
    }

    private void Initialize()
    {
        loader = LoaderManager.Get();
        Tower.TowerDestroyed += GameOver;
        LightStand.LightFinished += GameWon;
        if (DebugScreen.GetInstance())
        {
            DebugScreen.GetInstance().AddButton("ResetGameScene", ResetGame);
            DebugScreen.GetInstance().AddButton("Add Players Level", LevelUpPlayers);
        }
        aSource = GetComponent<AudioSource>();
        AudioManager.Get().AddMusic(aSource);
        mage = GameObject.Find("Mage");
        archer = GameObject.Find("Archer");
        GiveOrbsToPlayers();
        isInitialized = true;
    }

    private void GiveOrbsToPlayers()
    {
        if (p1Orbs.Capacity > 0 && p2Orbs.Capacity > 0)
        {
            mage.GetComponentInChildren<ColorAttribute>().EquipColors(p1Orbs);
            archer.GetComponentInChildren<ColorAttribute>().EquipColors(p2Orbs);
        }
    }

    private void ResetGame()
    {
        loader.LoadSceneQuick("SampleScene");
        winGame = false;
        tutorialDone = true;
    }

    private void GameWon(LightStand l)
    {
        winGame = true;
        loader.LoadScene("FinalScreen");
    }

    private void LevelUpPlayers()
    {
        mage.GetComponent<PlayerLevel>().LevelUpPlayer();
        archer.GetComponent<PlayerLevel>().LevelUpPlayer();
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

    internal void SetPlayerOrbs(List<string> orbsP1, List<string> orbsP2)
    {
        p1Orbs = orbsP1;
        p2Orbs = orbsP2;
    }
}
