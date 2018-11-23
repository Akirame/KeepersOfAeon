using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    public delegate void BalloonActions(Balloon b);
    public BalloonActions onDeath;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            onDeath(this);            
        }
    }
}
