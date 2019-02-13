using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedBehavior : Enemy
{
    public float timeToAttack;
    public RangeDetector detector;
    public EnemyBullet bullet;
    private float timer;
    private float throwOffset = 20f;

    protected override void Start()
    {
        base.Start();
        timer = 0;
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
            if (rampart.IsAlive())
            {
                if (canAttack)
                {
                    if (timer < timeToAttack)
                    {
                        timer += Time.deltaTime;
                    }
                    else
                    {
                        Attack();
                    }
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
    }

    private void OffRange(RangeDetector d)
    {
        rampart = d.GetRampart();
        movementBehaviour.enabled = true;
        movementBehaviour.SetCanMove(true);
    }

    private void Attack()
    {
        GameObject b = Instantiate(bullet.gameObject, transform.position, Quaternion.identity);
        Vector2 bulletDirection = rampart.transform.position - transform.position + new Vector3(0, UnityEngine.Random.Range(0,throwOffset));
        b.GetComponent<EnemyBullet>().Shoot(bulletDirection.normalized, damage);
        timer = 0;
    }

}

