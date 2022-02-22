using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<WaveConfigSO> waveConfigs;
    [SerializeField] private float timeBetweenWaves;
    [SerializeField] private bool isLooping = true;
    private WaveConfigSO currentWave;
    
    void Start()
    {
        StartCoroutine(SpawnEnemyWaves());
    }

    public WaveConfigSO GetCurrentWave()
    {
        return currentWave;
    }

    IEnumerator SpawnEnemyWaves()
    {
        do
        {
            foreach (WaveConfigSO wave in waveConfigs)
            {
                currentWave = wave;
            
                for (int i = 0; i < currentWave.GetEnemyCount(); i++)
                {
                    Instantiate(
                        currentWave.GetEnemyPrefab(i), 
                        currentWave.GetStartingWaypoint().position,
                        Quaternion.Euler(0,0,180),
                        transform
                    );
                    yield return new WaitForSeconds(currentWave.getRandomSpawnTime());
                }
            
                yield return new WaitForSeconds(timeBetweenWaves);
            }
        } while (isLooping);

    }
}
