using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    public delegate void BalloonActions(Balloon b);
    public BalloonActions onDeath;
    public ColorAttribute.COLOR_TYPE balloonElement;
    public SpriteRenderer sr;
    public Sprite[] sprites;

    private void Start()
    {
        balloonElement = (ColorAttribute.COLOR_TYPE)UnityEngine.Random.Range(0, 3);
        sr = GetComponent<SpriteRenderer>();
        switch(balloonElement)
        {
            case ColorAttribute.COLOR_TYPE.GREEN:
                sr.sprite = sprites[1];
                break;
            case ColorAttribute.COLOR_TYPE.MAGENTA:
                sr.sprite = sprites[2];
                break;
            case ColorAttribute.COLOR_TYPE.ORANGE:
                sr.sprite = sprites[3];
                break;
            case ColorAttribute.COLOR_TYPE.YELLOW:
                sr.sprite = sprites[0];
                break;
        }
    }
    public void TakeDamage(ColorAttribute.COLOR_TYPE element)
    {
        if(element == balloonElement)
            onDeath(this);
    }
}
