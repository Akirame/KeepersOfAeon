using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour {

    public enum TUTORIAL_TYPE {ORB, ATTACK }
    public TUTORIAL_TYPE tutorialType;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            TutorialText tutorial = collision.GetComponentInChildren<TutorialText>();
            switch (tutorialType)
            {
                case TUTORIAL_TYPE.ORB:
                    tutorial.ChangeTutorialTo(TutorialText.TUTORIAL_STATE.ORB);
                    break;
                case TUTORIAL_TYPE.ATTACK:
                    if (tutorial.orbTutorialEnd)
                        tutorial.ChangeTutorialTo(TutorialText.TUTORIAL_STATE.ATTACK);
                    break;
                default:
                    break;
            }
        }
    }

}
