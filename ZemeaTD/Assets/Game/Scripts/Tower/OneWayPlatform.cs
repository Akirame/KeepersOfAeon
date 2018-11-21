using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour {

    private List<Collider2D> playerCollision; 
	// Use this for initialization
	void Start () { 
	}
	
	// Update is called once per frame
	void Update () {
	}
    IEnumerator CancelIgnore(Collider2D playerCollider)
    {
        yield return new WaitForSeconds(0.2f);
        Physics2D.IgnoreCollision(playerCollider, GetComponent<BoxCollider2D>(),false);
    }
    public void Deactivate(Collider2D playerCollider)
    {
        Physics2D.IgnoreCollision(playerCollider, GetComponent<BoxCollider2D>());
        StartCoroutine(CancelIgnore(playerCollider));
    }

}
