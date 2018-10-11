using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged : Enemy
{
    public float timeToAttack;
    public RangeDetector detector;
    public Bullet bullet;
    private float timer;
    private bool isAttacking;
    private Animator anim;

    protected override void Start()
    {
        base.Start();
        timer = 0;
        detector.RampartOnRange += OnRange;
        detector.RampartOffRange += OffRange;
        anim = GetComponent<Animator>();
    }
    private void OnDestroy()
    {
        detector.RampartOnRange -= OnRange;
        detector.RampartOffRange -= OffRange;
    }
    private void Update()
    {
        if (rampart)
            if (rampart.IsAlive())
            {
                if (timer < timeToAttack)
                {
                    timer += Time.deltaTime;
                }
                else if (syncroAttackWithAnim)
                {
                    GameObject b = Instantiate(bullet.gameObject, transform.position, Quaternion.identity);
                    Vector2 bulletDirection = rampart.transform.position - transform.position;
                    b.GetComponent<Bullet>().SetDamage(damage);
                    b.GetComponent<Bullet>().SetType(Bullet.TypeOf.Enemy);
                    b.GetComponent<Bullet>().Shoot(bulletDirection.normalized, Vector3.right);
                    timer = 0;
                    syncroAttackWithAnim = false;

                }
            }
            else
            {
                rampart = null;
                movementBehaviour.SetCanMove(true);
                timer = 0;
            }
    }
    private void OnRange(RangeDetector d)
    {
        rampart = d.GetRampart();
        movementBehaviour.SetCanMove(false);
        anim.SetBool("AttackOn", true);
    }
    private void OffRange(RangeDetector d)
    {
        rampart = d.GetRampart();
        movementBehaviour.enabled = true;
        anim.SetBool("AttackOn", false);
    }
}

