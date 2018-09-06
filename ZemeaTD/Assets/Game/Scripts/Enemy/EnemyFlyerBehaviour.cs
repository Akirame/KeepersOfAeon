using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyerBehaviour : MonoBehaviour
{
    private Rigidbody2D rig;
    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        rig.gravityScale = 0;
        transform.position = new Vector2(transform.position.x, 20);
    }
    private void Update()
    {
        
    }
}
