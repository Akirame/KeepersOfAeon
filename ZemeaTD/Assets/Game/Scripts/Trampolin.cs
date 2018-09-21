using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampolin : MonoBehaviour {

    public float jumpForce;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            CharacterController2D character = collision.GetComponent<CharacterController2D>();
            if (character.jumped)
            {
                Rigidbody2D rig = collision.gameObject.GetComponent<Rigidbody2D>();
                rig.velocity = new Vector2(rig.velocity.x, jumpForce);
            }
        }
    }
}
