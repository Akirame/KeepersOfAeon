using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBehaviour : MonoBehaviour {

    public float maxAngleAttack = 65f; 
    public float angleAttackSpeed = 10f;
    public GameObject crosshair;
    public GameObject bullet;
    public Transform crossPos;
    public ElementalOrb currentElement;    
    private Vector3 angleAttack;
    private InputControl inputPlayer;
    private GameObject bulletsContainer;
    private int playerDamage;
    private float timer;
    public float timeBetweenAttacks = 0.2f;
    private CharacterController2D player;

    private void Start()
    {
        inputPlayer = GetComponent<InputControl>();
        bulletsContainer = new GameObject("BulletsContainer");
        player = GetComponent<CharacterController2D>();
        playerDamage = player.playerData.maxDamage;
        timer = timeBetweenAttacks;
    }

    public void AttackControl()
    {
        if (Input.GetKey(inputPlayer.jump))
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
        else if (Input.GetKey(inputPlayer.moveDown))
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

        if (Input.GetKey(inputPlayer.secondaryButton))
        {
            if(timer >= timeBetweenAttacks) {
                GameObject b = Instantiate(bullet, transform.position, transform.rotation, bulletsContainer.transform);
                Vector2 bulletDirection = crossPos.position - transform.position;
                b.GetComponent<ElementalProyectile>().Shoot(bulletDirection.normalized, playerDamage, currentElement);
                timer = 0;
            }
            else
                timer += Time.deltaTime;
        }
    }

    private bool IsFacingRight()
    {
        return transform.localScale.x > 0;

    }

    public void SetVisibilityCrosshair(bool val)
    {
        crossPos.gameObject.SetActive(val);
        angleAttack = Vector3.zero;
    }

    public void ChangeElement(ElementalOrb element)
    {
        currentElement = element;
    }
}
