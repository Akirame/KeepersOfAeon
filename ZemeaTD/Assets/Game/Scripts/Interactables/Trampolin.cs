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
            InputControl characterInput = collision.GetComponent<InputControl>();
            if (character.jumped && !Input.GetKey(characterInput.moveDown))
            {
                Rigidbody2D rig = collision.gameObject.GetComponent<Rigidbody2D>();
                rig.velocity = new Vector2(rig.velocity.x, jumpForce);
            }
        }
        if (collision.tag == "ElementalOrb")
        {
            Rigidbody2D rigOrb = collision.GetComponent<Rigidbody2D>();
            if (rigOrb.velocity.y < 0)
            {
                rigOrb.velocity = new Vector2(rigOrb.velocity.x, jumpForce / 1.8f);
            }
        }
    }
}
