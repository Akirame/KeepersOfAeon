using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour {

    private PlatformEffector2D effector;
    private List<Collider2D> playerCollision;

	// Use this for initialization
	void Start () {
        effector = GetComponent<PlatformEffector2D>();        
	}
	
	// Update is called once per frame
	void Update () {
	}
    public void Deactivate(Collider2D playerCollider)
    {
        Physics2D.IgnoreCollision(playerCollider, GetComponent<BoxCollider2D>());
    }

}
