using System;
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
    
    private ScoreKeeper _scoreKeeper;

    private void Awake()
    {
        _scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    private void Start()
    {
        healthSlider.maxValue = playerHealth.GetHealth();
    }

    private void Update()
    {
        UpdateScore();
        UpdateHealth();
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
}
