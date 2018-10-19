using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalExplosion : MonoBehaviour {

    public float explosionRadius;
    public float explosionForce;
    public Sprite sprite;
    public int damage;
    private float force;
    private GameObject playerThrow;
    public ParticleSystem particles;
    private Vector3 defaultPos;

    private void Start()
    {
        //GetComponent<SpriteRenderer>().sprite = sprite;
        defaultPos = new Vector3(transform.position.x, transform.position.y, -10);
    }
    
    void Update () {
        //force += explosionForce * Time.deltaTime;
        //transform.localScale = new Vector2(1, 1) * force;
        //if (transform.localScale.x > explosionRadius)
        //{
        //    Destroy(this.gameObject);
        //}
        transform.position = defaultPos;
        if(!particles.isEmitting)
            Destroy(this.gameObject);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().TakeDamage(damage,playerThrow);
        }
    }

    public void SetPlayerThrow(GameObject player)
    {
        playerThrow = player;
    }

}
