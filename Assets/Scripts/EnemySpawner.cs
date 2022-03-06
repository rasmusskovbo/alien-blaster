using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [Header("Waves")]
    [SerializeField] private List<WaveConfigSO> waveConfigs;
    [SerializeField] private float timeBetweenWaves = 2;
    [SerializeField] private int maxTimeBetweenWaves = 10;
    [SerializeField] private bool isLooping = true;
    [SerializeField] private int maxAmountOfSpawners = 5;
    [SerializeField] private int timeBetweenExtraSpawners = 30;

    [Header("Boss")] 
    [SerializeField] private List<WaveConfigSO> bossConfigs;
    [SerializeField] private float timeBetweenBossSpawns = 60f;
    [SerializeField] private float bossSpawnIncrement = 10f;
    [SerializeField] private float minimumTimeBetweenBossSpawns = 5f;

    [Header("Difficulty")]
    [SerializeField] private int difficultyCounter;
    [SerializeField] private int spawnExtraBossesAtWaveCount;
    [SerializeField] private int upgradeEnemiesAtWaveCount;
    [SerializeField] private int finalEnemiesAtWaveCount;
    [SerializeField] private GameObject upgradedEnemyPrefab;
    [SerializeField] private GameObject finalEnemyPrefab;
    private int enemyLevel = 0;

    private UIDisplay _uiDisplay;
    private int waveCounter;
    
    void Start()
    {
        waveCounter = 0;
        _uiDisplay = FindObjectOfType<UIDisplay>();
        _uiDisplay.UpdateDifficultyCounter(difficultyCounter);
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
            
            waveCounter++;
            _uiDisplay.UpdateWaveCounter(waveCounter);

            for (int i = 0; i < wave.GetEnemyCount(); i++)
            {
                if (enemyLevel == 0)
                {
                    Instantiate(
                        wave.GetEnemyPrefab(i),
                        wave.GetStartingWaypoint().position,
                        Quaternion.Euler(0, 0, 180),
                        transform
                    ).GetComponent<Pathfinder>().SetCurrentWave(wave); 
                }
                else if (enemyLevel == 1)
                {
                    Instantiate(
                        upgradedEnemyPrefab,
                        wave.GetStartingWaypoint().position,
                        Quaternion.Euler(0, 0, 180),
                        transform
                    ).GetComponent<Pathfinder>().SetCurrentWave(wave); 
                }
                else if (enemyLevel == 2)
                {
                    Instantiate(
                        finalEnemyPrefab,
                        wave.GetStartingWaypoint().position,
                        Quaternion.Euler(0, 0, 180),
                        transform
                    ).GetComponent<Pathfinder>().SetCurrentWave(wave); 
                }
                
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
    
    IEnumerator ScaleDifficulty()
    {
        while (difficultyCounter < maxAmountOfSpawners)
        {
            Debug.Log("Difficulty level: " + difficultyCounter);
            yield return new WaitForSeconds(timeBetweenExtraSpawners);
            
            if (timeBetweenWaves < maxTimeBetweenWaves) timeBetweenWaves++;
            if (timeBetweenBossSpawns > minimumTimeBetweenBossSpawns) timeBetweenBossSpawns -= bossSpawnIncrement;
            difficultyCounter++;
            _uiDisplay.UpdateDifficultyCounter(difficultyCounter);
            _uiDisplay.DisplayText("DIFFICULTY UP");
            StartCoroutine(RandomWaveSpawner());
            
            if (difficultyCounter == spawnExtraBossesAtWaveCount)
            {
                StartCoroutine(BossSpawner());
                FindObjectOfType<AudioPlayer>().DisableLaserSounds();
                Debug.Log("Spawning extra boss");
                _uiDisplay.DisplayText("UFO OVERFLOW");
            }

            if (difficultyCounter >= upgradeEnemiesAtWaveCount && enemyLevel < 1)
            {
                Debug.Log("Enemies upgraded");
                _uiDisplay.DisplayText("MASSIVE ATTACK");
                enemyLevel = 1;
            }
            
            if (difficultyCounter >= finalEnemiesAtWaveCount)
            {
                Debug.Log("Enemies upgraded");
                _uiDisplay.DisplayText("SUDDEN DEATH");
                enemyLevel = 2;
            }
            
        }
        
        
        
    }
    
}
