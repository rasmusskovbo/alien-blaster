using System;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] private AudioClip shootingSFX;
    [SerializeField] private AudioClip enemyShootingSFX;
    [SerializeField] [Range(0f, 1f)] private float shootingFXVolume = 0.25f;

    [Header("Damage")] 
    [SerializeField] private AudioClip onHitSFX;
    [SerializeField] private AudioClip onForceFieldHitSFX;
    [SerializeField] private AudioClip playerDestroyedSFX;
    [SerializeField] private AudioClip enemyDestroyedSFX;
    [SerializeField] [Range(0f, 1f)] private float onHitVolume = 0.25f;
    [SerializeField] [Range(0f, 1f)] private float playerDestroyedVolume = 0.75f;
    [SerializeField] [Range(0f, 1f)] private float enemyDestroyedVolume = 0.25f;
    
    [Header("Pickups")]
    [SerializeField] private AudioClip pickupSFX;
    [SerializeField] private AudioClip boostSFX;
    [SerializeField] [Range(0f, 1f)] private float pickupSFXVolume = 0.25f;

    [Header("Upgrades")] 
    [SerializeField] private AudioClip weaponsTierUpgradeSFX;
    [SerializeField] [Range(0f, 1f)] private float weaponsTierUpgradeSFXVolume = 0.25f;

    // Static instancess version of Singleton pattern
    private static AudioPlayer instance;
    
    private void Awake()
    {
        ManageSingleton();
    }

    void ManageSingleton()
    {
        /* Private Singleton pattern
         * int instanceCount = FindObjectsOfType(GetType()).Length;
         * if (instanceCount > 1)
         */

        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    /* Used for Static version of Singleton pattern
    public AudioPlayer GetInstance()
    {
        return instance;
    }
    */

    public void PlayShootingFX()
    {
        if (shootingSFX != null)
        {
            AudioSource.PlayClipAtPoint(
                shootingSFX, 
                Camera.main.transform.position, 
                shootingFXVolume
            );
        }
    }
    
    public void PlayEnemyShootingFX()
    {
        if (enemyShootingSFX != null)
        {
            AudioSource.PlayClipAtPoint(
                enemyShootingSFX, 
                Camera.main.transform.position, 
                shootingFXVolume
            );
        }
    }

    public void PlayOnHitSFX()
    {
        if (onHitSFX != null)
        {
            AudioSource.PlayClipAtPoint(
                onHitSFX, 
                Camera.main.transform.position, 
                onHitVolume
            );
        }
    }
    
    public void PlayOnForceFieldHitSFX()
    {
        if (onForceFieldHitSFX != null)
        {
            AudioSource.PlayClipAtPoint(
                onForceFieldHitSFX, 
                Camera.main.transform.position, 
                onHitVolume
            );
        }
    }

    public void PlayPlayerDestroySFX()
    {
        if (playerDestroyedSFX != null)
        {
            AudioSource.PlayClipAtPoint(
                playerDestroyedSFX, 
                Camera.main.transform.position, 
                playerDestroyedVolume
            );
        }
    }
    
    public void PlayEnemyDestroySFX()
    {
        if (enemyDestroyedSFX != null)
        {
            AudioSource.PlayClipAtPoint(
                enemyDestroyedSFX, 
                Camera.main.transform.position, 
                enemyDestroyedVolume
            );
        }
    }

    public void PlayPickupSFX()
    {
        if (pickupSFX != null)
        {
            AudioSource.PlayClipAtPoint(
                pickupSFX, 
                Camera.main.transform.position, 
                pickupSFXVolume
            );
        }
    }
    
    public void PlayBoostSFX()
    {
        if (boostSFX != null)
        {
            AudioSource.PlayClipAtPoint(
                boostSFX, 
                Camera.main.transform.position, 
                pickupSFXVolume
            );
        }
    }
    
    public void PlayWeaponsTierUpgradeSFX()
    {
        if (weaponsTierUpgradeSFX != null)
        {
            AudioSource.PlayClipAtPoint(
                weaponsTierUpgradeSFX, 
                Camera.main.transform.position, 
                weaponsTierUpgradeSFXVolume
            );
        }
    }
}
