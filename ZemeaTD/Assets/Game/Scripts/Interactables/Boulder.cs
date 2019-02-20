using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder :Proyectile
{
    public float desacceleration = 10f;
    public ParticleSystem dustParticles;
    private float timer = 0f;
    private float rotationConta = 0;

    protected override void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();        
    }

    private void Start()
    { 
        speed = 30f;
        damage = 10;
        lifeTimer = 3f;
        rotationConta = transform.rotation.z;
    }

    private void Update()
    {
        if(timer < lifeTimer)
            timer += Time.deltaTime;
        else
            DestroyProyectile();
        if(onGround)
        {
            if(!dustParticles.isPlaying)
                dustParticles.Play();
            float aux = rigid.velocity.x - desacceleration;
            if(aux < 0)
                aux = 0;
            rigid.velocity = new Vector2(rigid.velocity.x - Mathf.Sign(rigid.velocity.x) * desacceleration * Time.deltaTime, 0);
            rotationConta += Time.deltaTime * 100;
            sr.transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, rotationConta);
        }
    }

    public void Shoot(Vector2 dir, int _damage, GameObject _player)
    {
        direction = dir;
        damage = (int)(_damage * 1.5f);
        player = _player;
        rigid.velocity = new Vector2(Mathf.Sign(direction.x) * speed, Random.Range(direction.y, direction.y + 45));
        dustParticles.transform.localPosition = new Vector2(2 * -Mathf.Sign(direction.x), -2);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy" && collision.GetComponent<Enemy>().IsAlive())
        {
            collision.GetComponent<Enemy>().TakeDamage(damage, player);
            PopDamageText(damage);
            Destroy(gameObject);
        }
        if(collision.tag == "Balloon")
        {
            //completar
        }
        if(collision.tag == "Ground")
        {
            onGround = true;
        }
    }
}
