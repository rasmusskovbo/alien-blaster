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
    [SerializeField] private float firingSpeed = 0.35f;
    [SerializeField] private float projectileLifetime = 5f;

    bool boostActive = false;
    
    [Header("AI")]
    [SerializeField] private bool useAi;
    [SerializeField] private float minimumFiringSpeed = 1f;
    [SerializeField] private float maximumFiringSpeed = 3f;
    [SerializeField] private float firingSpeedVariance = 1f;
    
    [Header("Player Specific")]
    [SerializeField] private float fastestFiringSpeed = 0.05f;
    [SerializeField] private float fastestProjectileSpeed = 30f;
    [SerializeField] private float projectileSpeedIncrement = 1f;
    [SerializeField] private float firingSpeedIncrement = 0.03f;
    [SerializeField] private float boostFireOffset = 0.4f;
    private float temporaryFireRate;
    
    [Header("Weapons Upgrade")]
    [SerializeField] private int currentUpgradeProgress;
    [SerializeField] private int upgradesRequiredPerTier = 15;
    [SerializeField] private int upgradeScaling = 5;
    [SerializeField] private int maxWeaponsTier = 4;
    [SerializeField] private int projectileRotation = 15;
    [SerializeField] private GameObject tier2Prefab;
    [SerializeField] private GameObject tier3Prefab;
    private int weaponsTier = 1;
    
    
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
            if (boostActive)
            {
                yield return BoostFire();
            }
            else
                switch (weaponsTier)
                {
                    default:
                        yield return FireProjectile();
                        break;
                    
                    case 2:
                        yield return TierTwoFire();
                        SwapProjectilePrefab(tier2Prefab);
                        break;
                    
                    case 3:
                        yield return TierThreeFire();
                        SwapProjectilePrefab(tier3Prefab);
                        break;
                    
                    case 4:
                        yield return TierFourFire();
                        SwapProjectilePrefab(boostProjectilePrefab);
                        break;
                }
        }
    }

    private object BoostFire()
    {
        float positionX = transform.position.x;
        float positionY = transform.position.y;

        GameObject projectileLeftRotate = Instantiate(
            boostProjectilePrefab,
            new Vector3(positionX - boostFireOffset, positionY),
            Quaternion.Euler(0,0,projectileRotation)
        );
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
        GameObject projectileRightRotate = Instantiate(
            boostProjectilePrefab,
            new Vector3(positionX + boostFireOffset, positionY),
            Quaternion.Euler(0,0,-projectileRotation)
        );
        
        List<GameObject> projectiles = new List<GameObject>();
        projectiles.Add(projectileLeftRotate);
        projectiles.Add(projectileLeft);
        projectiles.Add(projectileMiddle);
        projectiles.Add(projectileRight);
        projectiles.Add(projectileRightRotate);

        FireProjectilesLoop(projectiles);
        
        return new WaitForSeconds(firingSpeed);
    }
    
    private object TierTwoFire()
    {
        float positionX = transform.position.x;
        float positionY = transform.position.y;

        GameObject projectileLeft = Instantiate(
            projectilePrefab,
            new Vector3(positionX - boostFireOffset / 2, positionY),
            Quaternion.identity
        );
        GameObject projectileRight = Instantiate(
            projectilePrefab,
            new Vector3(positionX + boostFireOffset / 2, positionY),
            Quaternion.identity
        );
        
        List<GameObject> projectiles = new List<GameObject>();
        projectiles.Add(projectileLeft);
        projectiles.Add(projectileRight);

        FireProjectilesLoop(projectiles);
        
        return new WaitForSeconds(firingSpeed);
    }
    
    private object TierThreeFire()
    {
        float positionX = transform.position.x;
        float positionY = transform.position.y;

        GameObject projectileLeft = Instantiate(
            projectilePrefab,
            new Vector3(positionX - boostFireOffset, positionY),
            Quaternion.Euler(0,0,projectileRotation)
        );
        GameObject projectileMiddleLeft = Instantiate(
            projectilePrefab,
            new Vector3(positionX - boostFireOffset / 2, positionY),
            Quaternion.identity
        );
        GameObject projectileMiddleRight = Instantiate(
            projectilePrefab,
            new Vector3(positionX + boostFireOffset / 2, positionY),
            Quaternion.identity
        );
        GameObject projectileRight = Instantiate(
            projectilePrefab,
            new Vector3(positionX + boostFireOffset, positionY),
            Quaternion.Euler(0,0,-projectileRotation)
        );
        
        List<GameObject> projectiles = new List<GameObject>();
        projectiles.Add(projectileLeft);
        projectiles.Add(projectileMiddleLeft);
        projectiles.Add(projectileMiddleRight);
        projectiles.Add(projectileRight);

        FireProjectilesLoop(projectiles);
        
        return new WaitForSeconds(firingSpeed);
    }
    
    private object TierFourFire()
    {
        float positionX = transform.position.x;
        float positionY = transform.position.y;

        GameObject projectileFarLeft = Instantiate(
            projectilePrefab,
            new Vector3(positionX - boostFireOffset, positionY),
            Quaternion.Euler(0,0,90)
        );
        GameObject projectileLeft = Instantiate(
            projectilePrefab,
            new Vector3(positionX - boostFireOffset, positionY),
            Quaternion.Euler(0,0,projectileRotation)
        );
        GameObject projectileMiddleLeft = Instantiate(
            projectilePrefab,
            new Vector3(positionX - boostFireOffset / 2, positionY),
            Quaternion.identity
        );
        GameObject projectileMiddleRight = Instantiate(
            projectilePrefab,
            new Vector3(positionX + boostFireOffset / 2, positionY),
            Quaternion.identity
        );
        GameObject projectileRight = Instantiate(
            projectilePrefab,
            new Vector3(positionX + boostFireOffset, positionY),
            Quaternion.Euler(0,0,-projectileRotation)
        );
        GameObject projectileFarRight = Instantiate(
            projectilePrefab,
            new Vector3(positionX + boostFireOffset, positionY),
            Quaternion.Euler(0,0,-90)
        );
        GameObject projectileBottomLeft = Instantiate(
            projectilePrefab,
            new Vector3(positionX - boostFireOffset / 2, positionY),
            Quaternion.Euler(0,0,180)
        );
        GameObject projectileBottomRight = Instantiate(
            projectilePrefab,
            new Vector3(positionX + boostFireOffset / 2, positionY),
            Quaternion.Euler(0,0,180)
        );
        
        List<GameObject> projectiles = new List<GameObject>();
        projectiles.Add(projectileFarLeft);
        projectiles.Add(projectileLeft);
        projectiles.Add(projectileMiddleLeft);
        projectiles.Add(projectileMiddleRight);
        projectiles.Add(projectileRight);
        projectiles.Add(projectileFarRight);
        projectiles.Add(projectileBottomLeft);
        projectiles.Add(projectileBottomRight);

        FireProjectilesLoop(projectiles);
        
        return new WaitForSeconds(firingSpeed);
    }

    public void FireProjectilesLoop(List<GameObject> projectiles)
    {
        foreach (GameObject projectile in projectiles)
        {
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

            if (rb) rb.velocity = projectile.transform.up * projectileSpeed;

            Destroy(projectile, projectileLifetime);
        }

        _audioPlayer.PlayShootingFX();
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

    public void IncreaseFiringRate()
    {
        if ((firingSpeed - firingSpeedIncrement) < fastestFiringSpeed)
        {
            firingSpeed = fastestFiringSpeed;
            if (!boostActive) temporaryFireRate = fastestFiringSpeed;
        }
        else
        {
            firingSpeed -= firingSpeedIncrement; 
            temporaryFireRate -= firingSpeedIncrement;
        }

        
        if ((projectileSpeed + projectileSpeedIncrement) > fastestProjectileSpeed)
        {
            projectileSpeed = fastestProjectileSpeed; 
        }
        else
        {
            projectileSpeed += projectileSpeedIncrement;
        }

        if (ShouldStopFireRatePickup())
        {
            FindObjectOfType<PickupSpawner>().StopFireRateSpawn();
            FindObjectOfType<UIDisplay>().DisableFireRateSlider();
            _audioPlayer.PlayWeaponsTierUpgradeSFX();
        }
    }

    public void IncreaseWeaponsUpgrade()
    {
        currentUpgradeProgress++;
        UpgradeWeaponsAndResetTier();
        
        if (ShouldStopWeaponsUpgradePickup())
        {
            FindObjectOfType<PickupSpawner>().StopBoostSpawn();
            FindObjectOfType<PickupSpawner>().StopWeaponsUpgradeSpawn();
            FindObjectOfType<UIDisplay>().DisableWeaponsUpgradeSlider();
        }
    }

    public void ActivateBoost(float duration)
    {
        StartCoroutine(InnerBoost(duration));
    }

    IEnumerator InnerBoost(float duration)
    {
        temporaryFireRate = firingSpeed;

        firingSpeed = fastestFiringSpeed;
        boostActive = true;

        yield return new WaitForSecondsRealtime(duration);
        
        firingSpeed = temporaryFireRate;
            
        boostActive = false; 
    }

    public void UpgradeWeaponsAndResetTier()
    {
        if (currentUpgradeProgress >= upgradesRequiredPerTier)
        {
            currentUpgradeProgress = 0;
            upgradesRequiredPerTier += upgradeScaling;
            FindObjectOfType<UIDisplay>().UpdateWeaponsUpgradeSlider(upgradesRequiredPerTier);
            _audioPlayer.PlayWeaponsTierUpgradeSFX();
            UpgradeWeaponsTier();
        }
    }

    public void UpgradeWeaponsTier()
    {
        if (weaponsTier < maxWeaponsTier)
        {
            weaponsTier++;
        }
    }

    public int GetUpgradesPerTier()
    {
        return upgradesRequiredPerTier;
    }

    public int GetCurrentUpgradeProgress()
    {
        return currentUpgradeProgress;
    }
    
    public bool ShouldStopWeaponsUpgradePickup()
    {
        return weaponsTier == maxWeaponsTier;
    }
    
    public bool ShouldStopFireRatePickup()
    {
        return projectileSpeed == fastestProjectileSpeed && firingSpeed == fastestFiringSpeed;
    }

    public float GetFireRate()
    {
        return firingSpeed;
    }
    
    public float GetProjectileSpeed()
    {
        return projectileSpeed;
    }
    
    public float GetFastestProjectileSpeed()
    {
        return fastestProjectileSpeed;
    }

    public void SwapProjectilePrefab(GameObject newPrefab) 
    {
        this.projectilePrefab = newPrefab;
    }

}
