using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorAttribute :MonoBehaviour
{
    public enum COLOR_TYPE
    {
        GREEN = 0,
        MAGENTA,
        ORANGE,
        YELLOW,
        LAST
    };

    public COLOR_TYPE colorType;
    public COLOR_TYPE[] equipedColors;
    private int currentColor;
    private int conta = 0;
    public Color c;
    public Sprite[] colorSprites;
    private SpriteRenderer rend;
    private Animator anim;

    private void Start()
    {
        currentColor = (int)equipedColors[conta];
        rend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        colorType = (COLOR_TYPE)currentColor;
        anim.Play("Idle",0, UnityEngine.Random.Range(0, anim.GetCurrentAnimatorStateInfo(0).length));
        UpdateColor();        
    }

    public void CicleUpColor()
    {
        if (conta < 1)
            conta++;
        else
            conta = 0;
        currentColor = (int)equipedColors[conta];
        colorType = (COLOR_TYPE)currentColor;
        UpdateColor();
    }
    public void CicleDownColor()
    {
        if (conta > 0)
            conta--;
        else
            conta = 1;
        currentColor = (int)equipedColors[conta];
        colorType = (COLOR_TYPE)currentColor;
        UpdateColor();
    }

    public void UpdateColor()
    {
        switch(colorType)
        {
            case COLOR_TYPE.GREEN:
                c = Color.green;
                rend.sprite = colorSprites[0];
                break;
            case COLOR_TYPE.MAGENTA:
                c = Color.magenta;
                rend.sprite = colorSprites[1];
                break;
            case COLOR_TYPE.ORANGE:
                c = Color.red;
                rend.sprite = colorSprites[2];
                break;
            case COLOR_TYPE.YELLOW:
                c = Color.yellow;
                rend.sprite = colorSprites[3];
                break;
        }
    }
}
