using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampolin : MonoBehaviour {

    public float jumpForce;
    public Vector3 bounceLocalScale;
    private Vector3 currentLocalScale;
    private bool bounceEnabled = false;
    private void Start()
    {
        currentLocalScale = transform.localScale;
    }

    IEnumerator BounceEffect()
    {
        for(int i = 0; i < 2; i++)
        {
            if (bounceEnabled)
                transform.localScale = bounceLocalScale;
            else
                transform.localScale = currentLocalScale;                            
            bounceEnabled = !bounceEnabled;
            yield return new WaitForSeconds(0.1f);
        }
        transform.localScale = currentLocalScale;
        bounceEnabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            CharacterController2D character = collision.GetComponent<CharacterController2D>();
            InputControl characterInput = collision.GetComponent<InputControl>();
            if (character.jumped && !(Input.GetAxis(characterInput.axisY) > 0))
            {
            StopAllCoroutines();
            StartCoroutine("BounceEffect");
                Rigidbody2D rig = collision.gameObject.GetComponent<Rigidbody2D>();
                rig.velocity = new Vector2(rig.velocity.x, jumpForce);
            }
        }
        if (collision.tag == "ElementalOrb")
        {
            Rigidbody2D rigOrb = collision.GetComponent<Rigidbody2D>();
            if (rigOrb.velocity.y < 0)
            {
            StopAllCoroutines();
            StartCoroutine("BounceEffect");
                rigOrb.velocity = new Vector2(rigOrb.velocity.x, jumpForce / 1.8f);
            }
        }
    }
}
