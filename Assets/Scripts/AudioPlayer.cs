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

    // God mode in this class is a hack and should not be here - just finishing up project.
    private bool isAudioEnabled = true;
    private bool isLaserSoundsEnabled = true;
    private bool isGodModeEnabled;
    
    // Static instancess version of Singleton pattern
    private static AudioPlayer instance;
    
    private void Awake()
    {
        if (isAudioEnabled) GetComponent<AudioSource>().Play();
        ManageSingleton();
    }

    void ManageSingleton()
    {
        /* Private Singleton pattern
         * int instanceCount = FindObjectsOfType(GetType()).Length;
         * if (instanceCount > 1)
         */

        if (instance != null && isAudioEnabled)
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

    public void SetAudio()
    {
        isAudioEnabled = !isAudioEnabled;
        AudioSource theme = GetComponent<AudioSource>();
        if (theme.isPlaying)
        {
            theme.Stop();
        }
        else
        {
            theme.Play();
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
        if (shootingSFX != null && isAudioEnabled && isLaserSoundsEnabled)
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
        if (enemyShootingSFX != null && isAudioEnabled && isLaserSoundsEnabled)
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
        if (onHitSFX != null && isAudioEnabled && isLaserSoundsEnabled)
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
        if (onForceFieldHitSFX != null && isAudioEnabled)
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
        if (playerDestroyedSFX != null && isAudioEnabled)
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
        if (enemyDestroyedSFX != null && isAudioEnabled)
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
        if (pickupSFX != null && isAudioEnabled)
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
        if (boostSFX != null && isAudioEnabled)
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
        if (weaponsTierUpgradeSFX != null && isAudioEnabled)
        {
            AudioSource.PlayClipAtPoint(
                weaponsTierUpgradeSFX, 
                Camera.main.transform.position, 
                weaponsTierUpgradeSFXVolume
            );
        }
    }

    public void DisableLaserSounds()
    {
        isLaserSoundsEnabled = false;
    }
    
    public bool GetGodModeSetting()
    {
        Debug.Log("Returning God mode" + isGodModeEnabled);
        return isGodModeEnabled;
    }

    public void ToggleGodMode()
    {
        isGodModeEnabled = !isGodModeEnabled;
        Debug.Log("God mode" + isGodModeEnabled);
    }
}
