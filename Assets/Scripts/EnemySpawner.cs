using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<WaveConfigSO> waveConfigs;
    [SerializeField] private float timeBetweenWaves = 2;
    [SerializeField] private bool isLooping = true;
    [SerializeField] private int delayBetweenExtraWaves = 60;
    private WaveConfigSO nextWave;
    
    void Start()
    {
        StartCoroutine(SpawnEnemyWaves());
        StartCoroutine(SpawnIncreasinglyExtraWaves());
    }

    public WaveConfigSO GetCurrentWave()
    {
        return nextWave;
    }

    IEnumerator SpawnEnemyWaves()
    {
        do
        {
            foreach (WaveConfigSO wave in waveConfigs)
            {
                nextWave = wave;
            
                for (int i = 0; i < nextWave.GetEnemyCount(); i++)
                {
                    Instantiate(
                        nextWave.GetEnemyPrefab(i), 
                        nextWave.GetStartingWaypoint().position,
                        Quaternion.Euler(0,0,180),
                        transform
                    );
                    
                    
                    
                    yield return new WaitForSeconds(nextWave.getRandomSpawnTime());
                }
            
                yield return new WaitForSeconds(timeBetweenWaves);
            }
            
            IncreaseDifficulty();
        } while (isLooping);

    }

    IEnumerator SpawnIncreasinglyExtraWaves()
    {
        do
        {
            for (int i = 0; i < waveConfigs.Count; i++)
            {
                yield return new WaitForSeconds(delayBetweenExtraWaves);

                
                int randomIndex = Random.Range(0, waveConfigs.Count);
                nextWave = waveConfigs[randomIndex];
                
                Debug.Log(waveConfigs.Count);
                Debug.Log("Spawning extra index, wave: " + randomIndex +", " + nextWave.name);
            
                for (int j = 0; j < nextWave.GetEnemyCount(); j++)
                {
                    Instantiate(
                        nextWave.GetEnemyPrefab(j), 
                        nextWave.GetStartingWaypoint().position,
                        Quaternion.Euler(0,0,180),
                        transform
                    );
                    
                    yield return new WaitForSeconds(nextWave.getRandomSpawnTime());
                }
            }
        } while (isLooping);
    }

    void IncreaseDifficulty()
    {
        if (timeBetweenWaves > 0)
        {
            timeBetweenWaves--;
            Mathf.Clamp(timeBetweenWaves, 0, Int32.MaxValue);
        }
        
        if (delayBetweenExtraWaves >= 10)
        {
            delayBetweenExtraWaves -= 10;
            Mathf.Clamp(delayBetweenExtraWaves, 0, Int32.MaxValue);
        }
        else if (delayBetweenExtraWaves > 0)
        {
            delayBetweenExtraWaves--;
            Mathf.Clamp(delayBetweenExtraWaves, 0, Int32.MaxValue);
        }
    }
}
