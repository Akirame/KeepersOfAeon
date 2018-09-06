using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rampart : MonoBehaviour
{
    private int health;
    private SpriteRenderer renderer;

	void Start ()
    {
        health = 100;
        renderer = GetComponent<SpriteRenderer>();
	}
    private void Update()
    {
        if (IsAlive())
        {

        }
        else
        {
            Color c = new Vector4(0, 0, 0, 0);
            renderer.color = c;
        }
    }
    public void Attacked(int damage)
    {
        health -= damage;        
        renderer.color = Color.red;
        Debug.Log(health + "---" + gameObject.name);
    }
    private bool IsAlive()
    {
        if (health > 0)
            return true;
        else
            return false;
    }
}
