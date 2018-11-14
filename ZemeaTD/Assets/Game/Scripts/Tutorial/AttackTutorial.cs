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
    private Animation anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animation>();
        toggleAttack.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (playerInput)
        {
            if (Input.GetButtonDown(playerInput.toggleAttackButton))
            {
                controlsEnabled = !controlsEnabled;
                toggleAttack.SetActive(!controlsEnabled);
                attack.SetActive(controlsEnabled);
                aim.SetActive(controlsEnabled);
                orbChange.SetActive(controlsEnabled);
                if (controlsEnabled)
                {
                    anim.Play("AttackTextInfo");
                }
                else
                {
                    anim.Play("AttackTextToggle");
                }
            }
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            toggleAttack.SetActive(true);
            playerInput = collision.GetComponent<InputControl>();
            anim["AttackTextToggle"].speed = 1;
            anim.Play("AttackTextToggle");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerInput = null;
            anim["AttackTextToggle"].speed = -1;
            anim["AttackTextToggle"].time = anim["AttackTextToggle"].length;
            anim.Play("AttackTextToggle");
        }
    }

}
