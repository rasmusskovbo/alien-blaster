using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [Header("Boss")] 
    [SerializeField] private List<WaveConfigSO> bossConfigs;
    [SerializeField] private float timeBetweenBossSpawns = 60f;
    [SerializeField] private float bossSpawnIncrement = 10f;


    void Start()
    {
        //StartCoroutine(StartSpawn());
    }
    
    IEnumerator StartSpawn()
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
            );

        } while (true);
    }
}
