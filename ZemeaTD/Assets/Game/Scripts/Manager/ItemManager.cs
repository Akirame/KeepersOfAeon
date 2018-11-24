using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{

    public Item[] items;
    public float spawnTime = 5f;
    public float height = 1;
    public float width = 1;
    private float timer = 0;
    private bool canSpawnItems = false;

    private void Start()
    {
        Item.ItemConsumed += DestroyItem;
    }

    private void Update()
    {
        CheckCanSpawn();
        TrySpawnItem();
    }

    private void TrySpawnItem()
    {
        if (canSpawnItems)
        {
            if (timer < spawnTime)
                timer += Time.deltaTime;
            else
            {
                SpawnItem();
                timer = 0;
            }
        }
    }

    private void CheckCanSpawn()
    {
        if (GameManager.Get().tutorialDone && !canSpawnItems)
        {
            canSpawnItems = true;
        }
    }

    private void DestroyItem(Item i)
    {
        Destroy(i.gameObject);
    }

    private void SpawnItem()
    {
        Vector3 newPos = new Vector3(UnityEngine.Random.Range(-width/2, width/2), UnityEngine.Random.Range(-height/2, height/2),1);
        Vector3 finalPos = transform.position + newPos;
        Instantiate(items[UnityEngine.Random.Range(0, items.Length)].gameObject, finalPos,Quaternion.identity,transform);
    }
}
