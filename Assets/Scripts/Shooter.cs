using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject boostProjectilePrefab;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float firingSpeed = 1f;
    [SerializeField] private float projectileLifetime = 5f;
    [SerializeField] private float fastestFiringSpeed = 0.05f;
    [SerializeField] private float boostFireOffset = 0.3f;
    bool boostActive = false;
    
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
            if (!boostActive)
            {
                yield return FireProjectile();
            }
            else
            {
                yield return BoostFire();
            }

        }
    }

    private object BoostFire()
    {
        float positionX = transform.position.x;
        float positionY = transform.position.y;

        GameObject projectileLeft = Instantiate(
            boostProjectilePrefab,
            new Vector3(positionX - boostFireOffset, positionY),
            Quaternion.identity
        );
        GameObject projectileMiddle = Instantiate(
            boostProjectilePrefab,
            transform.position,
            Quaternion.identity
        );
        GameObject projectileRight = Instantiate(
            boostProjectilePrefab,
            new Vector3(positionX + boostFireOffset, positionY),
            Quaternion.identity
        );
        
        List<GameObject> projectiles = new List<GameObject>();
        projectiles.Add(projectileLeft);
        projectiles.Add(projectileMiddle);
        projectiles.Add(projectileRight);

        foreach (GameObject projectile in projectiles)
        {
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

            if (rb) rb.velocity = transform.up * projectileSpeed;

            Destroy(projectile, projectileLifetime);
        }

        _audioPlayer.PlayShootingFX();
        return new WaitForSeconds(firingSpeed);
    }

    private object FireProjectile()
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
            return new WaitForSeconds(GetRandomFiringRate());
        }
        else
        {
            _audioPlayer.PlayShootingFX();
            return new WaitForSeconds(firingSpeed);
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

    public void IncreaseFiringRate(float value)
    {
        if ((firingSpeed - value) <= fastestFiringSpeed) return;
            
        firingSpeed -= value;
        projectileSpeed += value * 2;

    }

    public void ActivateBoost(float duration)
    {
        StartCoroutine(InnerBoost(duration));
    }

    IEnumerator InnerBoost(float duration)
    {
        Debug.Log("Activate boost");
        float currentFiringRate = firingSpeed;

        firingSpeed = fastestFiringSpeed;
        boostActive = true;

        yield return new WaitForSecondsRealtime(duration);

        Debug.Log("Resuming control");
        firingSpeed = currentFiringRate;
            
        boostActive = false; 
    }

}
