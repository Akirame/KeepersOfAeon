using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour {

    public enum TUTORIAL_TYPE {ORB, ATTACK }
    public TUTORIAL_TYPE tutorialType;
    public GameObject player = null;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!player)
        {
            if (collision.tag == "Player")
            {
                player = collision.gameObject;
            }
        }
        else
        {
            if (collision.tag == "Player")
            {
                if (player != collision.gameObject)
                {
                    TutorialText tutorial = collision.GetComponentInChildren<TutorialText>();
                    tutorial.ChangeTutorialTo(TutorialText.TUTORIAL_STATE.ORB);
                    Destroy(this.gameObject);
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            TutorialText tutorial = collision.GetComponentInChildren<TutorialText>();
            switch (tutorialType)
            {
                case TUTORIAL_TYPE.ORB:
                    if (tutorial)
                    {
                        tutorial.ChangeTutorialTo(TutorialText.TUTORIAL_STATE.ORB);
                    }
                    break;
                case TUTORIAL_TYPE.ATTACK:
                    if (tutorial.orbTutorialEnd)
                    {
                        tutorial.ChangeTutorialTo(TutorialText.TUTORIAL_STATE.ATTACK);
                        Destroy(this.gameObject);
                    }
                    break;
                default:
                    break;
            }
        }
    }

}
