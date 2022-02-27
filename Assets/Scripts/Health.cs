using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int health = 50;
    [SerializeField] private int maxHealth = 50;
    [SerializeField] private int points = 10; // 2x amount of hits needed base
    [SerializeField] private ParticleSystem hitEffect;
    [SerializeField] private bool applyCameraShake;
    
    private CameraShake _cameraShake;
    private AudioPlayer _audioPlayer;
    private ScoreKeeper _scoreKeeper;
    private LevelManager _levelManager;
    
    private void Awake()
    {
        _audioPlayer = FindObjectOfType<AudioPlayer>();
        _cameraShake = Camera.main.GetComponent<CameraShake>();
        _scoreKeeper = FindObjectOfType<ScoreKeeper>();
        _levelManager = FindObjectOfType<LevelManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();
        if (damageDealer)
        {
            TakeDamage(damageDealer.GetDamage());
            PlayHitEffect();
            _audioPlayer.PlayOnHitSFX();
            ShakeCamera();
            damageDealer.Hit();
        }
    }

    void TakeDamage(int damage)
    {
        health -= damage;
        
        if (health <= 0)
        {
            ResolveDestroy();
            Destroy(gameObject);
        }
    }

    public int GetHealth()
    {
        return health;
    }
    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public void Heal(float value)
    {
        if ((health + value) > maxHealth)
        {
            health = maxHealth;
        }
        else
        {
            health += (int) value;    
        }
    }

    void PlayHitEffect()
    {
        if (hitEffect != null)
        {
            ParticleSystem instance = Instantiate(hitEffect, transform.position, Quaternion.identity); //instatiate at position of target being hit
            Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }
    
    void ResolveDestroy()
    {
        if (gameObject.tag.Equals("Enemy"))
        {
            _audioPlayer.PlayEnemyDestroySFX();
            _scoreKeeper.ModifyScore(points);
        }
        else
        {
            _audioPlayer.PlayPlayerDestroySFX();
            _levelManager.LoadGameOver();
        }
    }

    void ShakeCamera()
    {
        if (_cameraShake != null && applyCameraShake)
        {
            _cameraShake.Play();
        }
    }
}
