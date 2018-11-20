using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalOrb :MonoBehaviour
{
    public enum ELEMENT_TYPE
    {
        WATER = 0,
        FIRE,
        EARTH,
        NONE,
        Count
    };

    public ELEMENT_TYPE elementType;
    public ELEMENT_TYPE[] elementsInPower;
    private int currentElement;
    private int conta = 0;
    public Color c;
    public Sprite[] orbSprites;
    private SpriteRenderer rend;
    private Animator anim;

    private void Start()
    {
        currentElement = (int)elementsInPower[conta];
        rend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        elementType = (ELEMENT_TYPE)currentElement;
        anim.Play("Idle",0, UnityEngine.Random.Range(0, anim.GetCurrentAnimatorStateInfo(0).length));
        UpdateOrb();        
    }

    public void CicleUpElement()
    {
        if (conta < 1)
            conta++;
        else
            conta = 0;
        currentElement = (int)elementsInPower[conta];
        elementType = (ELEMENT_TYPE)currentElement;
        UpdateOrb();
    }
    public void CicleDownElement()
    {
        if (conta > 0)
            conta--;
        else
            conta = 1;
        currentElement = (int)elementsInPower[conta];
        elementType = (ELEMENT_TYPE)currentElement;
        UpdateOrb();
    }

    public void UpdateOrb()
    {
        switch(elementType)
        {
            case ELEMENT_TYPE.EARTH:
                c = Color.green;
                rend.sprite = orbSprites[0];
                break;
            case ELEMENT_TYPE.FIRE:
                c = Color.red;
                rend.sprite = orbSprites[1];
                break;
            case ELEMENT_TYPE.WATER:
                c = Color.blue;
                rend.sprite = orbSprites[2];
                break;
            case ELEMENT_TYPE.NONE:
                c = Color.white;
                rend.sprite = orbSprites[3];
                break;
        }
    }
}
