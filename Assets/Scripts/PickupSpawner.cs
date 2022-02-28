using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [Header("HP")]
    [SerializeField] private GameObject hpPickupPrefab;
    [SerializeField] private int hpFrequency = 30;
    [SerializeField] private int hpFrequencyVariance = 5;
    [SerializeField] private int hpPickupSpeed = 10;
    [SerializeField] private int hpPickupLifeTime = 10;
    
    
    [Header("Fire Rate")]
    [SerializeField] private GameObject fireRatePickupPrefab;
    [SerializeField] private int fireRateFrequency = 60;
    [SerializeField] private int fireRateFrequencyVariance = 20;
    [SerializeField] private int fireRatePickupSpeed = 5;
    [SerializeField] private int fireRatePickupLifeTime = 10;
    
    [Header("Boost")]
    [SerializeField] private GameObject boostPickupPrefab;
    [SerializeField] private int boostFrequency = 20;
    [SerializeField] private int boostFrequencyVariance = 5;
    [SerializeField] private int boostPickupSpeed = 15;
    [SerializeField] private int boostPickupLifeTime = 5;
    
    private Vector2 spawnPosition;
    private int spawnRange = 4;
    private Coroutine fireRateSpawner;
    
    void Start()
    {
        StartCoroutine(
            SpawnPickup(
                hpPickupPrefab, 
                hpFrequency, 
                hpFrequencyVariance, 
                hpPickupSpeed, 
                hpPickupLifeTime)
            );
        
        fireRateSpawner = StartCoroutine(
            SpawnPickup(
                fireRatePickupPrefab, 
                fireRateFrequency, 
                fireRateFrequencyVariance, 
                fireRatePickupSpeed, 
                fireRatePickupLifeTime)
        );
        
        StartCoroutine(
            SpawnPickup(
                boostPickupPrefab, 
                boostFrequency, 
                boostFrequencyVariance, 
                boostPickupSpeed, 
                boostPickupLifeTime)
        );
    }

    IEnumerator SpawnPickup(
        GameObject pickup,
        int frequency,
        int frequencyVariance,
        int pickupSpeed,
        int pickupLifetime
    )
    {
        while (true)
        {
            yield return new WaitForSeconds(GetRandomSpawnFrequency(frequency, frequencyVariance));
            
            spawnPosition = new Vector2(GetRandomSpawnPosition(), 10);
            
            GameObject instance = Instantiate(
                pickup,
                spawnPosition,
                Quaternion.Euler(0,0,180)
            );
        
            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
        
            if (rb) rb.velocity = transform.up * pickupSpeed;
            Destroy(instance, pickupLifetime);
        }
    }

    public int GetRandomSpawnPosition()
    {
        return Random.Range(-spawnRange, spawnRange);
    }
    
    float GetRandomSpawnFrequency(int baseFrequency, int variance)
    {
        return Random.Range(
            baseFrequency - variance,
            baseFrequency + variance
        );
    }

    public void StopFireRateSpawn()
    {
        StopCoroutine(fireRateSpawner);
    }
}
