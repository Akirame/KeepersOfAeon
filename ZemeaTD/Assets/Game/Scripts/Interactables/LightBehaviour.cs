using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBehaviour :MonoBehaviour
{

    public delegate void LightAction(LightBehaviour l);
    public static LightAction LightFinished;

    private bool lightOn = false;
    private bool inTutorial = true;
    private List<GameObject> playerList;
    public float lightValue = 0f;
    public float lightPerSecond = 0.3f;
    public float maxLight = 100;
    public ParticleSystem particles;
    public UILight LightUICanvas;

    private void Start()
    {
        if(DebugScreen.GetInstance())
        {
            DebugScreen.GetInstance().AddButton("Win", CompleteLight);
        }
        LightUICanvas.maxLightValue = maxLight;
        playerList = new List<GameObject>();
    }

    private void Update()
    {
        lightOn = (playerList.Count > 0) ? true : false;
        CheckOnTutorial();
        if(lightOn && !inTutorial)
            ActivateLight();
        else
        if(particles.isEmitting)
            particles.Stop();
        if(lightValue >= maxLight)
            LightFinished(this);
    }

    private void CheckOnTutorial()
    {
        if(GameManager.Get().tutorialDone && inTutorial)
        {
            inTutorial = false;
        }
    }

    private void ActivateLight()
    {
        if(lightValue < maxLight)
        {
            if(!particles.isEmitting)
                particles.Play();
            lightValue += lightPerSecond * Time.deltaTime;
            LightUICanvas.UpdateTexts(lightValue);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
            playerList.Add(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
            playerList.Remove(collision.gameObject);
    }

    public void CompleteLight()
    {
        lightValue = maxLight * 2;
    }

    private void ActivateLightAfterTutorial(Target t)
    {
        inTutorial = false;
    }
}
