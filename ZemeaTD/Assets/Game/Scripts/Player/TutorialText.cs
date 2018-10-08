using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialText : MonoBehaviour {

    public enum TUTORIAL_STATE {MOVEMENT, ORB, ATTACK}
    private TUTORIAL_STATE currentTutorial;
    public InputControl playerControl;
    public CharacterController2D player;
    public Text tutorialText;
    private float timer;
    public float tutorialTextTime = 7.5f;
    private List<string> tutorialList;
    public int tutorialIndex = 0;
    public bool movementTutorialEnd = false;
    public bool orbTutorialEnd = false;
    public bool attackTutorialEnd = false;

    // Use this for initialization
    void Start () {
        tutorialList = new List<string>();
        player = transform.parent.GetComponent<CharacterController2D>();
        InitializeTutorials();
        currentTutorial = TUTORIAL_STATE.MOVEMENT;
        ChangeTextFacing();
        ChangeText();
    }

    public void ChangeTutorialTo(TUTORIAL_STATE tutorial)
    {
        currentTutorial = tutorial;
        // SETEO EL COMIENZO DEL TUTORIAL
        if (tutorial != currentTutorial)
        {
            switch (tutorial)
            {
                case TUTORIAL_STATE.MOVEMENT:
                    if (!movementTutorialEnd)
                        tutorialIndex = 0;
                    break;
                case TUTORIAL_STATE.ORB:
                    if (!orbTutorialEnd)
                        tutorialIndex = 2;
                    break;
                case TUTORIAL_STATE.ATTACK:
                    if (!attackTutorialEnd)
                        tutorialIndex = 5;
                    break;
                default:
                    break;
            }
        }

    }

    // Update is called once per frame
    void Update () {
        switch (currentTutorial)
        {
            case TUTORIAL_STATE.MOVEMENT:
                MovementTutorial();
                break;
            case TUTORIAL_STATE.ORB:
                OrbTutorial();
                break;
            case TUTORIAL_STATE.ATTACK:
                AttackTutorial();
                break;
            default:
                break;
        }
        ChangeTextFacing();
        ChangeText();
        if (IsTutorialCompleted())
        {
            tutorialText.text = "";
            Destroy(this.gameObject);
        }
	}

    private bool IsTutorialCompleted()
    {
        return movementTutorialEnd && orbTutorialEnd && attackTutorialEnd;
    }


    public void AttackTutorial()
    {
        if (!attackTutorialEnd)
        {
            timer += Time.deltaTime;
            if (timer >= tutorialTextTime)
            {
                tutorialIndex++;
                ChangeText();
            }
            if (tutorialIndex + 1 > 7)
            {
                attackTutorialEnd = true;
            }
        }
    }

    public void OrbTutorial()
    {
        if (!orbTutorialEnd)
        {
            timer += Time.deltaTime;
            if (timer >= tutorialTextTime)
            {
                tutorialIndex++;
                ChangeText();
            }
            if (tutorialIndex + 1 > 4)
            {
                orbTutorialEnd = true;
            }
        }
    }

    public void MovementTutorial() {
        if (!movementTutorialEnd)
        {
            timer += Time.deltaTime;
            if (timer >= tutorialTextTime)
            {
                tutorialIndex++;
                ChangeText();
            }
            if (tutorialIndex + 1 > 1)
            {
                movementTutorialEnd = true;
            }
        }
    }

    private void ChangeText()
    {
        if (tutorialText.text != tutorialList[tutorialIndex])
        {
            tutorialText.text = tutorialList[tutorialIndex];
            timer = 0;
        }
    }


    private void ChangeTextFacing()
    {
        if (player.lookingRight)
        {
            tutorialText.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            tutorialText.transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void InitializeTutorials()
    {
        tutorialList.Add("USE " + playerControl.moveLeft.ToString() + " AND " + playerControl.moveRight.ToString() + " TO MOVE"); // MOVEMENT
        tutorialList.Add("USE " + playerControl.jump.ToString() + " TO JUMP");
        tutorialList.Add(playerControl.primaryButton.ToString() + " TO PICK AN ORB");
        tutorialList.Add(playerControl.primaryButton.ToString() + " TO CONSUME AN ORB");
        tutorialList.Add("YOU CAN THROW A PICKED ORB WITH "+ playerControl.secondaryButton.ToString());
        tutorialList.Add(playerControl.thirdButton.ToString() + " TO ACTIVATE/DEACTIVATE ATTACK MODE ON BALCONY");
        tutorialList.Add("USE " + playerControl.jump.ToString() + " AND " + playerControl.moveDown.ToString() + " TO AIM");
        tutorialList.Add(playerControl.secondaryButton.ToString() + " TO SHOOT");
    }

}
