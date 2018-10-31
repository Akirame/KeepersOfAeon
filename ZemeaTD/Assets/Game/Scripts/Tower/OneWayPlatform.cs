using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour {

    private PlatformEffector2D effector;
    private InputControl playerInput;
    public float resetTime = 0.5f;
    public float timer;
    public bool lookingUp = true;

	// Use this for initialization
	void Start () {
        effector = GetComponent<PlatformEffector2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (playerInput)
        {
            if (Input.GetAxis(playerInput.axisY) > 0 && Input.GetButtonDown(playerInput.jump))
            {
                lookingUp = false;
                effector.rotationalOffset = 180f;
            }
        }
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInput = collision.gameObject.GetComponent<InputControl>();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInput = null;
        }
    }

}
