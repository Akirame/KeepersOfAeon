using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        rend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        UpdateColor();
        anim.Play("Idle",0, UnityEngine.Random.Range(0, anim.GetCurrentAnimatorStateInfo(0).length));
    }

    public void CicleUpColor()
    {
        if (conta < 1)
            conta++;
        else
            conta = 0;
        UpdateColor();
    }

    public void CicleDownColor()
    {
        if (conta > 0)
            conta--;
        else
            conta = 1;
        UpdateColor();
    }

    public void UpdateColor()
    {
        currentColor = (int)equipedColors[conta];
        colorType = (COLOR_TYPE)currentColor;
        switch (colorType)
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

    public void EquipColors(List<string> playerOrbs)
    {
        equipedColors[0] = GetColorTypeByString(playerOrbs[0]);
        equipedColors[1] = GetColorTypeByString(playerOrbs[1]);
        UpdateColor();
    }

    public COLOR_TYPE GetColorTypeByString(string v)
    {
        COLOR_TYPE colorOrb = COLOR_TYPE.LAST;
        switch (v.ToUpper())
        {
            case "ORBE_GREEN":
                colorOrb = COLOR_TYPE.GREEN;
                break;
            case "ORBE_MAGENTA":
                colorOrb = COLOR_TYPE.MAGENTA;
                break;
            case "ORBE_ORANGE":
                colorOrb = COLOR_TYPE.ORANGE;
                break;
            case "ORBE_YELLOW":
                colorOrb = COLOR_TYPE.YELLOW;
                break;
            default:
                break;
        }
        return colorOrb;
    }

    public Color GetProyectileRawColor()
    {
        return c;
    }

}
