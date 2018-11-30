using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialComponent : MonoBehaviour {

    private int hitCounter;
    public DestructibleTutorialTrigger[] triggers;
    public bool tutorialTerminated = false;

	// Use this for initialization
	void Start () {
        hitCounter = triggers.Length;
	}
	
	// Update is called once per frame
	void Update () {
        CheckTriggersActivated();
	}

    private void CheckTriggersActivated()
    {
        if (!tutorialTerminated)
        {
            int cont = 0;
            foreach (DestructibleTutorialTrigger item in triggers)
            {
                if (item.activated)
                {
                    cont++;
                }
            }
            if (cont == hitCounter)
            {
                tutorialTerminated = true;
                transform.parent.GetComponent<TutorialManager>().ChangeToNextTutorial();
            }
        }
    }

}
