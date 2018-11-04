using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTutorial : MonoBehaviour {

    public GameObject toggleAttack;
    public GameObject attack;
    public GameObject aim;
    public GameObject orbChange;
    private InputControl playerInput;
    private bool controlsEnabled = false; 

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (playerInput)
        {
            if (Input.GetButtonDown(playerInput.thirdButton))
            {
                controlsEnabled = !controlsEnabled;
                toggleAttack.SetActive(!controlsEnabled);
                attack.SetActive(controlsEnabled);
                aim.SetActive(controlsEnabled);
                orbChange.SetActive(controlsEnabled);
            }
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerInput = collision.GetComponent<InputControl>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerInput = null;
        }
    }

}
