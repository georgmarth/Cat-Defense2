using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour {

    [HideInInspector]
    public int health { get; private set;}

    public int startingHealth = 20;
    public Text healthText;
    public static HealthManager healthManager;

    private void Start()
    {
        health = startingHealth;
        UpdateUI();
    }

    private void Awake()
    {
        if (healthManager == null)
        {
            healthManager = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetHealth(int amount)
    {
        health = amount;
        UpdateUI();
    }

    private void UpdateUI()
    {
        healthText.text = "Health: " + health;
    }

    public void Damage(int amount)
    {
        SetHealth(health - amount);
        if (health <= 0)
        {
            GameManager.gameManager.Die();
        }
    }

    public void Reset()
    {
        SetHealth(startingHealth);
    }
}
