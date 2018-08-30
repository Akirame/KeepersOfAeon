using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject ObjectiveToGo;
    private GameObject goPlayer;    
    public float transitionTime = 3f;
    public bool fadeToBlack = true;
    public Animator doorDisplay;
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
            SpriteRenderer goSprite = goPlayer.gameObject.GetComponent<SpriteRenderer>();
            if (timer < transitionTime)
            {                
                if (fadeToBlack)
                goSprite.color = new Color(Mathf.SmoothStep(1f, 0f, (timer / transitionTime)), Mathf.SmoothStep(1f, 0f, (timer / transitionTime)), Mathf.SmoothStep(1f, 0f, (timer / transitionTime)), 1f);
                else
                goSprite.color = new Color(1f, 1f, 1f, Mathf.SmoothStep(1f, 0f, (timer / transitionTime)));
                timer += Time.deltaTime;                
            }
            else
            {
                goPlayer.transform.position = ObjectiveToGo.transform.position;
                goSprite.color = Color.white;
                timer = 0;
                transitionOn = false;
                goPlayer.GetComponent<CharacterController2D>().SetCanMove(true);
                goPlayer = null;
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
        goPlayer = go;
        go.GetComponent<CharacterController2D>().SetCanMove(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            doorDisplay.SetBool("Appear", true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            doorDisplay.SetBool("Appear", false);
        }
    }
}
