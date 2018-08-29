using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject ObjectiveToGo;
    private GameObject nose;
    public float transitionTime = 3f;
    public bool fadeToBlack = true;
    private bool transitionOn = false;
    private float startTime;
    private float timer;

    private void Start()
    {
        timer = 0;
    }
    private void Update()
    {
        if (transitionOn)
        {
            SpriteRenderer goSprite = nose.gameObject.GetComponent<SpriteRenderer>();
            if (timer < transitionTime)
            {
                if(fadeToBlack)
                goSprite.color = new Color(Mathf.SmoothStep(1f, 0f, (timer / transitionTime)), Mathf.SmoothStep(1f, 0f, (timer / transitionTime)), Mathf.SmoothStep(1f, 0f, (timer / transitionTime)), 1f);
                else
                goSprite.color = new Color(1f, 1f, 1f, Mathf.SmoothStep(1f, 0f, (timer / transitionTime)));
                timer += Time.deltaTime;                
            }
            else
            {
                nose.transform.position = ObjectiveToGo.transform.position;
                goSprite.color = Color.white;
                timer = 0;
                transitionOn = false;
                nose = null;
            }
        }
    }
    /// <summary>
    /// Transporta al jugador hacia la otra puerta,haciendo un fade para que parezca que entra a la puerta
    /// </summary>
    /// <param name="go"></param>
    public void GotoObjective(GameObject go)
    {
        transitionOn = true;
        nose = go;        
    }
}
