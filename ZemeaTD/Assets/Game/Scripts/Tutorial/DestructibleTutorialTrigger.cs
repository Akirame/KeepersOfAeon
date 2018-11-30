using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleTutorialTrigger : MonoBehaviour {

    public bool activated = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !activated)
        {
            activated = true;
        }
    }
}
