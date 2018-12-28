using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyerBehaviour : MonoBehaviour
{        
    public float maxSpeedUpDown = 4f;
    public float minSpeedUpDown = 2f;
    public float maxDistanceUpDown = 10f;
    public float minDistanceUpDown = 2f;
    private float distanceUpDown;
    private float speedUpDown;
    private float centerPos;
    private bool flying = true;
    private bool onGround = false;
    public float rotateAngle;
    private float rotation;
    private int dirRotation = 1;
    public List<Balloon> balloonsGroup;

    private void Start()
    {       
        centerPos = Random.Range(15f, 50f);
        distanceUpDown = Random.Range(minDistanceUpDown, maxDistanceUpDown);
        speedUpDown = Random.Range(minSpeedUpDown, maxSpeedUpDown);
        for(int i = 0; i < balloonsGroup.Count; i++)
            balloonsGroup[i].onDeath += BalloonDestroyed;
        if (Random.Range(0f,1f) > 0.5)
        {
            dirRotation = -1;
        }
    }

    private void Update()
    {
        if(balloonsGroup.Count <= 0 && flying)
        {
            flying = false;
            GetComponent<EnemyMovementBehaviour>().enabled = flying;
        }
    }
    private void LateUpdate()
    {
        if (flying)
        {
            Vector3 mov = new Vector3(transform.position.x, centerPos + Mathf.Sin(speedUpDown * Time.time) * distanceUpDown, transform.position.z);
            transform.position = mov;
        }
        else
        {
            if (!onGround)
            {
                rotation += rotateAngle * Time.deltaTime * dirRotation;
                transform.Rotate(Vector3.back, rotation);
            }
        }

    }
    private void BalloonDestroyed(Balloon b)
    {
        balloonsGroup.Remove(b);
        Destroy(b.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            Rigidbody2D rid = GetComponent<Rigidbody2D>();
            rid.velocity = new Vector2();
            rid.gravityScale = 0;
            onGround = true;
            GetComponent<Enemy>().Kill();
        }
    }
}
