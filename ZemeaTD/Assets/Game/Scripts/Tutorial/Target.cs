using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

    public WaveControl waveControl;
    public GameObject tutorial;
    private SpriteRenderer rend;
    public List<Sprite> colorSprites;
    private int hitConta = 0;
    private void Start()
    {
        rend = GetComponentInChildren<SpriteRenderer>();
        RandomColor();
    }
    private void Update()
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            if(CheckColor(collision.GetComponent<ElementalProyectile>().element))
            {
                hitConta++;
                RandomColor();
            }
            if (!waveControl.gameStarted && hitConta > 3)
            {
                tutorial.GetComponent<TutorialManager>().TutorialEnd();
            }
        }
    }

    private bool CheckColor(ElementalOrb.ELEMENT_TYPE element)
    {
        switch(element)
        {
            case ElementalOrb.ELEMENT_TYPE.WATER:
                return (rend.sprite == colorSprites[1]);
            case ElementalOrb.ELEMENT_TYPE.FIRE:
                return (rend.sprite == colorSprites[2]);
            case ElementalOrb.ELEMENT_TYPE.EARTH:
                return (rend.sprite == colorSprites[3]);
            case ElementalOrb.ELEMENT_TYPE.NONE:
                return (rend.sprite == colorSprites[0]);
        }
        return false;
    }

    private void RandomColor()
    {
        rend.sprite = colorSprites[UnityEngine.Random.Range(0, colorSprites.Count)];
    }
}
