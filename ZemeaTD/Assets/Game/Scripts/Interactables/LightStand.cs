using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightStand : MonoBehaviour
{

    public delegate void LightAction(LightStand l);
    public static LightAction LightFinished;

    private List<GameObject> playerList;
    public float lightValue = 0f;
    public float lightPerSecond = 0.3f;
    public float maxLight = 100;
    public UILight LightUICanvas;

    private ParticleSystem particles;
    private ParticleSystem.EmissionModule em;
    private bool lightOn = false;
    private bool inTutorial = true;
    private Animator lightAnimator;
    private List<GameObject> lightStones;    
    private GameObject promptButton;
    private const int lightStonesCount = 10;
    private bool ultimateLightAvailable = false;
    private float maxUltimateLightCharge = 25f;
    private float currentUltimateLightCharge = 0f;
    private int currentLightsOn = 0;

    private void Start()
    {
        if(DebugScreen.GetInstance())
        {
            DebugScreen.GetInstance().AddButton("Win", CompleteLight);
        }
        LightUICanvas.maxLightValue = maxLight;
        playerList = new List<GameObject>();
        particles = GetComponentInChildren<ParticleSystem>();
        em = particles.emission;
        lightAnimator = GetComponent<Animator>();
        if (DebugScreen.GetInstance())
        {
            DebugScreen.GetInstance().AddButton("Activate Ultimate", DebugSetUltimateAvailable);
        }
        lightStones = new List<GameObject>();
        for(int i = 1; i <= lightStonesCount;i++)
        {
            lightStones.Add(transform.Find("LightStand").transform.Find("L"+ i).gameObject);
        }        
        promptButton = transform.Find("Canvas").Find("PromptButton").gameObject;
    }

    private void LateUpdate()
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

        if(ultimateLightAvailable)
        {
            foreach(GameObject player in playerList)
            {                
                if (Input.GetButtonDown(player.GetComponent<InputControl>().attackButton))
                {
                    lightAnimator.SetTrigger("OnUltimateLight");
                }
            }
        }
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
            em.rateOverTime = (int)lightValue;
            LightUICanvas.UpdateTexts(lightValue);
            
            if(!ultimateLightAvailable)
            {
                if(currentUltimateLightCharge < maxUltimateLightCharge)
                {
                    currentUltimateLightCharge += lightPerSecond * Time.deltaTime;
                    currentLightsOn = ((int)currentUltimateLightCharge * (int)lightStonesCount) / (int)maxUltimateLightCharge;

                    for(int i = 0; i < currentLightsOn;i++)
                    {
                        lightStones[i].GetComponent<SpriteRenderer>().color = new Color(1f,0f,1f,1f);
                    }
                }
                else
                {
                    SetUltimateAvailable(true);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerList.Add(collision.gameObject);
            if(ultimateLightAvailable)
                promptButton.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
            playerList.Remove(collision.gameObject);
        if(playerList.Count <= 0)
            promptButton.SetActive(false);
    }

    public void CompleteLight()
    {
        lightValue = maxLight * 2;
    }

    private void ActivateLightAfterTutorial(TutorialTarget t)
    {
        inTutorial = false;
    }

    public void SetUltimateAvailable(bool value)
    {
        ultimateLightAvailable = value;
        lightAnimator.SetBool("UltiAvailable", value);
        if(ultimateLightAvailable)
        {
            foreach(GameObject light in lightStones)
            {
                currentUltimateLightCharge = 0f;
                currentLightsOn = 0;
                light.GetComponent<SpriteRenderer>().color = new Color(1,1,1,1);
            }
        }

        promptButton.SetActive(playerList.Count > 0 ? value : false);
    }

    public void DebugSetUltimateAvailable()
    {
        ultimateLightAvailable = !ultimateLightAvailable;
        lightAnimator.SetBool("UltiAvailable", ultimateLightAvailable);
        promptButton.SetActive(playerList.Count > 0 ? ultimateLightAvailable : false);
    }

    public void OnUltimateLight()
    {
        SetUltimateAvailable(false);
        WaveControl.GetInstance().KillAllEnemiesOnScreen();
    }
}
