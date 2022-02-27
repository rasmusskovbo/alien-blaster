using System;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private string type;
    [SerializeField] private float value;

    private AudioPlayer _audioPlayer;

    private void Awake()
    {
        _audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.tag.Equals("Player")) return;
        
        if (type.Equals("HP"))
        {
            col.GetComponent<Health>().Heal(value);
            _audioPlayer.PlayPickupSFX();
            Destroy(gameObject);
        }
        
        if (type.Equals("FR"))
        {
            col.GetComponent<Shooter>().IncreaseFiringRate(value);
            _audioPlayer.PlayPickupSFX();
            Destroy(gameObject);
        }
        
        if (type.Equals("BOOST"))
        {
            col.GetComponent<Shooter>().ActivateBoost(value);
            _audioPlayer.PlayBoostSFX();
            Destroy(gameObject);
        }
    }
    
}
