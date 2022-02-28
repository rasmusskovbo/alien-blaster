using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [Header("Waves")]
    [SerializeField] private List<WaveConfigSO> waveConfigs;
    [SerializeField] private float timeBetweenWaves = 2;
    [SerializeField] private bool isLooping = true;
    [SerializeField] private int maxAmountOfSpawners = 5;
    [SerializeField] private int timeBetweenExtraSpawners = 30;

    [Header("Boss")] 
    [SerializeField] private List<WaveConfigSO> bossConfigs;
    [SerializeField] private float timeBetweenBossSpawns = 60f;
    [SerializeField] private float bossSpawnIncrement = 10f;
    [SerializeField] private float minimumTimeBetweenBossSpawns = 5f;

    private int waveCounter;
    
    void Start()
    {
        waveCounter = 0;
        StartCoroutine(RandomWaveSpawner());
        StartCoroutine(BossSpawner());
        StartCoroutine(ScaleDifficulty());
    }
    
    IEnumerator RandomWaveSpawner()
    {
        do
        {
            int randomIndex = Random.Range(0, waveConfigs.Count);
            WaveConfigSO wave = waveConfigs[randomIndex];

            for (int i = 0; i < wave.GetEnemyCount(); i++)
            {
                Instantiate(
                    wave.GetEnemyPrefab(i),
                    wave.GetStartingWaypoint().position,
                    Quaternion.Euler(0, 0, 180),
                    transform
                ).GetComponent<Pathfinder>().SetCurrentWave(wave);

                waveCounter++;
                
                yield return new WaitForSeconds(wave.getRandomSpawnTime());
            }

            yield return new WaitForSeconds(timeBetweenWaves);
            
        } while (isLooping);
    }
    
    IEnumerator BossSpawner()
    {
        do
        {
            yield return new WaitForSeconds(timeBetweenBossSpawns);
            
            int randomIndex = Random.Range(0, bossConfigs.Count);
            WaveConfigSO bossWave = bossConfigs[randomIndex];

            Instantiate(
                bossWave.GetEnemyPrefab(0),
                bossWave.GetStartingWaypoint().position,
                Quaternion.Euler(0, 0, 180),
                transform
            ).GetComponent<Pathfinder>().SetCurrentWave(bossWave);

        } while (true);
    }
    
    /*
    void IncreaseDifficulty()
    {
        // Decrease time between all wave spawners
        if (timeBetweenWaves > 0)
        {
            timeBetweenWaves--;
            Mathf.Clamp(timeBetweenWaves, 0, Int32.MaxValue);
        }
        
        // Increase frequency of extra wave spawners
        if (delayBetweenExtraWaves > 10)
        {
            delayBetweenExtraWaves -= 10;
            Mathf.Clamp(delayBetweenExtraWaves, 0, Int32.MaxValue);
        }
        else if (delayBetweenExtraWaves >= 1)
        {
            delayBetweenExtraWaves--;
            Mathf.Clamp(delayBetweenExtraWaves, 0, Int32.MaxValue);
        }

        // Add wave spawner ever reset
        StartCoroutine(SecondSpawner());
    }
    */

    IEnumerator ScaleDifficulty()
    {
        int counter = 0;

        while (counter < maxAmountOfSpawners)
        {
            yield return new WaitForSeconds(timeBetweenExtraSpawners);
            if (timeBetweenWaves < maxAmountOfSpawners) timeBetweenWaves++;
            if (timeBetweenBossSpawns > minimumTimeBetweenBossSpawns) timeBetweenBossSpawns -= bossSpawnIncrement;
            counter++;
            Debug.Log("Increasing difficulty. Current level: " + counter);
            Debug.Log("Time between waves: " + timeBetweenWaves);
            Debug.Log("Boss spawn frequency: " + timeBetweenBossSpawns);
            StartCoroutine(RandomWaveSpawner());
        }
        
    }
    
}
