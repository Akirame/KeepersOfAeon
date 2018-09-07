using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyerBehaviour : MonoBehaviour
{
    private Rigidbody2D rig;    
    public float time = 3;
    public float maxY = 200;
    private float timer = 0;

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        rig.gravityScale = 0;
        transform.position = new Vector2(transform.position.x, 20);        
    }
    private void Update()
    {
        if (timer < time)
        {
            rig.velocity = new Vector2(rig.velocity.x, maxY * Time.deltaTime);
            timer += Time.deltaTime;
        }
        else
        {
            timer = 0;
            maxY *= -1;
        }
    }
}
