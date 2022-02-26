using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float firingSpeed = 0.2f;
    [SerializeField] private float projectileLifetime = 5f;
    
    [Header("AI")]
    [SerializeField] private bool useAi;
    [SerializeField] private float minimumFiringSpeed = 1f;
    [SerializeField] private float maximumFiringSpeed = 3f;
    [SerializeField] private float firingSpeedVariance = 1f;
    
    [HideInInspector]
    public bool isFiring = false;
    
    private Coroutine firingCoroutine;
    private AudioPlayer _audioPlayer;

    private void Awake()
    {
        _audioPlayer = FindObjectOfType<AudioPlayer>();
    }


    void Start()
    {
        if (useAi) isFiring = true;
    }
    
    void Update()
    {
        Fire();
    }

    void Fire()
    {
        
        if (isFiring && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinuously());    
        }
        else if (!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
        
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject projectile = Instantiate(
                projectilePrefab,
                transform.position,
                Quaternion.identity
            );
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

            // This works because rigidbody is kinematic = final velocity, not affected by physics
            if (rb) rb.velocity = transform.up * projectileSpeed;
            
            Destroy(projectile, projectileLifetime);
            
            
            
            if (useAi)
            {
                _audioPlayer.PlayEnemyShootingFX();
                yield return new WaitForSeconds(GetRandomFiringRate());
            }
            else
            {
                _audioPlayer.PlayShootingFX();
                yield return new WaitForSeconds(firingSpeed);
            }
        }
    }

    float GetRandomFiringRate()
    {
        float randomFiringRate = Random.Range(
            firingSpeed - firingSpeedVariance,
            firingSpeed + firingSpeedVariance
        );

        return Mathf.Clamp(randomFiringRate, minimumFiringSpeed, maximumFiringSpeed);
    }
    
}
