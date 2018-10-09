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
        ChangeTextTo(tutorialIndex);
    }

    public void ChangeTutorialTo(TUTORIAL_STATE tutorial)
    {
        // SETEO EL COMIENZO DEL TUTORIAL
        if (tutorial != currentTutorial)
        {
            currentTutorial = tutorial;
            switch (tutorial)
            {
                case TUTORIAL_STATE.MOVEMENT:
                    if (!movementTutorialEnd)
                        tutorialIndex = 0; //La primera linea de Movimiento
                    break;
                case TUTORIAL_STATE.ORB:
                    tutorialIndex = 2; //La primera linea de Orbes
                    break;
                case TUTORIAL_STATE.ATTACK:
                    if (!attackTutorialEnd)
                        tutorialIndex = 5; //La primera linea de Ataque
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
        if (IsTutorialCompleted())
        {
            ClearText();
            Destroy(this.gameObject);
        }
	}

    private void ClearText()
    {
        tutorialText.text = "";
    }

    private bool IsTutorialCompleted()
    {
        return movementTutorialEnd && orbTutorialEnd && attackTutorialEnd;
    }


    public void AttackTutorial()
    {
        if (!attackTutorialEnd)
        {
            ChangeTextTo(tutorialIndex);
            timer += Time.deltaTime;
            if (timer >= tutorialTextTime)
            {
                if (tutorialIndex + 1 > 7)
                {
                    attackTutorialEnd = true;
                    ClearText();
                }
                else
                {
                    tutorialIndex++;
                    ChangeTextTo(tutorialIndex);
                    timer = 0;
                }
            }
        }
    }

    public void OrbTutorial()
    {
        if (!orbTutorialEnd)
        {
            ChangeTextTo(tutorialIndex);
            timer += Time.deltaTime;
            if (timer >= tutorialTextTime)
            {
                if (tutorialIndex + 1 > 4)
                {
                    orbTutorialEnd = true;
                    ClearText();
                }
                else
                {
                    tutorialIndex++;
                    ChangeTextTo(tutorialIndex);
                    timer = 0;
                }
            }
        }
    }

    public void MovementTutorial() {
        if (!movementTutorialEnd)
        {
            ChangeTextTo(tutorialIndex);
            timer += Time.deltaTime;
            if (timer >= tutorialTextTime)
            {
                if (tutorialIndex + 1 > 1)
                {
                    movementTutorialEnd = true;
                    ClearText();
                }
                else
                {
                    tutorialIndex++;
                    ChangeTextTo(tutorialIndex);
                    timer = 0;
                }
            }
        }
    }

    private void ChangeTextTo(int index)
    {
        tutorialText.text = tutorialList[tutorialIndex];
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
