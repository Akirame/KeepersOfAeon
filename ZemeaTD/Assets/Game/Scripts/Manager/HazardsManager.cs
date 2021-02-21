using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardsManager : MonoBehaviour
{
    public float startingHazardsSpawnTime = 20f;
    public float startingChanceOfSpawn = 20f;    
    public float minusTimePerCheck = 1f;
    public float plusChancePerCheck = 5f;

    private List<HazardSquare> UnactiveHazards;
    private List<HazardSquare> ActiveHazards;
    private float currentTimer = 0f;
    private float currentHazardsSpawnTime;
    private float currentChanceOfSpawn;

    void Start()
    {
        UnactiveHazards = new List<HazardSquare>();
        ActiveHazards = new List<HazardSquare>();
        UnactiveHazards.AddRange(GameObject.FindObjectsOfType<HazardSquare>());

        currentChanceOfSpawn = startingChanceOfSpawn;
        currentHazardsSpawnTime = startingHazardsSpawnTime;

        foreach(HazardSquare hazard in UnactiveHazards)
            hazard.onHazardStopped += RemoveHazardFromList;
    }

    void Update()
    {
        if(UnactiveHazards.Count > 0 && WaveControl.GetInstance().IsWaveActive())
        if(currentTimer >= currentHazardsSpawnTime)
        {
            currentTimer = 0f;
            float randomNumber = Random.Range(0f,100f);
            if(randomNumber <= currentChanceOfSpawn)
            {
                currentChanceOfSpawn = startingChanceOfSpawn;
                currentHazardsSpawnTime = startingHazardsSpawnTime;

                int randomIndex = Random.Range(0,UnactiveHazards.Count);
                HazardSquare hazard = UnactiveHazards[randomIndex];
                hazard.SetActive(true);                
                ActiveHazards.Add(hazard);
                UnactiveHazards.Remove(hazard);
            }
            else
            {
                currentChanceOfSpawn += plusChancePerCheck;
                currentHazardsSpawnTime -= minusTimePerCheck;
            }
        }
        else
        {
            currentTimer += Time.deltaTime;
        }
    }

    void RemoveHazardFromList(HazardSquare hazard)
    {
        hazard.SetActive(false);
        ActiveHazards.Remove(hazard);
        UnactiveHazards.Add(hazard);
    }

    private void OnDestroy()
    {
        foreach(HazardSquare hazard in UnactiveHazards)
            hazard.onHazardStopped -= RemoveHazardFromList;

        foreach(HazardSquare hazard in ActiveHazards)
            hazard.onHazardStopped -= RemoveHazardFromList;
    }
}
