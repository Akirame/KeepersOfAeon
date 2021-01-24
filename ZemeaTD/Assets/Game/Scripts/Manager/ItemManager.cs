using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public enum SpawnTypes {Item, Weapon};
    public Item[] items;
    public Item weapon;
    public float minItemSpawnTime = 5f;
    public float maxItemSpawnTime = 10f;
    public float itemSpawnTime = 0f;
    public float minWeaponSpawnTime = 25f;
    public float maxWeaponSpawnTime = 45f;
    public float weaponSpawnTime = 0f;
    private RectTransform rectTransform;
    private float itemTimer = 0;
    private float weaponTimer = 0;
    private bool canSpawnItems = false;
    private AudioSource aSource;

    private void Start()
    {
        
        Item.ItemConsumed += DestroyItem;
        aSource = GetComponent<AudioSource>();
        AudioManager.Get().AddSound(aSource);
        rectTransform = GetComponent<RectTransform>();
        SetItemSpawnTime();
        SetWeaponSpawnTime();
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
            if (itemTimer < itemSpawnTime)
                itemTimer += Time.deltaTime;
            else
            {
                SpawnItem(SpawnTypes.Item);
                itemTimer = 0;
                SetItemSpawnTime();
            }
            if (weaponTimer < itemSpawnTime)
                weaponTimer += Time.deltaTime;
            else
            {
                SpawnItem(SpawnTypes.Weapon);
                weaponTimer = 0;
                SetItemSpawnTime();
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

    private void SpawnItem(SpawnTypes spawnType)
    {
        int randomItemIndex = UnityEngine.Random.Range(0, items.Length);
        int iterations;
        Item itemToSpawn = spawnType == SpawnTypes.Item ? items[randomItemIndex] : weapon;
        iterations = itemToSpawn.itemType == Item.TypeOfItem.Chicken  ? 2 : 1;
        
        
        for(int i = 0; i < iterations; i++)
        {
            float halfRectWidth = rectTransform.rect.width / 2;
            float halfRectHeight = rectTransform.rect.height / 2;
            Vector3 newPos = new Vector3(UnityEngine.Random.Range(-halfRectWidth, halfRectWidth), UnityEngine.Random.Range(-halfRectHeight, halfRectHeight), 1);
            Vector3 finalPos = transform.position + newPos;
            Instantiate(itemToSpawn.gameObject, finalPos, Quaternion.identity, transform);
        }
    }

    private void SetItemSpawnTime()
    {
        itemSpawnTime = UnityEngine.Random.Range(minItemSpawnTime,maxItemSpawnTime);
    }

    private void SetWeaponSpawnTime()
    {
        weaponSpawnTime = UnityEngine.Random.Range(minWeaponSpawnTime,maxWeaponSpawnTime);
    }
    private void OnDestroy()
    {
        Item.ItemConsumed -= DestroyItem;
    }
}
