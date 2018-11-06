using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

    public WaveControl waveControl;
    public GameObject tutorial;
    public SpriteRenderer rend;
    private int hitConta = 0;
    private Color[] colors = { Color.red, Color.white, Color.blue, Color.green };
    private void Start()
    {         
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            if(CheckColor(collision.GetComponent<ElementalProyectile>().element.elementType))
            {
                hitConta++;
                RandomColor();
            }
            if (!waveControl.gameStarted && hitConta > 3)
            {
                waveControl.gameStarted = true;
                TutorialEnd();
            }
        }
    }
    private bool CheckColor(ElementalOrb.ELEMENT_TYPE element)
    {
        switch(element)
        {
            case ElementalOrb.ELEMENT_TYPE.WATER:
                return (rend.color == Color.blue);                
            case ElementalOrb.ELEMENT_TYPE.FIRE:
                return (rend.color == Color.red);
            case ElementalOrb.ELEMENT_TYPE.EARTH:
                return (rend.color == Color.green);
            case ElementalOrb.ELEMENT_TYPE.NONE:
                return (rend.color == Color.white);
        }
        return false;
    }
    private void TutorialEnd()
    {
        foreach (Transform item in tutorial.GetComponentsInChildren<Transform>())
        {
            Destroy(item.gameObject);
        }
    }
    private void RandomColor()
    {
        Color c;
        do {
            c = colors[UnityEngine.Random.Range(0, colors.Length)];
        } while (rend.color == c);
        rend.color = c;
    }
}
