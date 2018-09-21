using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalOrb3 : MonoBehaviour {

    public enum ELEMENT_TYPE {WATER, FIRE, EARTH };
    public ELEMENT_TYPE elementType;
    public Sprite elementSprite;
    public Color c;
    public bool used = false;

    public void SetActive(bool val)
    {
        if (val)
        {
            GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
            used = false;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = new Color(0,0,0,125);
            used = true;
        }
    }
}
