using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Scriptable Object = Easily replicable interface like object, that can hold dynamic data, e.g. paths, movespeed etc to be loaded.

// Add to Right Click add asset
[CreateAssetMenu(menuName = "Wave Config", fileName = "New Wave Config")]
public class WaveConfigSO : ScriptableObject
{
    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private Transform pathPrefab;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float timeBetweenSpawns = 1f;
    [SerializeField] private float spawnTimeVariance = 0f;
    [SerializeField] private float minimumSpawnTime = 0.5f;

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    public Transform GetStartingWaypoint()
    {
        return pathPrefab.GetChild(0);
    }

    public List<Transform> GetWaypoints()
    {
        List<Transform> waypoints = new List<Transform>();
        
        foreach (Transform waypoint in pathPrefab)
        {
            waypoints.Add(waypoint);
        }

        return waypoints;
    }

    public int GetEnemyCount()
    {
        return enemyPrefabs.Count;
    }

    public GameObject GetEnemyPrefab(int index)
    {
        return enemyPrefabs[index];
    }

    public float getRandomSpawnTime()
    {
        float spawnTime = Random.Range(
            timeBetweenSpawns - spawnTimeVariance,
            timeBetweenSpawns + spawnTimeVariance
        );

        return Mathf.Clamp(spawnTime, minimumSpawnTime, float.MaxValue);
    }
}
