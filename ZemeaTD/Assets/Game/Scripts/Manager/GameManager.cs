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

    private void Start()
    {
        Tower.TowerDestroyed += GameOver;
        LightBehaviour.LightFinished += GameWon;
    }

    private void GameWon(LightBehaviour l)
    {
        LoaderManager.Get().LoadScene("FinalScreen");
    }

    private void GameOver(Tower t)
    {
        LoaderManager.Get().LoadSceneQuick("FinalScreen");
    }
}
