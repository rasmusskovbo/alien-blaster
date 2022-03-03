using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class UIDisplay : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Health playerHealth;
    
    [Header("Score")]
    [SerializeField] private TextMeshProUGUI scoreText;

    [Header("Boost")] 
    [SerializeField] private GameObject boostPanel;
    [SerializeField] private Slider boostSlider;
    [SerializeField] private Pickup boostPickup;
    private float remainingBoostDuration;
    
    [Header("Shield")] 
    [SerializeField] private GameObject shieldPanel;
    [SerializeField] private Slider shieldSlider;
    [SerializeField] private Pickup shieldPickup;
    private float remainingShieldDuration;
    
    [Header("Weapons Upgrade")] 
    [SerializeField] private GameObject weaponsUpgradePanel;
    [SerializeField] private Slider weaponsUpgradeSlider;
    [SerializeField] private Shooter playerShooter;
    
    [Header("Fire Rate")] 
    [SerializeField] private GameObject fireRatePanel;
    [SerializeField] private Slider fireRateSlider;

    [Header("Sidebar")] 
    [SerializeField] private TextMeshProUGUI waveDisplay;
    [SerializeField] private TextMeshProUGUI difficultyDisplay;

    [Header("Announcement")] 
    [SerializeField] private TextMeshProUGUI announcementText;
    [SerializeField] private AnimatorController animatorController;

    private ScoreKeeper _scoreKeeper;

    private void Awake()
    {
        _scoreKeeper = FindObjectOfType<ScoreKeeper>();
        boostPanel.SetActive(false);
        shieldPanel.SetActive(false);
    }

    private void Start()
    {
        healthSlider.maxValue = playerHealth.GetHealth();
        boostSlider.maxValue = boostPickup.GetValue();
        shieldSlider.maxValue = shieldPickup.GetValue();
        weaponsUpgradeSlider.maxValue = playerShooter.GetUpgradesPerTier();
        SetFireRateSlider(
            playerShooter.GetProjectileSpeed(),
            playerShooter.GetFastestProjectileSpeed()
        );
    }
    

    private void Update()
    {
        UpdateScore();
        UpdateHealth();
        UpdateBoost();
        UpdateShield();
        UpdateWeaponsUpgrade();
        UpdateFireRate();
    }

    void UpdateScore()
    {
        scoreText.text = _scoreKeeper.GetPlayerScore().ToString("000000000");
    }

    void UpdateHealth()
    {
        int hp = playerHealth.GetHealth();
        int maxHP = playerHealth.GetMaxHealth();

        if (hp == maxHP)
        {
            healthSlider.value = maxHP + 1;
        }
        else
        {
            healthSlider.value = playerHealth.GetHealth();    
        }
    }

    void UpdateBoost()
    {
        boostSlider.value = remainingBoostDuration;
    }
    
    void UpdateShield()
    {
        shieldSlider.value = remainingShieldDuration;
    }
    
    void UpdateWeaponsUpgrade()
    {
        weaponsUpgradeSlider.value = playerShooter.GetCurrentUpgradeProgress();
    }

    public void UpdateWeaponsUpgradeSlider(int value)
    {
        weaponsUpgradeSlider.maxValue = value;
    }

    public void DisableWeaponsUpgradeSlider()
    {
        weaponsUpgradePanel.SetActive(false);
    }
    
    void UpdateFireRate()
    {
        fireRateSlider.value = playerShooter.GetProjectileSpeed();
    }

    public void UpdateWaveCounter(int value)
    {
        waveDisplay.text = value.ToString();
    }
    
    public void UpdateDifficultyCounter(int value)
    {
        difficultyDisplay.text = value.ToString();
    }

    void SetFireRateSlider(float min, float max)
    {
        fireRateSlider.minValue = min;
        fireRateSlider.maxValue = max;
    }

    public void DisableFireRateSlider()
    {
        fireRatePanel.SetActive(false);
    }

    public void DisplayBoost()
    {
        StartCoroutine(BoostCountdown());
    }

    IEnumerator BoostCountdown()
    {
        boostPanel.SetActive(true);
        remainingBoostDuration = boostPickup.GetValue();
        
        while (remainingBoostDuration > 0)
        {
            remainingBoostDuration -= Time.deltaTime;
            yield return null;
        }

        boostPanel.SetActive(false);
    }
    
    public void DisplayShield()
    {
        StartCoroutine(ShieldCountdown());
    }

    IEnumerator ShieldCountdown()
    {
        shieldPanel.SetActive(true);
        remainingShieldDuration = shieldPickup.GetValue();
        
        while (remainingShieldDuration > 0)
        {
            remainingShieldDuration -= Time.deltaTime;
            yield return null;
        }

        shieldPanel.SetActive(false);
    }
}
