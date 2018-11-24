using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    public delegate void BalloonActions(Balloon b);
    public BalloonActions onDeath;
    public ElementalOrb.ELEMENT_TYPE balloonElement;
    public SpriteRenderer sr;
    public Sprite[] sprites;

    private void Start()
    {
        balloonElement = (ElementalOrb.ELEMENT_TYPE)UnityEngine.Random.Range(0, 3);
        sr = GetComponent<SpriteRenderer>();
        switch(balloonElement)
        {
            case ElementalOrb.ELEMENT_TYPE.WATER:
                sr.sprite = sprites[1];
                break;
            case ElementalOrb.ELEMENT_TYPE.FIRE:
                sr.sprite = sprites[2];
                break;
            case ElementalOrb.ELEMENT_TYPE.EARTH:
                sr.sprite = sprites[3];
                break;
            case ElementalOrb.ELEMENT_TYPE.NONE:
                sr.sprite = sprites[0];
                break;
        }
    }
    public void TakeDamage(ElementalOrb.ELEMENT_TYPE element)
    {
        if(element == balloonElement)
            onDeath(this);
    }
}
