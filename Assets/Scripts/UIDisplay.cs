using System;
using System.Collections;
using TMPro;
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
    private float remainingDuration = 0;
    
    private ScoreKeeper _scoreKeeper;

    private void Awake()
    {
        _scoreKeeper = FindObjectOfType<ScoreKeeper>();
        boostPanel.SetActive(false);
    }

    private void Start()
    {
        healthSlider.maxValue = playerHealth.GetHealth();
        boostSlider.maxValue = boostPickup.GetValue();
    }

    private void Update()
    {
        UpdateScore();
        UpdateHealth();
        UpdateBoost();
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
        boostSlider.value = remainingDuration;
    }

    public void DisplayBoost()
    {
        StartCoroutine(BoostCountdown());
    }

    IEnumerator BoostCountdown()
    {
        boostPanel.SetActive(true);
        remainingDuration = boostPickup.GetValue();
        
        while (remainingDuration > 0)
        {
            remainingDuration -= Time.deltaTime;
            yield return null;
        }

        boostPanel.SetActive(false);
    }
}
