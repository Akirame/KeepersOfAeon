using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBehaviour : MonoBehaviour
{

    public delegate void LightAction(LightBehaviour l);
    public static LightAction LightFinished;

    private bool lightOn = false;    
    public float lightValue = 0f;
    public float lightPerSecond = 0.3f;
    public ParticleSystem particles;

    public UILight LightUICanvas;

    private void Start()
    {
        if (DebugScreen.GetInstance())
        {
            DebugScreen.GetInstance().AddButton("Win", LightOn);
        }
    }

    private void Update() {
        if (lightOn)
            ActivateLight();
        else
        if (particles.isEmitting)
            particles.Stop();
        if (lightValue >= 100)
            LightFinished(this);
    }
    private void ActivateLight() {
        if(lightValue < 100) {
            if (!particles.isEmitting)
                particles.Play();
            lightValue += lightPerSecond * Time.deltaTime;
            LightUICanvas.UpdateTexts((int)lightValue);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player")
            lightOn = true;
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player")
            lightOn = false;
    }

    public void LightOn()
    {
        lightValue = 200;
    }

}
