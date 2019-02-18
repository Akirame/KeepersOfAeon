using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder :MonoBehaviour
{
    public float speed = 30f;
    private Vector2 direction;
    public int damage = 10;
    public GameObject player;
    public PopText popText;
    public float desacceleration = 10f;
    public ParticleSystem dustParticles;
    public SpriteRenderer sp;
    private Rigidbody2D rigid;
    private bool onGround = false;
    private float lifeTimer = 3f;
    private float timer = 0f;
    private float rotationConta = 0;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        rotationConta = transform.rotation.z;
    }
    private void Update()
    {
        if(timer < lifeTimer)
            timer += Time.deltaTime;
        else
            Destroy(this.gameObject);
        if(onGround)
        {
            if(!dustParticles.isPlaying)
                dustParticles.Play();
            float aux = rigid.velocity.x - desacceleration;
            if(aux < 0)
                aux = 0;
            rigid.velocity = new Vector2(rigid.velocity.x - Mathf.Sign(rigid.velocity.x) * desacceleration * Time.deltaTime, 0);
            rotationConta += Time.deltaTime * 100;
            sp.transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, rotationConta);            
        }
    }
    public void Shoot(Vector2 dir, int _damage, GameObject _player)
    {        
        direction = dir;
        damage = (int)(_damage * 1.5f);
        player = _player;        
        rigid.velocity = new Vector2(Mathf.Sign(direction.x) * speed, Random.Range(direction.y,direction.y+45));
        dustParticles.transform.localPosition = new Vector2(2 * -Mathf.Sign(direction.x), -2);        
    }

    public int GetDamage()
    {
        return damage;
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

    private void PopDamageText(int bulletDamage)
    {
        GameObject go = Instantiate(popText.gameObject, transform.position, Quaternion.identity, transform.parent);
        go.GetComponent<PopText>().CreateText(bulletDamage.ToString(), Color.black);        
    }

    private void OnBecameInvisible()
    {
        DestroyProyectile();
    }
    public void DestroyProyectile()
    {
        Destroy(gameObject);
    }
}
