using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIJoystickPlugScreen : MonoBehaviour {
    private static UIJoystickPlugScreen instance;
    public static UIJoystickPlugScreen Get()
    {
        return instance;
    }

    public Canvas PlugJoystickCanvas;
    private bool paused = false;
    private float timeScale;
    private string[] names;
    private int conta = 0;
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
            Destroy(this.gameObject);
    }
    private void Start()
    {        
        timeScale = Time.timeScale;        
        StartCoroutine(CheckJoysticks());
    }    
    IEnumerator CheckJoysticks()
    {
        while (true)
        {
            names = Input.GetJoystickNames();
            conta = 0;            
                for(int i = 0; i < names.Length; i++)
                {
                    Debug.Log(names[i].Length);
                    if(names[i].Length > 0)
                        conta++;
                    else
                        conta = 0;
                }            
                if(conta != 2)
                    PauseGame();            
            else if(paused)
                UnPauseGame();
            yield return new WaitForSecondsRealtime(1);
        }
    }
    private void PauseGame()
    {
        Time.timeScale = 0;
        paused = true;
        PlugJoystickCanvas.gameObject.SetActive(true);
    }
    private void UnPauseGame()
    {
        Time.timeScale = timeScale;
        paused = false;
        PlugJoystickCanvas.gameObject.SetActive(false);
    }
}
