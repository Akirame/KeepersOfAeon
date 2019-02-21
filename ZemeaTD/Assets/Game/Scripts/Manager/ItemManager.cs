using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{

    public Item[] items;
    public float spawnTime = 5f;
    private RectTransform rectTransform;
    private float timer = 0;
    private bool canSpawnItems = false;
    private AudioSource aSource;

    private void Start()
    {
        
        Item.ItemConsumed += DestroyItem;
        aSource = GetComponent<AudioSource>();
        AudioManager.Get().AddSound(aSource);
        rectTransform = GetComponent<RectTransform>();
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
        if (i.WasConsumed())
        {
            aSource.PlayOneShot(i.sound);
        }
        Destroy(i.gameObject);
    }

    private void SpawnItem()
    {
        int randomItemIndex = UnityEngine.Random.Range(0, items.Length);
        int iterations;
        iterations = randomItemIndex == items.Length - 1 ? 2 : 1;
        for(int i = 0; i < iterations; i++)
        {
            float halfRectWidth = rectTransform.rect.width / 2;
            float halfRectHeight = rectTransform.rect.height / 2;
            Vector3 newPos = new Vector3(UnityEngine.Random.Range(-halfRectWidth, halfRectWidth), UnityEngine.Random.Range(-halfRectHeight, halfRectHeight), 1);
            Vector3 finalPos = transform.position + newPos;
            Instantiate(items[randomItemIndex].gameObject, finalPos, Quaternion.identity, transform);
        }
    }
    private void OnDestroy()
    {
        Item.ItemConsumed -= DestroyItem;
    }
}
