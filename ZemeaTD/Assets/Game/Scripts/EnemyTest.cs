using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour
{
    public enum Dir { LEFT,RIGHT}
    public float speed = 10;
    public Dir direction;

    private void Update()
    {
        switch (direction)
        {
            case Dir.LEFT:
                GetComponent<Rigidbody2D>().velocity = new Vector2(-speed * Time.deltaTime, 0);
                break;
            case Dir.RIGHT:
                GetComponent<Rigidbody2D>().velocity = new Vector2(speed * Time.deltaTime, 0);
                break;            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Tower")
        {            
            Destroy(this.gameObject);
        }
    }
}
