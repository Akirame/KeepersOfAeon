using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : MonoBehaviour {

    public float speed;
    public int health = 50;
    public int experience = 100;
    public int damage = 50;
    private float rotationSpeed;
    private Vector2 direction;
    private Vector2 velocity;
    private Rigidbody2D rid;

    // Use this for initialization
    void Start () {
        rid = GetComponent<Rigidbody2D>();
        CalculateDirection();
        Movement();
        EnableWarning();
	}

    private void EnableWarning()
    {
        UIGame.Get().EnableWarning(transform.position);
    }

    private void Movement()
    {
        velocity = direction * speed * Time.deltaTime;
        rid.velocity = velocity;
        rotationSpeed = UnityEngine.Random.Range(-50, 50);
        rid.angularVelocity = rotationSpeed;
    }

    private void CalculateDirection()
    {
        if (transform.position.x < 0)
        {
            direction.x = UnityEngine.Random.Range(0.8f,1f);
        }
        else
        {
            direction.x = UnityEngine.Random.Range(-0.8f,-1f);
        }
        direction.y = -1;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Rampart")
        {
            collision.GetComponent<Rampart>().Attacked(damage);
            Destroy(gameObject);
        }
        if (collision.tag == "Bullet")
        {
            TakeDamage(collision.GetComponent<ElementalProyectile>().GetDamage());
            Destroy(collision.gameObject);
        }
        if (collision.tag == "Tower")
        {
            collision.GetComponent<Tower>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    private void TakeDamage(int val)
    {
        health -= val;
        if (health <= 0)
        {
            GameManager.Get().player1Level.AddExperience(experience);
            GameManager.Get().player2Level.AddExperience(experience);

            Destroy(gameObject);
        }
    }


    private void OnWillRenderObject()
    {
        //rightWarning.SetActive(false);
        //leftWarning.SetActive(false);
    }
}
