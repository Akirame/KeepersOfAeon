using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardSquare : MonoBehaviour
{
    public delegate void HazardActions(HazardSquare hazard);
    public HazardActions onHazardStopped;
    public bool isActive = false;
    public float explosionTime = 10f;
    public int explosionDamage = 10;
    public float damagePerButtonHit = 5f;

    private Tower tower;
    private float currentTime = 0f;
    private UnityEngine.UI.Image radialTimer;
    private GameObject ElementsGroup;
    private float hp = 100;
    private List<GameObject> players;
    private Animator animator;

    void Start()
    {
        tower = (Tower)GameObject.FindObjectOfType(typeof(Tower));
        ElementsGroup = transform.Find("ElementsGroup").gameObject;
        radialTimer = ElementsGroup.transform.Find("Canvas").Find("HazardTimer").GetComponent<UnityEngine.UI.Image>();
        players = new List<GameObject>();
        animator = ElementsGroup.GetComponent<Animator>();
    }


    void Update()
    {
        if(isActive)
        {
            if(players.Count > 0)
            foreach(GameObject player in players)
            {                
                if (Input.GetButtonDown(player.GetComponent<InputControl>().attackButton))
                {
                    StopHazard();
                    animator.SetTrigger("PutOutFire");
                }
            }
            radialTimer.fillAmount = currentTime / explosionTime;
            if(currentTime >= explosionTime)
            {
                tower.TakeDamage(explosionDamage);
                currentTime = 0f;
            }
            else
            {
                currentTime += Time.deltaTime;
            }
        }
    }

    public void StopHazard()
    {
        hp -= damagePerButtonHit;
        if (hp <= 0)
        {
            onHazardStopped(this);
        }
    }

    public void SetActive(bool value) 
    {
        isActive = value;
        ElementsGroup.SetActive(value);
        hp = 100;
        currentTime = 0f;
        radialTimer.fillAmount = currentTime / explosionTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            players.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            players.Remove(collision.gameObject);
        }
    }
}
