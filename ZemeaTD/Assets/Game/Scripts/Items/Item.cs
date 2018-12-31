using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
    public delegate void ItemActions(Item i);
    public static ItemActions InvulnerableConsume;
    public static ItemActions RalenticeConsume;
    public static ItemActions ItemConsumed;
    public float lifeTime = 8;
    public AudioClip sound;
    public bool consumed = false;
    private float timer = 0;

    public enum TypeOfItem
    {
      InvulnerableShield,
      RalenticeEnemies,
    }
    public TypeOfItem itemType;

    private void Update()
    {
        if(timer < lifeTime)
            timer += Time.deltaTime;
        else
            ItemConsumed(this);
    }
    protected  void ConsumeItem()
    {
        switch(itemType)
        {
            case TypeOfItem.InvulnerableShield:
                InvulnerableConsume(this);
                break;
            case TypeOfItem.RalenticeEnemies:
                RalenticeConsume(this);
                break;
        }
        consumed = true;
        ItemConsumed(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
            ConsumeItem();
    }
}
