using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour {

    private PlatformEffector2D effector;    
    public float resetTime = 0.5f;
    public float timer;
    public bool lookingUp = true;    

	// Use this for initialization
	void Start () {
        effector = GetComponent<PlatformEffector2D>();        
	}
	
	// Update is called once per frame
	void Update () {
        if (!lookingUp)
        {
            timer += Time.deltaTime;
            if (timer >= resetTime)
            {                
                effector.rotationalOffset = 0;
                lookingUp = true;
                timer = 0;
            }
        }
	}
    public void Deactivate()
    {
        lookingUp = false;
        effector.rotationalOffset = 180f;
    }

}
