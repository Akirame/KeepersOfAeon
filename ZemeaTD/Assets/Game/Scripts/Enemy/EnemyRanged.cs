using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged : Enemy
{
    public float timeToAttack;    
    private float timer;    
    private bool isAttacking;
    public RangeDetector detector;
    public SpriteRenderer sprite;
    public Bullet bullet;

    protected override void Start()
    {
        base.Start();
        timer = 0;
        sprite = GetComponent<SpriteRenderer>();
        detector.RampartOnRange += OnRange;
        detector.RampartOffRange += OffRange;
    }
    private void OnDestroy()
    {
        detector.RampartOnRange -= OnRange;
        detector.RampartOffRange -= OffRange;
    }
    private void Update()
    {
        if (rampart)
        {
            sprite.color = Color.red;
            if (timer < timeToAttack)
            {
                timer += Time.deltaTime;
            }
            else
            {
                GameObject b = Instantiate(bullet.gameObject, transform.position,Quaternion.identity);
                Vector2 bulletDirection = rampart.transform.position - transform.position;
                b.GetComponent<Bullet>().SetDamage(damage);
                b.GetComponent<Bullet>().SetType(Bullet.TypeOf.Enemy);
                b.GetComponent<Bullet>().Shoot(bulletDirection.normalized,Vector3.right);
                timer = 0;
            }
        }
        else
        {
            sprite.color = Color.white;
            timer = 0;
        }
    }
    private void OnRange(RangeDetector d)
    {
        rampart = d.GetRampart();
        movementBehaviour.Deactivate();
    }
    private void OffRange(RangeDetector d)
    {
        rampart = d.GetRampart();
        movementBehaviour.enabled = true;
    }
}

