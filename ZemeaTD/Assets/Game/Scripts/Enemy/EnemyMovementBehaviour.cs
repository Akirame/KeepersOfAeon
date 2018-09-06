using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementBehaviour : MonoBehaviour
{
    private Rigidbody2D rig;
    public float speed = 200;
    public enum Dir { LEFT, RIGHT }
    private Dir direction;

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();        
    }
    private void Awake()
    {
        SetDirection();
    }
    private void Update()
    {
        {
            switch (direction)
            {
                case Dir.LEFT:
                    rig.velocity = new Vector2(-speed * Time.deltaTime, rig.velocity.y);
                    break;
                case Dir.RIGHT:
                    rig.velocity = new Vector2(speed * Time.deltaTime, rig.velocity.y);
                    break;
            }
        }
    }
    public void SetDirection()
    {
        if (transform.position.x > 0)
            direction = Dir.LEFT;
        else
            direction = Dir.RIGHT;
    }
    public void Deactivate()
    {
        rig.velocity = Vector2.zero;
        this.enabled = false;
    }
}
