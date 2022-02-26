using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] private AudioClip shootingSFX;
    [SerializeField] private AudioClip enemyShootingSFX;
    [SerializeField] [Range(0f, 1f)] private float shootingFXVolume = 0.25f;

    [Header("Damage")] 
    [SerializeField] private AudioClip onHitSFX;
    [SerializeField] private AudioClip playerDestroyedSFX;
    [SerializeField] private AudioClip enemyDestroyedSFX;
    [SerializeField] [Range(0f, 1f)] private float onHitVolume = 0.25f;
    [SerializeField] [Range(0f, 1f)] private float playerDestroyedVolume = 0.75f;
    [SerializeField] [Range(0f, 1f)] private float enemyDestroyedVolume = 0.25f;

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
}
