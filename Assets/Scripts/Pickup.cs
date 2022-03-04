using System;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private string type;
    [SerializeField] private float value;

    private UIDisplay _uiDisplay;
    private AudioPlayer _audioPlayer;

    private void Awake()
    {
        _uiDisplay = FindObjectOfType<UIDisplay>();
        _audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.tag.Equals("Player")) return;
        
        if (type.Equals("HP"))
        {
            col.GetComponent<Health>().Heal(value);
            _audioPlayer.PlayPickupSFX();
            _uiDisplay.DisplayText("HP UP!");
            Destroy(gameObject);
        }
        
        if (type.Equals("FR"))
        {
            col.GetComponent<Shooter>().IncreaseFiringRate();
            _audioPlayer.PlayPickupSFX();
            _uiDisplay.DisplayText("FIRE RATE UP!");
            Destroy(gameObject);
        }
        
        if (type.Equals("WU"))
        {
            col.GetComponent<Shooter>().IncreaseWeaponsUpgrade();
            _audioPlayer.PlayPickupSFX();
            _uiDisplay.DisplayText("WEAPONS UP!");
            Destroy(gameObject);
        }
        
        if (type.Equals("BOOST"))
        {
            col.GetComponent<Shooter>().ActivateBoost(value);
            _uiDisplay.DisplayBoost();
            _audioPlayer.PlayBoostSFX();
            _uiDisplay.DisplayText("BOOST ACTIVATED");
            Destroy(gameObject);
        }
        
        if (type.Equals("SHIELD"))
        {
            col.GetComponent<Health>().ActivateShield(value);
            _uiDisplay.DisplayShield();
            _audioPlayer.PlayOnForceFieldHitSFX();
            _uiDisplay.DisplayText("SHIELD ACTIVATED");
            Destroy(gameObject);
        }
    }

    public float GetValue()
    {
        return value;
    }
    
}
