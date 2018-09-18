using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampolin : MonoBehaviour {

    public float jumpForce;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (Input.GetKeyDown(collision.gameObject.GetComponent<InputControl>().jump))
            {
                Rigidbody2D rig = collision.gameObject.GetComponent<Rigidbody2D>();
                rig.velocity = new Vector2(rig.velocity.x, jumpForce);
            }
        }
    }

}
