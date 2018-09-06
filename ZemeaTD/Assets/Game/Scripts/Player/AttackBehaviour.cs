using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : MonoBehaviour {

    public float maxAngleAttack = 65f;
    private Vector3 angleAttack;
    public float angleAttackSpeed = 10f;
    public GameObject crosshair;
    public GameObject bullet;
    public Transform crossPos;

    public void AttackControl()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (IsFacingRight())
            {
                angleAttack.z += Time.deltaTime * angleAttackSpeed;
            }
            else
            {
                angleAttack.z -= Time.deltaTime * angleAttackSpeed;
            }
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            if (IsFacingRight())
            {
                angleAttack.z -= Time.deltaTime * angleAttackSpeed;

            }
            else
            {
                angleAttack.z += Time.deltaTime * angleAttackSpeed;
            }
        }
        if (angleAttack.z > maxAngleAttack)
        {
            angleAttack.z = maxAngleAttack;
        }
        else if (angleAttack.z < -maxAngleAttack)
        {
            angleAttack.z = -maxAngleAttack;
        }
        crosshair.transform.eulerAngles = angleAttack;
        if (Input.GetKeyDown(KeyCode.X))
        {
            GameObject b = Instantiate(bullet, transform.position, transform.rotation, transform.parent);
            Vector2 bulletDirection = crossPos.position - transform.position;
            b.GetComponent<TestBullet>().Shoot(bulletDirection.normalized, angleAttack);
        }
    }

    private bool IsFacingRight()
    {
        return transform.localScale.x == 1;

    }

    public void SetVisibilityCrosshair(bool val)
    {
        crossPos.gameObject.SetActive(val);
        angleAttack = Vector3.zero;
    }

}
