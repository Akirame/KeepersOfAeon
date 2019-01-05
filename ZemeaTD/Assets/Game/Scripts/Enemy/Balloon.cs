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
    public GameObject player;

    private void Start()
    {
        //balloonElement = (ColorAttribute.COLOR_TYPE)UnityEngine.Random.Range(0, 3);
        balloonElement = ColorAttribute.COLOR_TYPE.GREEN;
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = sprites[0];
        //switch(balloonElement)
        //{
        //    case ColorAttribute.COLOR_TYPE.GREEN:
        //        sr.sprite = sprites[0];
        //        break;
        //    case ColorAttribute.COLOR_TYPE.MAGENTA:
        //        sr.sprite = sprites[1];
        //        break;
        //    case ColorAttribute.COLOR_TYPE.ORANGE:
        //        sr.sprite = sprites[2];
        //        break;
        //    case ColorAttribute.COLOR_TYPE.YELLOW:
        //        sr.sprite = sprites[4];
        //        break;
        //}
    }
    public void TakeDamage(ColorAttribute.COLOR_TYPE element, GameObject _player)
    {
        player = _player;
        if(element == balloonElement)
            onDeath(this);
    }
}
